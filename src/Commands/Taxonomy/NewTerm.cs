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

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TaxonomyItemPipeBind<TermSet> TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public Hashtable CustomProperties;

        [Parameter(Mandatory = false)]
        public Hashtable LocalCustomProperties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [Alias("TermStoreName")]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            var termStore = default(TermStore);
            if (TermStore != null)
            {
                if (TermStore.IdValue != Guid.Empty)
                {
                    termStore = taxonomySession.TermStores.GetById(TermStore.IdValue);
                }
                else if (!string.IsNullOrEmpty(TermStore.StringValue))
                {
                    termStore = taxonomySession.TermStores.GetByName(TermStore.StringValue);
                }
                else
                {
                    termStore = TermStore.Item;
                }
            }
            else
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
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

            TermSet termSet;
            if (TermSet.Id != Guid.Empty)
            {
                termSet = termGroup.TermSets.GetById(TermSet.Id);
            }
            else if (!string.IsNullOrEmpty(TermSet.Title))
            {
                termSet = termGroup.TermSets.GetByName(TermSet.Title);
            }
            else
            {
                termSet = TermSet.Item;
            }

            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            var termName = TaxonomyExtensions.NormalizeName(Name);
            var term = termSet.CreateTerm(termName, Lcid, Id);
            ClientContext.Load(term);
            ClientContext.ExecuteQueryRetry();
            term.SetDescription(Description, Lcid);
            
            var customProperties  = CustomProperties ?? new Hashtable();
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
