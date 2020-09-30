using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "PnPTermGroup")]
    public class NewTermGroup : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("GroupName")]
        public string Name;

        [Parameter(Mandatory = false)]
        [Alias("GroupId")]
        public Guid Id = Guid.Empty;

        [Parameter(Mandatory = false)]
        public string Description;

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
            // Create Group
            var group = termStore.CreateTermGroup(Name, Id, Description);

            WriteObject(group);
        }

    }
}
