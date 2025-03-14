using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;

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
        public SwitchParameter External;

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

            var navigationNodeCreationInformation = new NavigationNodeCreationInformation
            {
                Title = Title,
                Url = Url,
                IsExternal = External.IsPresent,
            };

            if (ParameterSpecified(nameof(PreviousNode)))
            {
                navigationNodeCreationInformation.PreviousNode = PreviousNode.GetNavigationNode(CurrentWeb);
            }
            else
            {
                navigationNodeCreationInformation.AsLastNode = !First.IsPresent;
            }

            NavigationNodeCollection nodeCollection = null;

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

            if (ParameterSpecified(nameof(Parent)))
            {
                var parentNode = Parent.GetNavigationNode(CurrentWeb);
                nodeCollection = parentNode.Children;
                CurrentWeb.Context.Load(nodeCollection);
                CurrentWeb.Context.ExecuteQueryRetry();
            }
            else if (Location == NavigationType.SearchNav)
            {
                nodeCollection = CurrentWeb.LoadSearchNavigation();
            }
            else if (Location == NavigationType.Footer)
            {
                nodeCollection = CurrentWeb.LoadFooterNavigation();
            }
            else if(Location == NavigationType.QuickLaunch)
            {
                nodeCollection = CurrentWeb.Navigation.QuickLaunch;
                ClientContext.Load(nodeCollection);
            } 
            else
            {
                nodeCollection = CurrentWeb.Navigation.TopNavigationBar;
                ClientContext.Load(nodeCollection);
            }

            
            if (nodeCollection == null)
            {
                throw new Exception("Unable to define Navigation Node collection to add the node to");
            }

            var addedNode = nodeCollection.Add(navigationNodeCreationInformation);

            if (ParameterSpecified(nameof(AudienceIds)))
            {
                addedNode.AudienceIds = AudienceIds;
                addedNode.Update();
            }

            ClientContext.Load(addedNode);
            ClientContext.ExecuteQueryRetry();

            // Retrieve the menu definition and save it back again. This step is needed to enforce some properties of the menu to be shown, such as the audience targeting.
            CurrentWeb.EnsureProperties(w => w.Url);
            var menuState = Utilities.REST.RestHelper.Get<Model.SharePoint.NavigationNodeCollection>(Connection.HttpClient, $"{CurrentWeb.Url}/_api/navigation/MenuState?menuNodeKey='{menuNodeKey}'", ClientContext.GetAccessToken(), false);

            var currentItem = menuState?.Nodes?.Select(node => SearchNodeById(node, addedNode.Id))
                .FirstOrDefault(result => result != null);
            if (currentItem != null)
            {
                currentItem.OpenInNewWindow = OpenInNewTab.ToBool();

                if (ParameterSpecified(nameof(AudienceIds)))
                {
                    currentItem.AudienceIds = AudienceIds;
                }

                var payload = JsonSerializer.Serialize(menuState);
                Utilities.REST.RestHelper.Post(Connection.HttpClient, $"{CurrentWeb.Url}/_api/navigation/SaveMenuState", ClientContext, @"{ ""menuState"": " + payload + "}", "application/json", "application/json;odata=nometadata");
            }
            else
            {
                LogWarning("Something went wrong while trying to set AudienceIDs or Open in new tab property");
            }
            

            WriteObject(addedNode);
        }

        private static Model.SharePoint.NavigationNode SearchNodeById(Model.SharePoint.NavigationNode root, int id)
        {
            if (root.Key == id.ToString())
            {
                return root;
            }

            return root.Nodes.Select(child => SearchNodeById(child, id)).FirstOrDefault(result => result != null);
        }

    }
}
