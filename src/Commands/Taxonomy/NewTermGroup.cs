using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

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
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false)]
        public string[] Contributors;

        [Parameter(Mandatory = false)]
        public string[] Managers;

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
            // Create Group
            var group = termStore.CreateTermGroup(Name, Id, Description);
            bool updateRequired = false;
            if (Contributors != null && Contributors.Length > 0)
            {
                foreach (var contributor in Contributors)
                {
                    group.AddContributor(contributor);
                }
                updateRequired = true;
            }
            if (Managers != null && Managers.Length > 0)
            {
                foreach (var manager in Managers)
                {
                    group.AddGroupManager(manager);
                }
                updateRequired = true;
            }

            if (updateRequired)
            {
                termStore.CommitAll();
                ClientContext.Load(group, group => group.GroupManagerPrincipalNames, group => group.ContributorPrincipalNames, group => group.Name, group => group.Description, group => group.Id);
                ClientContext.Load(termStore);
                ClientContext.ExecuteQueryRetry();

                taxonomySession.UpdateCache();
                taxonomySession.Context.ExecuteQueryRetry();
            }

            WriteObject(group);
        }

    }
}
