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
        [Alias("GroupName")]
        public TaxonomyItemPipeBind<TermGroup> Identity;

        [Parameter(Mandatory = false)]
        [Alias("TermStoreName")]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

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
            // Get Group
            if (termStore != null)
            {

                if (Identity != null)
                {
                    TermGroup group = null;
                    if (Identity.Id != Guid.Empty)
                    {
                        group = termStore.Groups.GetById(Identity.Id);
                    }
                    else
                    {
                        group = termStore.Groups.GetByName(Identity.Title);
                    }
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
            else
            {
                WriteError(new ErrorRecord(new ArgumentException("Cannot find termstore"), "INCORRECTTERMSTORE", ErrorCategory.ObjectNotFound, TermStore));
            }
        }

    }
}
