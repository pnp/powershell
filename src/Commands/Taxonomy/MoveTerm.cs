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
    [Cmdlet(VerbsCommon.Move, "PnPTerm")]
    public class MoveTerm : PnPSharePointCmdlet
    {
        private const string ParameterSet_TERMID = "By Term Id";
        private const string ParameterSet_TERMNAME = "By Term Name";
        private const string ParameterSet_MoveToTerm = "Move To Term";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_MoveToTerm)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        [Alias("Term")]
        public TaxonomyTermPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermSetPipeBind TargetTermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermGroupPipeBind TargetTermGroup;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_MoveToTerm)]
        public TaxonomyTermPipeBind TargetTerm;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_MoveToTerm)]
        public SwitchParameter MoveToTerm;

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
            if (MoveToTerm.ToBool())
            {
                Term sourceterm = Identity.GetTerm(ClientContext, termStore, null, false, null);
                Term destinationterm = TargetTerm.GetTerm(ClientContext, termStore, null, false, null);

                sourceterm.Move(destinationterm);
                ClientContext.ExecuteQueryRetry();
            }
            else
            {
                Term term = null;
                TermSet destinationtermSet = null;
                if (ParameterSetName == ParameterSet_TERMID)
                {
                    term = Identity.GetTerm(ClientContext, termStore, null, false, null);
                    TermGroup destinationtermGroup = TargetTermGroup.GetGroup(termStore);
                    destinationtermSet = TargetTermSet.GetTermSet(destinationtermGroup);
                }
                else
                {
                    TermGroup termGroup = TermGroup.GetGroup(termStore);
                    TermSet termSet = TermSet.GetTermSet(termGroup);
                    term = Identity.GetTerm(ClientContext, termStore, termSet, false, null);
                    TermGroup destinationtermGroup = TargetTermGroup.GetGroup(termStore);
                    destinationtermSet = TargetTermSet.GetTermSet(destinationtermGroup);
                }

                term.Move(destinationtermSet);
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
