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
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TaxonomyItemPipeBind<TermGroup> Identity;

        [Parameter(Mandatory = false)]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

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
                if (TermStore.StringValue != null)
                {
                    termStore = taxonomySession.TermStores.GetByName(TermStore.StringValue);
                }
                else if (TermStore.IdValue != Guid.Empty)
                {
                    termStore = taxonomySession.TermStores.GetById(TermStore.IdValue);
                }
                else
                {
                    if (TermStore.Item != null)
                    {
                        termStore = TermStore.Item;
                    }
                }
            }
            // Get Group
            if (termStore != null)
            {
                TermGroup group = null;
                if (Identity.Id != Guid.Empty)
                {
                    group = termStore.Groups.GetById(Identity.Id);
                }
                else
                {
                    group = termStore.Groups.GetByName(Identity.Title);
                }
                ClientContext.Load(group);
                ClientContext.ExecuteQueryRetry();

                if (group.ServerObjectIsNull.Value != false)
                {
                    bool updateRequired = false;
                    if(ParameterSpecified(nameof(Name)))
                    {
                        group.Name = Name;
                        updateRequired = true;
                    }
                    if(ParameterSpecified(nameof(Description)))
                    {
                        group.Description = Description;
                        updateRequired = true;
                    }
                    if(updateRequired)
                    {
                        termStore.CommitAll();
                        ClientContext.ExecuteQuery();
                    }
                    WriteObject(group);
                } else
                {
                    throw new PSArgumentException("Group not found");
                }
            }
            else
            {
                WriteError(new ErrorRecord(new ArgumentException("Cannot find termstore"), "INCORRECTTERMSTORE", ErrorCategory.ObjectNotFound, TermStore));
            }
        }

    }
}
