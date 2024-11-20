using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Remove, "PnPNavigationNode", DefaultParameterSetName = ParameterSet_BYID)]
    [OutputType(typeof(void))]
    public class RemoveNavigationNode : PnPWebCmdlet
    {
        private const string ParameterSet_BYNAME = "Remove node by Title";
        private const string ParameterSet_BYID = "Remove a node by ID";
        private const string ParameterSet_REMOVEALLNODES = "All Nodes";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYID)]
        public NavigationNodePipeBind Identity;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_BYNAME)]
        public NavigationType Location;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_REMOVEALLNODES)]
        public SwitchParameter All;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_REMOVEALLNODES)
            {
                if (Force || ShouldContinue(string.Format(Resources.RemoveNavigationNodeInLocation, Location), Resources.Confirm))
                {
                    CurrentWeb.DeleteAllNavigationNodes(Location);
                }
            }
            else
            {
                if (Force || ShouldContinue("Remove node?", Resources.Confirm))
                {
                    if (ParameterSetName == ParameterSet_BYID)
                    {
                        var node = Identity.GetNavigationNode(CurrentWeb);
                        node.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
            }
        }
    }
}
