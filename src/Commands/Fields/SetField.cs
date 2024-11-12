using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Set, "PnPField")]
    [OutputType(typeof(void))]
    public class SetField : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public FieldPipeBind Identity = new FieldPipeBind();

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public SwitchParameter UpdateExistingLists;

        [Parameter(Mandatory = false)]
        public ShowInFiltersPaneStatus? ShowInFiltersPane;

        protected override void ExecuteCmdlet()
        {
            const string allowDeletionPropertyKey = "AllowDeletion";
            Field field = null;
            if (List != null)
            {
                WriteVerbose("Retrieving provided list");
                var list = List.GetList(CurrentWeb);

                if (list == null)
                {
                    throw new ArgumentException("Unable to retrieve the list specified using the List parameter", "List");
                }

                if (Identity.Id != Guid.Empty)
                {
                    WriteVerbose($"Retrieving field by its ID {Identity.Id} from the list");
                    field = list.Fields.GetById(Identity.Id);
                }
                else if (!string.IsNullOrEmpty(Identity.Name))
                {
                    WriteVerbose($"Retrieving field by its name {Identity.Name} from the list");
                    field = list.Fields.GetByInternalNameOrTitle(Identity.Name);
                }
                if (field == null)
                {
                    throw new ArgumentException("Unable to retrieve field with id, name or the field instance provided through Identity on the specified List", "Identity");
                }
            }
            else
            {
                if (Identity.Id != Guid.Empty)
                {
                    WriteVerbose($"Retrieving field by its ID {Identity.Id} from the web");
                    field = ClientContext.Web.Fields.GetById(Identity.Id);
                }
                else if (!string.IsNullOrEmpty(Identity.Name))
                {
                    WriteVerbose($"Retrieving field by its name {Identity.Name} from the web");
                    field = ClientContext.Web.Fields.GetByInternalNameOrTitle(Identity.Name);
                }
                else if (Identity.Field != null)
                {
                    WriteVerbose($"Using passed in field");
                    field = Identity.Field;
                }

                if (field == null)
                {
                    throw new ArgumentException("Unable to retrieve field with id, name or the field instance provided through Identity on the current web", "Identity");
                }
            }

            if (Values != null && Values.Count > 0 && Values.ContainsKey(allowDeletionPropertyKey))
            {
                ClientContext.Load(field, f => f.SchemaXmlWithResourceTokens);
            }
            else
            {
                ClientContext.Load(field);
            }
            if(ShowInFiltersPane.HasValue)
            {
                WriteVerbose($"Updating field to show in filters pane setting {ShowInFiltersPane.Value}");
                field.ShowInFiltersPane = ShowInFiltersPane.Value;
                field.Update();
            }
            ClientContext.ExecuteQueryRetry();

            if (Values != null  && Values.Count > 0)
            {
                WriteVerbose($"Updating {Values.Count} field value{(Values.Count != 1 ? "s" : "")}");

                // Get a reference to the type-specific object to allow setting type-specific properties, i.e. LookupList and LookupField for Microsoft.SharePoint.Client.FieldLookup
                var typeSpecificField = field.TypedObject;

                foreach (string key in Values.Keys)
                {
                    var value = Values[key];

                    WriteVerbose($"Updating field {key} to {value}");

                    var property = typeSpecificField.GetType().GetProperty(key);

                    bool isAllowDeletionProperty = string.Equals(key, allowDeletionPropertyKey, StringComparison.Ordinal);

                    if (property == null && !isAllowDeletionProperty)
                    {
                        WriteWarning($"No property '{key}' found on this field. Value will be ignored.");
                    }
                    else
                    {
                        try
                        {
                            if (isAllowDeletionProperty)
                            {
                                field.SetAllowDeletion(value as bool?);
                            }
                            else
                            {
                                property.SetValue(typeSpecificField, value);
                            }
                        }
                        catch (Exception e)
                        {
                            WriteWarning($"Setting property '{key}' to '{value}' failed with exception '{e.Message}'. Value will be ignored.");
                        }
                    }
                }
                field.UpdateAndPushChanges(UpdateExistingLists);
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}