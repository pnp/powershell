using System;
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
                    var addedNode = nodeCollection.Add(new NavigationNodeCreationInformation()
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
                    throw new Exception("Navigation Node Collection is null");
                }
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}
