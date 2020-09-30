using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using Resources = PnP.PowerShell.Commands.Properties.Resources;


namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Remove, "PnPTermGroup")]
    public class RemoveTermGroup : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string GroupName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string TermStoreName;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            TermStore termStore;
            if (string.IsNullOrEmpty(TermStoreName))
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = taxonomySession.TermStores.GetByName(TermStoreName);
            }
            // Get Group
            if (termStore != null)
            {
                var group = termStore.GetTermGroupByName(GroupName);
                if (group != null)
                {
                    if (Force || ShouldContinue(string.Format(Resources.RemoveTermGroup0AndAllUnderlyingTermSetsAndTerms, group.Name), Resources.Confirm))
                    {
                        group.EnsureProperty(g => group.TermSets);
                        if (group.TermSets.Any())
                        {
                            foreach (var termSet in group.TermSets)
                            {
                                termSet.DeleteObject();
                            }
                        }
                        group.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
                else
                {
                    ThrowTerminatingError(new ErrorRecord(new Exception("Cannot find group"), "INCORRECTGROUPNAME", ErrorCategory.ObjectNotFound, GroupName));

                }
            }
            else
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Cannot find termstore"),"INCORRECTTERMSTORE",ErrorCategory.ObjectNotFound,TermStoreName));
            }
        }

    }
}
