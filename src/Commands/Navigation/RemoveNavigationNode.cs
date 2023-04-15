using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;

using PnP.PowerShell.Commands.Base.PipeBinds;
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

        [Obsolete("Use -Identity with an Id instead.")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_BYNAME)]
        public NavigationType Location;

        [Obsolete("Use -Identity with an Id instead.")]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYNAME)]
        public string Title;

        [Obsolete("Use -Identity with an Id instead.")]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYNAME)]
        public string Header;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_REMOVEALLNODES)]
        public SwitchParameter All;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_REMOVEALLNODES)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                if (Force || ShouldContinue(string.Format(Resources.RemoveNavigationNodeInLocation, Location), Resources.Confirm))
                {
                    CurrentWeb.DeleteAllNavigationNodes(Location);
                }
#pragma warning restore CS0618 // Type or member is obsolete
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
                    else
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        CurrentWeb.DeleteNavigationNode(Title, Header, Location);
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                }
            }
        }
    }
}
