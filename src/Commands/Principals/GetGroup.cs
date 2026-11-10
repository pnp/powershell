using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroup", DefaultParameterSetName = "All")]
    [OutputType(typeof(Group))]
    public class GetGroup : PnPWebRetrievalsCmdlet<Group>
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByName")]
        public GroupPipeBind Identity = new GroupPipeBind();

        [Parameter(Mandatory = false, ParameterSetName = "Members")]
        public SwitchParameter AssociatedMemberGroup;

        [Parameter(Mandatory = false, ParameterSetName = "Visitors")]
        public SwitchParameter AssociatedVisitorGroup;

        [Parameter(Mandatory = false, ParameterSetName = "Owners")]
        public SwitchParameter AssociatedOwnerGroup;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "ByName")
            {
                // Get group by name using Core SDK because of
                // case sensitivity difference between Core SDK and CSOM
                // Loads group using CSOM to bypass a breaking change
                var pnpGroup = Identity.GetGroup(Connection.PnPContext);

                if (pnpGroup != null)
                {
                    var csomGroup = CurrentWeb.SiteGroups.GetById(pnpGroup.Id);
                    ClientContext.Load(csomGroup);
                    ClientContext.Load(csomGroup.Users);
                    LoadRetrievals(csomGroup);
                    ClientContext.ExecuteQueryRetry();

                    WriteObject(csomGroup);
                }
                else
                {
                    throw new PSArgumentException("Site group not found", nameof(Identity));
                }

            }
            else if (ParameterSetName == "Members")
            {
                ClientContext.Load(CurrentWeb.AssociatedMemberGroup);
                ClientContext.Load(CurrentWeb.AssociatedMemberGroup.Users);
                LoadRetrievals(CurrentWeb.AssociatedMemberGroup);
                ClientContext.ExecuteQueryRetry();
                WriteObject(CurrentWeb.AssociatedMemberGroup);
            }
            else if (ParameterSetName == "Visitors")
            {
                ClientContext.Load(CurrentWeb.AssociatedVisitorGroup);
                ClientContext.Load(CurrentWeb.AssociatedVisitorGroup.Users);
                LoadRetrievals(CurrentWeb.AssociatedVisitorGroup);
                ClientContext.ExecuteQueryRetry();
                WriteObject(CurrentWeb.AssociatedVisitorGroup);
            }
            else if (ParameterSetName == "Owners")
            {
                ClientContext.Load(CurrentWeb.AssociatedOwnerGroup);
                ClientContext.Load(CurrentWeb.AssociatedOwnerGroup.Users);
                LoadRetrievals(CurrentWeb.AssociatedOwnerGroup);
                ClientContext.ExecuteQueryRetry();
                WriteObject(CurrentWeb.AssociatedOwnerGroup);
            }
            else if (ParameterSetName == "All")
            {
                // The properties which have always been returned for every group, extended with the ones requested through -Includes
                var retrievals = new Expression<Func<Group, object>>[] { g => g.Users, g => g.Title, g => g.OwnerTitle, g => g.Owner.LoginName, g => g.LoginName }.Concat(RetrievalExpressions).ToArray();

                var groups = ClientContext.LoadQuery(CurrentWeb.SiteGroups.IncludeWithDefaultProperties(retrievals));
                ClientContext.ExecuteQueryRetry();
                WriteObject(groups, true);
            }

        }

        /// <summary>
        /// Queues up the properties requested through -Includes for loading on the provided group. The caller is responsible for executing the query.
        /// </summary>
        private void LoadRetrievals(Group group)
        {
            // Loading with an empty set of retrievals would come down to a plain load, which the caller has already done, so only add a load when properties have actually been requested
            if (RetrievalExpressions.Length > 0)
            {
                ClientContext.Load(group, RetrievalExpressions);
            }
        }
    }



}
