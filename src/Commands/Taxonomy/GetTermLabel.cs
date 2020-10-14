using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "TermLabel")]
    public class GetTermLabel : PnPSharePointCmdlet
    {
        private const string ParameterSet_BYID = "By Term Id";
        private const string ParameterSet_BYNAME = "By Termset";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermPipeBind Term;
        [Parameter(Mandatory = false)]
        public int Lcid;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
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

            termStore.EnsureProperty(ts => ts.Languages);

            Term term = null;

            if (ParameterSetName == ParameterSet_BYID)
            {
                term = Term.GetTerm(ClientContext, termStore, null, false, null);
            }
            else
            {
                TermGroup termGroup = TermGroup.GetGroup(termStore);

                TermSet termSet = TermSet.GetTermSet(termGroup);

                term = Term.GetTerm(ClientContext, termStore, termSet, false, null);

            }
            if (term != null)
            {
                if (ParameterSpecified(nameof(Lcid)))
                {
                    var languageLabels = term.GetAllLabels(Lcid);
                    ClientContext.Load(languageLabels);
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(languageLabels);
                }
                else
                {
                    List<Label> labels = new List<Label>();
                    foreach (var language in termStore.Languages)
                    {
                        var languageLabels = term.GetAllLabels(language);
                        ClientContext.Load(languageLabels);
                        ClientContext.ExecuteQueryRetry();
                        labels.AddRange(languageLabels);
                    }
                    WriteObject(labels, true);
                }
            }
        }
    }
}