using System.Management.Automation;
using Microsoft.SharePoint.Client;
using web = Microsoft.SharePoint.Client.Web;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Extensions;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPWeb")]
    public class RemoveWeb : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "ByUrl")]
        public string Url;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public WebPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "ByIdentity")
            {
                web web;
                if (Identity.Id != Guid.Empty)
                {
                    web = ClientContext.Web.GetWebById(Identity.Id);
                    web.EnsureProperty(w => w.Title);
                    if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveWeb0, web.Title), Properties.Resources.Confirm))
                    {
                        web.DeleteObject();
                        web.Context.ExecuteQueryRetry();
                    }
                }
                else if (Identity.Web != null)
                {
                    Identity.Web.EnsureProperty(w => w.Title);
                    if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveWeb0, Identity.Web.Title), Properties.Resources.Confirm))
                    {
                        Identity.Web.DeleteObject();
                        Identity.Web.Context.ExecuteQueryRetry();
                    }
                }
                else if (Identity.Url != null)
                {
                    web = ClientContext.Web.GetWebByUrl(Identity.Url);
                    web.EnsureProperty(w => w.Title);
                    if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveWeb0, Identity.Web.Title), Properties.Resources.Confirm))
                    {
                        web.DeleteObject();
                        web.Context.ExecuteQueryRetry();
                    }
                }

            }
            else {
                var web = SelectedWeb.GetWeb(Url);
                web.EnsureProperty(w => w.Title);
                if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveWeb0, web.Title), Properties.Resources.Confirm))
                {
                    SelectedWeb.DeleteWeb(Url);
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}