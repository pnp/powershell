using System;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Security.Cryptography;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

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
                ClientContext.Load(group);
                ClientContext.ExecuteQueryRetry();

                if (group.ServerObjectIsNull.Value != false)
                {
                    bool updateRequired = false;
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
                    if (updateRequired)
                    {
                        termStore.CommitAll();
                        ClientContext.ExecuteQuery();
                    }
                    WriteObject(group);
                }
                else
                {
                    throw new PSArgumentException("Group not found");
                }
            }
        }
    }
}
