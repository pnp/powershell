using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "TermSet")]
    public class NewTermSet : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public Guid Id = default(Guid);

        [Parameter(Mandatory = false)]
        public int Lcid = CultureInfo.CurrentCulture.LCID;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TermGroupPipeBind TermGroup;

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

            var termGroup = default(TermGroup);
            if (TermGroup != null)
            {
                if (!string.IsNullOrEmpty(TermGroup.Name))
                {
                    termGroup = termStore.Groups.GetByName(TermGroup.Name);
                }
                else if (TermGroup.Id != Guid.Empty)
                {
                    termGroup = termStore.Groups.GetById(TermGroup.Id);
                }
            }
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
