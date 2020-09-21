using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroup",DefaultParameterSetName="All")]
    public class GetGroup : PnPWebRetrievalsCmdlet<Group>
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByName")]
        [Alias("Name")]
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
                Group group = Identity.GetGroup(SelectedWeb);
                WriteObject(group);
            }
            else if (ParameterSetName == "Members")
            {
                ClientContext.Load(SelectedWeb.AssociatedMemberGroup);
                ClientContext.Load(SelectedWeb.AssociatedMemberGroup.Users);
                ClientContext.ExecuteQueryRetry();
                WriteObject(SelectedWeb.AssociatedMemberGroup);
            }
            else if (ParameterSetName == "Visitors")
            {
                ClientContext.Load(SelectedWeb.AssociatedVisitorGroup);
                ClientContext.Load(SelectedWeb.AssociatedVisitorGroup.Users);
                ClientContext.ExecuteQueryRetry();
                WriteObject(SelectedWeb.AssociatedVisitorGroup);
            }
            else if (ParameterSetName == "Owners")
            {
                ClientContext.Load(SelectedWeb.AssociatedOwnerGroup);
                ClientContext.Load(SelectedWeb.AssociatedOwnerGroup.Users);
                ClientContext.ExecuteQueryRetry();
                WriteObject(SelectedWeb.AssociatedOwnerGroup);
            }
            else if (ParameterSetName == "All")
            {
                var groups = ClientContext.LoadQuery(SelectedWeb.SiteGroups.IncludeWithDefaultProperties(g => g.Users));
                ClientContext.ExecuteQueryRetry();
                WriteObject(groups,true);
            }

        }
    }



}
