using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;


namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPNavigationNode", DefaultParameterSetName = ParameterSet_ALLBYLOCATION)]
    public class GetNavigationNode : PnPWebCmdlet
    {
        private const string ParameterSet_ALLBYLOCATION = "All nodes by location";
        private const string ParameterSet_BYID = "A single node by ID";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLBYLOCATION)]
        public NavigationType Location = NavigationType.QuickLaunch;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        public int Id;

        [Parameter(Mandatory = false)]
        public SwitchParameter Tree;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_ALLBYLOCATION)
            {
                if (Tree.IsPresent)
                {
                    NavigationNodeCollection navigationNodes = null;
                    if (Location == NavigationType.SearchNav)
                    {
                        navigationNodes = CurrentWeb.Navigation.GetNodeById(1040).Children;
                    }
                    else if (Location == NavigationType.Footer)
                    {
                        navigationNodes = CurrentWeb.LoadFooterNavigation();
                    }
                    else
                    {
                        navigationNodes = Location == NavigationType.QuickLaunch ? CurrentWeb.Navigation.QuickLaunch : CurrentWeb.Navigation.TopNavigationBar;
                    }
                    if (navigationNodes != null)
                    {
                        var nodesCollection = ClientContext.LoadQuery(navigationNodes);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(GetTree(nodesCollection, 0));
                    }
                }
                else
                {
                    NavigationNodeCollection nodes = null;
                    switch (Location)
                    {
                        case NavigationType.QuickLaunch:
                            {
                                nodes = CurrentWeb.Navigation.QuickLaunch;
                                break;
                            }
                        case NavigationType.TopNavigationBar:
                            {
                                nodes = CurrentWeb.Navigation.TopNavigationBar;
                                break;
                            }
                        case NavigationType.SearchNav:
                            {
                                nodes = CurrentWeb.Navigation.GetNodeById(1040).Children;
                                break;
                            }
                        case NavigationType.Footer:
                            {
                                nodes = CurrentWeb.LoadFooterNavigation();
                                break;
                            }
                    }
                    if (nodes != null)
                    {
                        ClientContext.Load(nodes);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(nodes, true);
                    }
                }
            }
            if (ParameterSpecified(nameof(Id)))
            {
                var node = CurrentWeb.Navigation.GetNodeById(Id);
                ClientContext.Load(node);
                ClientContext.Load(node, n => n.Children.IncludeWithDefaultProperties());
                ClientContext.ExecuteQueryRetry();
                if (Tree.IsPresent)
                {
                    WriteObject(GetTree(new List<NavigationNode>() { node }, 0));
                }
                else
                {
                    WriteObject(node);
                }
            }
        }

        private List<string> GetTree(IEnumerable<NavigationNode> nodes, int level)
        {
            var lines = new List<string>();
            var line = "";
            if (level > 0)
            {
                line = string.Join("", Enumerable.Repeat("  ", level));
            }
            var index = 1;
            foreach (var node in nodes)
            {
                if (!node.IsObjectPropertyInstantiated("Children"))
                {
                    ClientContext.Load(node.Children);
                    ClientContext.ExecuteQueryRetry();
                }

                line += "──";

                line += $" [{node.Id}] - {node.Title} - {node.Url}";
                lines.Add(line);
                if (node.Children != null && node.Children.Any())
                {
                    lines.AddRange(GetTree(node.Children.AsEnumerable(), level + 1));
                }
                index++;
                if (level > 0)
                {
                    line = string.Join("", Enumerable.Repeat("  ", level));
                }
                else
                {
                    line = "";
                }
            }
            return lines;
        }
    }
}
