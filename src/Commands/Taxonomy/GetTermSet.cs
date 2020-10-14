using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "TermSet")]
    public class GetTermSet : PnPRetrievalsCmdlet<TermSet>
    {
        [Parameter(Mandatory = false)]
        public TaxonomyTermSetPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<TermSet, object>>[] { g => g.Name, g => g.Id };
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

            var termGroup = TermGroup.GetGroup(termStore);

            if (Identity != null)
            {
                var termSet = Identity.GetTermSet(termGroup);
                
                ClientContext.Load(termSet, RetrievalExpressions);
                ClientContext.ExecuteQueryRetry();
                WriteObject(termSet);
            }
            else
            {
                var query = termGroup.TermSets.IncludeWithDefaultProperties(RetrievalExpressions);
                var termSets = ClientContext.LoadQuery(query);
                ClientContext.ExecuteQueryRetry();
                WriteObject(termSets, true);

            }

        }

    }
}
