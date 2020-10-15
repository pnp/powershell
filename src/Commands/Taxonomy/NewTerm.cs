using System;
using System.Collections;
using System.Globalization;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "Term")]
    public class NewTerm : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public Guid Id = Guid.Empty;

        [Parameter(Mandatory = false)]
        public int Lcid = CultureInfo.CurrentCulture.LCID;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public Hashtable CustomProperties;

        [Parameter(Mandatory = false)]
        public Hashtable LocalCustomProperties;

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
            termStore.EnsureProperty(ts => ts.DefaultLanguage);

            TermGroup termGroup = TermGroup.GetGroup(termStore);

            TermSet termSet = TermSet.GetTermSet(termGroup);

            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            var termName = TaxonomyExtensions.NormalizeName(Name);

            if(!ParameterSpecified(nameof(Lcid)))
            {
                Lcid = termStore.EnsureProperty(ts => ts.DefaultLanguage);
            }

            var term = termSet.CreateTerm(termName, Lcid, Id);
            ClientContext.Load(term);
            ClientContext.ExecuteQueryRetry();
            term.SetDescription(Description, Lcid);

            var customProperties = CustomProperties ?? new Hashtable();
            foreach (var key in customProperties.Keys)
            {
                term.SetCustomProperty(key as string, customProperties[key] as string);
            }

            var localCustomProperties = LocalCustomProperties ?? new Hashtable();
            foreach (var key in localCustomProperties.Keys)
            {
                term.SetLocalCustomProperty(key as string, localCustomProperties[key] as string);
            }
            termStore.CommitAll();
            ClientContext.Load(term);
            ClientContext.ExecuteQueryRetry();
            WriteObject(term);
        }
    }
}
