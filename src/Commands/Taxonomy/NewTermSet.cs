using System;
using System.Collections;
using System.Globalization;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "PnPTermSet")]
    public class NewTermSet : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public Guid Id = default(Guid);

        [Parameter(Mandatory = false)]
        public int Lcid = CultureInfo.CurrentCulture.LCID;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public string Contact;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsOpenForTermCreation;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsNotAvailableForTagging;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string[] StakeHolders;

        [Parameter(Mandatory = false)]
        public Hashtable CustomProperties;

        [Parameter(Mandatory = false)]
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

            var termGroup = TermGroup.GetGroup(termStore);
            
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            var termSet = termGroup.CreateTermSet(Name, Id, Lcid);

            ClientContext.Load(termSet);
            ClientContext.ExecuteQueryRetry();

            termSet.Contact = Contact;
            termSet.Description = Description;
            termSet.IsOpenForTermCreation = IsOpenForTermCreation;

            var customProperties = CustomProperties ?? new Hashtable();
            foreach (var key in customProperties.Keys)
            {
                termSet.SetCustomProperty(key as string, customProperties[key] as string);
            }
            if (IsNotAvailableForTagging)
            {
                termSet.IsAvailableForTagging = false;
            }
            if (!string.IsNullOrEmpty(Owner))
            {
                termSet.Owner = Owner;
            }

            if (StakeHolders != null)
            {
                foreach (var stakeHolder in StakeHolders)
                {
                    termSet.AddStakeholder(stakeHolder);
                }
            }

            termStore.CommitAll();
            ClientContext.ExecuteQueryRetry();
            WriteObject(termSet);
        }

    }
}
