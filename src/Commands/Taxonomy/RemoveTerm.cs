using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Remove, "PnPTerm", SupportsShouldProcess = true)]
    public class RemoveTerm : PnPSharePointCmdlet
    {
        private const string ParameterSet_TERMID = "By Term Id";
        private const string ParameterSet_TERMNAME = "By Term Name";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermPipeBind Identity;


        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

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

            Term term = null;
            if (ParameterSetName == ParameterSet_TERMID)
            {
                term = Identity.GetTerm(ClientContext, termStore, null, false, null);
            }
            else
            {
                var termGroup = TermGroup.GetGroup(termStore);
                var termSet = TermSet.GetTermSet(termGroup);
                term = Identity.GetTerm(ClientContext, termStore, termSet, false, null);
            }
            if (Force || ShouldContinue($"Delete term {term.Name} with id {term.Id}", Properties.Resources.Confirm))
            {
                term.DeleteObject();
                termStore.CommitAll();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
