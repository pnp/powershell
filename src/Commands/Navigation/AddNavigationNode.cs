using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPNavigationNode", DefaultParameterSetName = ParameterSet_Default)]
    [OutputType(typeof(NavigationNode))]
    public class AddNavigationNode : PnPWebCmdlet
    {
        private const string ParameterSet_Default = "Default";
        private const string ParameterSet_PreviousNode = "Add node after another node";

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = true)]
        public NavigationType Location;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = false)]
        public string Url;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = false)]
        public NavigationNodePipeBind Parent;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(Mandatory = false)]
        public SwitchParameter First;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = false)]
        public List<Guid> AudienceIds;

        [Parameter(ParameterSetName = ParameterSet_Default)]
        [Parameter(ParameterSetName = ParameterSet_PreviousNode)]
        [Parameter(Mandatory = false)]
        public SwitchParameter OpenInNewTab;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_PreviousNode)]
        public NavigationNodePipeBind PreviousNode;

        protected override void ExecuteCmdlet()
        {
            if (Url == null)
            {
                ClientContext.Load(CurrentWeb, w => w.Url);
                ClientContext.ExecuteQueryRetry();
                Url = CurrentWeb.Url;
            }

            string menuNodeKey = string.Empty;
            switch (Location)
            {
                case NavigationType.QuickLaunch:
                    menuNodeKey = "1025";
                    break;
                case NavigationType.TopNavigationBar:
                    menuNodeKey = "1002";
                    break;
                case NavigationType.Footer:
                    menuNodeKey = "3a94b35f-030b-468e-80e3-b75ee84ae0ad";
                    break;
                case NavigationType.SearchNav:
                    menuNodeKey = "1040";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Location");
            }

            // Get the current menu state
            CurrentWeb.EnsureProperties(w => w.Url);
            var menuState = Utilities.REST.RestHelper.Get<Model.SharePoint.NavigationNodeCollection>(
                Connection.HttpClient,
                $"{CurrentWeb.Url}/_api/navigation/MenuState?menuNodeKey='{menuNodeKey}'",
                ClientContext.GetAccessToken(),
                false);

            if (menuState == null)
            {
                throw new Exception("Unable to retrieve current menu state.");
            }

            // Build the new node
            var newNode = new Model.SharePoint.NavigationNode
            {
                NodeType = 0,
                Title = Title,
                SimpleUrl = Url,
                FriendlyUrlSegment = "",
                IsTitleForExistingLanguage = true,
                AudienceIds = AudienceIds ?? new List<Guid>(),
                CustomProperties = new List<object>(),
                Translations = new List<object>(),  
                OpenInNewWindow = OpenInNewTab.IsPresent ? true : null,
                Nodes = new List<Model.SharePoint.NavigationNode>()
            };

            // Find where to insert the node
            List<Model.SharePoint.NavigationNode> targetNodes = menuState.Nodes;
            if (ParameterSpecified(nameof(Parent)))
            {
                var parentNode = menuState.Nodes.Select(node => SearchNodeById(node, Parent.GetNavigationNode(CurrentWeb)?.Id ?? 0))
                    .FirstOrDefault(result => result != null);
                if (parentNode == null)
                {
                    throw new Exception("Parent node not found in menu state.");
                }
                if (parentNode.Nodes == null)
                    parentNode.Nodes = new List<Model.SharePoint.NavigationNode>();
                targetNodes = parentNode.Nodes;
            }

            int insertIndex = -1;
            if (ParameterSpecified(nameof(PreviousNode)))
            {
                var prevNode = targetNodes.Select(node => SearchNodeById(node, PreviousNode.GetNavigationNode(CurrentWeb)?.Id ?? 0))
                    .FirstOrDefault(result => result != null);
                if (prevNode != null)
                {
                    insertIndex = targetNodes.IndexOf(prevNode) + 1;
                }
            }
            else if (First.IsPresent)
            {
                insertIndex = 0;
            }

            if (insertIndex >= 0 && insertIndex <= targetNodes.Count)
                targetNodes.Insert(insertIndex, newNode);
            else
                targetNodes.Add(newNode);

            // Prepare the payload
            var payload = JsonSerializer.Serialize(new { menuState });

            // Save the new menu state
            Utilities.REST.RestHelper.Post(
                Connection.HttpClient,
                $"{CurrentWeb.Url}/_api/navigation/SaveMenuState",
                ClientContext,
                 payload,
                "application/json",
                "application/json;odata=nometadata"
            );

            // Output the new node (for consistency with previous behavior)
            WriteObject(newNode);
        }

        private static Model.SharePoint.NavigationNode SearchNodeById(Model.SharePoint.NavigationNode root, int id)
        {
            if (root.Key == id.ToString())
            {
                return root;
            }

            return root.Nodes.Select(child => SearchNodeById(child, id)).FirstOrDefault(result => result != null);
        }
            // LogWarning("Something went wrong while trying to set AudienceIDs or Open in new tab property");

    }
}
