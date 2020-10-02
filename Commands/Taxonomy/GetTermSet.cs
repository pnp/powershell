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
        public GenericObjectNameIdPipeBind<TermSet> Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

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
                if (TermStore.StringValue != null)
                {
                    termStore = taxonomySession.TermStores.GetByName(TermStore.StringValue);
                }
                else if (TermStore.IdValue != Guid.Empty)
                {
                    termStore = taxonomySession.TermStores.GetById(TermStore.IdValue);
                }
                else
                {
                    if (TermStore.Item != null)
                    {
                        termStore = TermStore.Item;
                    }
                }
            }

            TermGroup termGroup = null;

            if (TermGroup.Id != Guid.Empty)
            {
                termGroup = termStore.Groups.GetById(TermGroup.Id);
            }
            else if (!string.IsNullOrEmpty(TermGroup.Name))
            {
                termGroup = termStore.Groups.GetByName(TermGroup.Name);
            }
            if (Identity != null)
            {
                var termSet = default(TermSet);
                if (Identity.IdValue != Guid.Empty)
                {
                    termSet = termGroup.TermSets.GetById(Identity.IdValue);
                }
                else
                {
                    termSet = termGroup.TermSets.GetByName(Identity.StringValue);
                }
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
