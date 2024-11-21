using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Set, "PnPTermGroup")]
    public class SetTermGroup : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TaxonomyTermGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public string[] Contributors { get; set; }

        [Parameter(Mandatory = false)]
        public string[] Managers { get; set; }

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
            // Get Group
            if (termStore != null)
            {
                var group = Identity.GetGroup(termStore);
                try
                {
                    ClientContext.Load(group);
                    ClientContext.ExecuteQueryRetry();
                }
                catch (Exception)
                {
                    throw new PSArgumentException("Group not found");
                }

                try
                {
                    var updateRequired = false;
                    if (ParameterSpecified(nameof(Name)))
                    {
                        group.Name = Name;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(Description)))
                    {
                        group.Description = Description;
                        updateRequired = true;
                    }
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
                catch (Exception e)
                {
                    throw new PSArgumentException(e.Message);
                }
            }
        }
    }
}
