using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Framework.Entities;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Add, "PnPTaxonomyField")]
    public class AddTaxonomyField : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string DisplayName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string InternalName;

        [Parameter(Mandatory = true, ParameterSetName = "Path")]
        public string TermSetPath;

        [Parameter(Mandatory = false, ParameterSetName = "Id")]
        public GuidPipeBind TaxonomyItemId;

        [Parameter(Mandatory = false, ParameterSetName = "Path")]
        public string TermPathDelimiter = "|";

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Group;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public GuidPipeBind Id = new GuidPipeBind();

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter AddToDefaultView;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter MultiValue;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Required;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public AddFieldOptions FieldOptions = AddFieldOptions.DefaultValue;


        protected override void ExecuteCmdlet()
        {
            TaxonomyItem taxItem;
            Field field;
            if (ParameterSetName == "Path")
            {
                taxItem = ClientContext.Site.GetTaxonomyItemByPath(TermSetPath, TermPathDelimiter);
            }
            else
            {
                var taxSession = ClientContext.Site.GetTaxonomySession();
                var termStore = taxSession.GetDefaultKeywordsTermStore();
                try
                {
                    taxItem = termStore.GetTermSet(TaxonomyItemId.Id);
                }
                catch
                {
                    try
                    {
                        taxItem = termStore.GetTerm(TaxonomyItemId.Id);
                    }
                    catch
                    {
                        throw new Exception($"Taxonomy Item with Id {TaxonomyItemId.Id} not found");
                    }
                }
                taxItem.EnsureProperty(t => t.Id);
            }

            Guid id = Id.Id;
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }

            var fieldCI = new TaxonomyFieldCreationInformation()
            {
                Id = id,
                InternalName = InternalName,
                DisplayName = DisplayName,
                Group = Group,
                TaxonomyItem = taxItem,
                MultiValue = MultiValue,
                Required = Required,
                AddToDefaultView = AddToDefaultView
            };

            if (List != null)
            {
                var list = List.GetList(SelectedWeb);
                field = list.CreateTaxonomyField(fieldCI);
            }
            else
            {
                field = SelectedWeb.CreateTaxonomyField(fieldCI);
            }
            WriteObject(ClientContext.CastTo<TaxonomyField>(field));
        }

    }

}
