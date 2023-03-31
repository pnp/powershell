using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPNavigationNode")]
    [OutputType(typeof(NavigationNode))]
    public class AddNavigationNode : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public NavigationType Location;

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Url;

        [Parameter(Mandatory = false)]
        public int? Parent;

        [Parameter(Mandatory = false)]
        public SwitchParameter First;

        [Parameter(Mandatory = false)]
        public SwitchParameter External;

        [Parameter(Mandatory = false)]
        public List<Guid> AudienceIds;

        protected override void ExecuteCmdlet()
        {
            if (Url == null)
            {
                ClientContext.Load(CurrentWeb, w => w.Url);
                ClientContext.ExecuteQueryRetry();
                Url = CurrentWeb.Url;
            }
            if (Parent.HasValue)
            {
                var parentNode = CurrentWeb.Navigation.GetNodeById(Parent.Value);
                ClientContext.Load(parentNode);
                ClientContext.ExecuteQueryRetry();
                var addedNode = parentNode.Children.Add(new NavigationNodeCreationInformation()
                {
                    Title = Title,
                    Url = Url,
                    IsExternal = External.IsPresent,
                    AsLastNode = !First.IsPresent
                });
                ClientContext.Load(addedNode);
                ClientContext.ExecuteQueryRetry();
                WriteObject(addedNode);
            }
            else
            {
                NavigationNodeCollection nodeCollection = null;
                if (Location == NavigationType.SearchNav)
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
                    var addedNode = nodeCollection.Add(new NavigationNodeCreationInformation
                    {
                        Title = Title,
                        Url = Url,
                        IsExternal = External.IsPresent,
                        AsLastNode = !First.IsPresent
                    });

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
                    throw new Exception("Navigation Node Collection is null");
                }
            }
        }
    }
}
