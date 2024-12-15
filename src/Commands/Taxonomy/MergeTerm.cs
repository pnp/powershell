using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Merge, "PnPTerm")]
    public class MergeTerm : PnPSharePointCmdlet
    {
        private const string ParameterSet_TERMID = "By Term Id";
             
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        [Alias("Term")]
        public TaxonomyTermPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        public TaxonomyTermPipeBind TargetTerm;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [Alias("TermStoreName")]
        public TaxonomyTermStorePipeBind TermStore;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            TermStore termStore = null;
            if (TermStore == null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = TermStore.GetTermStore(taxonomySession);
            }


            Term sourceterm = Identity.GetTerm(ClientContext, termStore, null, false, null);
            Term destinationterm = TargetTerm.GetTerm(ClientContext, termStore, null, false, null);

            sourceterm.Merge(destinationterm);
            ClientContext.ExecuteQueryRetry();


        }
    }
}

