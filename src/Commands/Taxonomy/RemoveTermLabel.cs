using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Remove, "PnPTermLabel", SupportsShouldProcess = true)]
    public class RemoveTermLabel : PnPSharePointCmdlet
    {
        private const string ParameterSet_BYID = "By Term Id";
        private const string ParameterSet_BYNAME = "By Termset";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYNAME)]
        public string Label;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermPipeBind Term;

        [Parameter(Mandatory = true)]
        public int Lcid;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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


            termStore.EnsureProperty(ts => ts.Languages);

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
            if (term != null)
            {
                term.EnsureProperties(t => t.Name, t => t.Id);
                if (Force || ShouldContinue($"Delete label {Label} for language {Lcid} from Term {term.Name} with id {term.Id}", Properties.Resources.Confirm))
                {
                    var labels = term.GetAllLabels(Lcid);
                    ClientContext.Load(labels);
                    ClientContext.ExecuteQueryRetry();
                    var label = labels.FirstOrDefault(l => l.Value == Label);
                    if (label != null)
                    {
                        label.DeleteObject();
                        termStore.CommitAll();
                        ClientContext.ExecuteQueryRetry();
                    }
                    else
                    {
                        throw new PSArgumentException($"Label {Label} not found for language {Lcid}");
                    }
                }
            }
        }
    }
}