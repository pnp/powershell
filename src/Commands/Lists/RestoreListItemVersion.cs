using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Framework.Provisioning.ObjectHandlers.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Extensions;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsData.Restore, "PnPListItemVersion")]
    public class RestoreListItemVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemVersionPipeBind Version;
        
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        private static readonly FieldType[] UnsupportedFieldTypes =
        {
            FieldType.Attachments,
            FieldType.Computed
        };

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);

            if (list is null)
            {
                throw new PSArgumentException($"Cannot find the list provided through -{nameof(List)}", nameof(List));
            }

            var item = Identity.GetListItem(list);

            if (item is null)
            {
                throw new PSArgumentException($"Cannot find the list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            item.LoadProperties(i => i.Versions);

            ListItemVersion version = null;
            if (!string.IsNullOrEmpty(Version.VersionLabel))
            {
                version = item.Versions.FirstOrDefault(v => v.VersionLabel == Version.VersionLabel);
            }
            else if (Version.Id != -1)
            {
                version = item.Versions.FirstOrDefault(v => v.VersionId == Version.Id);
            }

            if (version is null)
            {
                throw new PSArgumentException($"Cannot find the list item version provided through -{nameof(Version)}", nameof(Version));
            }

            if (Force || ShouldContinue(string.Format(Resources.Restore, version.VersionLabel), Resources.Confirm))
            {
                LogDebug($"Trying to restore to version with label '{version.VersionLabel}'");
                
                var fields = ClientContext.LoadQuery(list.Fields.Include(f => f.InternalName, 
                    f => f.Title, f => f.Hidden, f => f.ReadOnlyField, f => f.FieldTypeKind));
                ClientContext.ExecuteQueryRetry();
                var itemValues = new List<FieldUpdateValue>();

                foreach (var fieldValue in version.FieldValues)
                {
                    var field = fields.FirstOrDefault(f => f.InternalName == fieldValue.Key || f.Title == fieldValue.Key);
                    if (field is { ReadOnlyField: false, Hidden: false } && !UnsupportedFieldTypes.Contains(field.FieldTypeKind))
                    {
                        if (field is TaxonomyField)
                        {
                            TaxonomyField taxField = ClientContext.CastTo<TaxonomyField>(field);
                            taxField.EnsureProperty(tf => tf.AllowMultipleValues);
                            if (taxField.AllowMultipleValues)
                            {
                                TaxonomyFieldValueCollection values = (TaxonomyFieldValueCollection)(fieldValue.Value);
                                var termValuesString = String.Empty;
                                if (values.Count > 0)
                                {
                                    foreach (var term in values)
                                    {
                                        termValuesString += "-1;#" + term.Label + "|" + term.TermGuid + ";#";
                                    }
                                    termValuesString = termValuesString.Substring(0, termValuesString.Length - 2);
                                }

                                var newTaxFieldValue = new TaxonomyFieldValueCollection(ClientContext, termValuesString, taxField);
                                itemValues.Add(new FieldUpdateValue(field.InternalName, newTaxFieldValue));
                                continue;
                            }   
                        }
                        itemValues.Add(new FieldUpdateValue(field.InternalName, fieldValue.Value));
                        
                    }
                }

                foreach (var itemValue in itemValues)
                {
                    item[itemValue.Key] = itemValue.Value;
                }

                item.Update();
                ClientContext.ExecuteQueryRetry();

                LogDebug($"Restored version {version.VersionLabel} of list item {item.Id} in list {list.Title}");
            }
        }
    }
}