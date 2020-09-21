using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.New, "PnPList")]
    public class NewList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public ListTemplateType Template;

        [Parameter(Mandatory = false)]
        public string Url = null;

        [Parameter(Mandatory = false)]
        public SwitchParameter Hidden;

        [Parameter(Mandatory = false)]
        public SwitchParameter EnableVersioning;

        [Parameter(Mandatory = false)]
        public SwitchParameter EnableContentTypes;

        [Parameter(Mandatory = false)]
        public SwitchParameter OnQuickLaunch;

        protected override void ExecuteCmdlet()
        {
            var list = SelectedWeb.CreateList(Template, Title, EnableVersioning, true, Url, EnableContentTypes, Hidden);
            if (Hidden)
            {
                SelectedWeb.DeleteNavigationNode(Title, "Recent", PnP.Framework.Enums.NavigationType.QuickLaunch);
            }
            if (OnQuickLaunch)
            {
                list.OnQuickLaunch = true;
                list.Update();
                ClientContext.ExecuteQueryRetry();
            }

            WriteObject(list);
        }
    }
}
