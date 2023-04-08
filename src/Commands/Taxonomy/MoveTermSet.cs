using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Move, "PnPTermSet")]
    public class MoveTermSet : PnPSharePointCmdlet
    {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("TermSet")]
        public TaxonomyTermSetPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermGroupPipeBind SourceTermGroup;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermGroupPipeBind DestinationTermGroup;

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

            TermGroup destinationtermGroup = DestinationTermGroup.GetGroup(termStore);
            TermGroup sourcetermGroup = SourceTermGroup.GetGroup(termStore);

            TermSet termSet = Identity.GetTermSet(sourcetermGroup);

            termSet.Move(destinationtermGroup);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
