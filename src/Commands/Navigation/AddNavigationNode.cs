using System;
using System.Collections.Generic;
using System.Management.Automation;
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

            var navigationNodeCreationInformation = new NavigationNodeCreationInformation {
                Title = Title,
                Url = Url,
                IsExternal = External.IsPresent,
            };

            if (ParameterSpecified(nameof(PreviousNode)))
            {
                navigationNodeCreationInformation.PreviousNode = PreviousNode.GetNavigationNode(CurrentWeb);
            } else
            {
                navigationNodeCreationInformation.AsLastNode = !First.IsPresent;
            }

            NavigationNodeCollection nodeCollection = null;
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
            else
            {
                nodeCollection = Location == NavigationType.QuickLaunch ? CurrentWeb.Navigation.QuickLaunch : CurrentWeb.Navigation.TopNavigationBar;
                ClientContext.Load(nodeCollection);
            }

            if (nodeCollection != null)
            {
                var addedNode = nodeCollection.Add(navigationNodeCreationInformation);

                if (ParameterSpecified(nameof(AudienceIds)))
                {
                    addedNode.AudienceIds = AudienceIds;
                    addedNode.Update();
                }

                ClientContext.Load(addedNode);
                ClientContext.ExecuteQueryRetry();

                if (Location == NavigationType.QuickLaunch)
                {
                    // Retrieve the menu definition and save it back again. This step is needed to enforce some properties of the menu to be shown, such as the audience targeting.
                    CurrentWeb.EnsureProperties(w => w.Url);
                    var menuState = Utilities.REST.RestHelper.GetAsync(Connection.HttpClient, $"{CurrentWeb.Url}/_api/navigation/MenuState", ClientContext, "application/json;odata=nometadata").GetAwaiter().GetResult();
                    Utilities.REST.RestHelper.PostAsync(Connection.HttpClient, $"{CurrentWeb.Url}/_api/navigation/SaveMenuState", ClientContext, @"{ ""menuState"": " + menuState + "}", "application/json", "application/json;odata=nometadata").GetAwaiter().GetResult();
                }

                WriteObject(addedNode);
            }
            else
            {
                throw new Exception("Unable to define Navigation Node collection to add the node to");
            }            
        }
    }
}
