using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Set, "PnPTaxonomyFieldValue", DefaultParameterSetName = "ITEM")]
    public class SetTaxonomyFieldValue : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListItem ListItem;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string InternalFieldName;

        [Parameter(Mandatory = true, ParameterSetName = "ITEM")]
        public GuidPipeBind TermId;

        [Parameter(Mandatory = false, ParameterSetName = "ITEM")]
        public string Label;

        [Parameter(Mandatory = true, ParameterSetName = "PATH")]
        public string TermPath;

        [Parameter(Mandatory = false, ParameterSetName = "ITEMS")]
        public Hashtable Terms;

        protected override void ExecuteCmdlet()
        {
            Field field = ListItem.ParentList.Fields.GetByInternalNameOrTitle(InternalFieldName);
            ListItem.Context.Load(field);
            ListItem.Context.ExecuteQueryRetry();

            switch (ParameterSetName)
            {
                case "ITEM":
                    {
                        ListItem.SetTaxonomyFieldValue(field.Id, Label, TermId.Id);
                        break;
                    }
                case "PATH":
                    {
                        ListItem.SetTaxonomyFieldValueByTermPath(TermPath, field.Id);
                        break;
                    }
                case "ITEMS":
                    {
                        var terms = new List<KeyValuePair<Guid, string>>();
                        foreach (string key in Terms.Keys)
                        {
                            var termId = Guid.Empty;
                            Guid.TryParse(key, out termId);

                            string termValue = Terms[key] as string;

                            if (termId != Guid.Empty && !string.IsNullOrEmpty(termValue))
                            {
                                terms.Add(new KeyValuePair<Guid, string>(termId, termValue));
                            }
                        }

                        ListItem.SetTaxonomyFieldValues(field.Id, terms);
                        break;
                    }
            }
        }
    }

}
