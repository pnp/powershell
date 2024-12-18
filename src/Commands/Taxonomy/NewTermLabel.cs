using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "PnPTermLabel")]
    public class NewTermLabel : PnPSharePointCmdlet
    {
        private const string ParameterSet_BYID = "By Term Id";
        private const string ParameterSet_BYNAME = "By Termset";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermPipeBind Term;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public int Lcid;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsDefault = true;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);

            TermStore termStore = null;
            if (TermStore == null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = TermStore.GetTermStore(taxonomySession);
            }

            termStore.EnsureProperty(ts => ts.DefaultLanguage);

            Term term = null;
            if (ParameterSetName == ParameterSet_BYID)
            {
                term = Term.GetTerm(ClientContext, termStore, null, false, null);
            }
            else
            {
                var termGroup = TermGroup.GetGroup(termStore);
                var termSet = TermSet.GetTermSet(termGroup);
                term = Term.GetTerm(ClientContext, termStore, termSet, false, null);
            }

            var label = term.CreateLabel(Name, Lcid, IsDefault.IsPresent ? IsDefault.ToBool() : false);
            ClientContext.Load(label);
            ClientContext.ExecuteQueryRetry();
            WriteObject(label);
        }
    }
}
