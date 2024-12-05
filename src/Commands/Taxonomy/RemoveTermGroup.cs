using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;


namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Remove, "PnPTermGroup", SupportsShouldProcess = true)]
    public class RemoveTermGroup : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("GroupName")]
        public TaxonomyTermGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            TermStore termStore;
            if (TermStore == null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = TermStore.GetTermStore(taxonomySession);
            }

            // Get Group
            var group = Identity.GetGroup(termStore);
            group.EnsureProperties(g => g.Name);
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
                throw new PSArgumentException("Cannot find group");
            }
        }
    }
}
