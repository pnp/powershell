using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "TermGroup")]
    public class GetTermGroup : PnPRetrievalsCmdlet<TermGroup>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public TaxonomyTermGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<TermGroup, object>>[] { g => g.Name, g => g.Id };
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

            // Get Group
            if (Identity != null)
            {
                var group = Identity.GetGroup(termStore);
                group.EnsureProperties(RetrievalExpressions);
                WriteObject(group);
            }
            else
            {
                var query = termStore.Groups.IncludeWithDefaultProperties(RetrievalExpressions);
                var termGroups = ClientContext.LoadQuery(query);
                ClientContext.ExecuteQueryRetry();
                WriteObject(termGroups, true);
            }
        }
    }
}
