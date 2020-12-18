using System;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Extensions;


namespace PnP.PowerShell.Commands
{
    public abstract class PnPWebCmdlet : PnPSharePointCmdlet
    {
        private Web _currentWeb;

        [Parameter(Mandatory = false)]
        public WebPipeBind Web;

        protected Web CurrentWeb
        {
            get
            {
                if (_currentWeb == null)
                {
                    _currentWeb = GetWeb();
                }
                return _currentWeb;
            }
        }

        private Web GetWeb()
        {
            Web web = ClientContext.Web;

            if (ParameterSpecified(nameof(Web)))
            {
                var subWeb = Web.GetWeb(ClientContext);
                subWeb.EnsureProperty(w => w.Url);
                PnPConnection.CurrentConnection.CloneContext(subWeb.Url);
                web = PnPConnection.CurrentConnection.Context.Web;
            }
            else
            {
                if (PnPConnection.CurrentConnection.Context.Url != PnPConnection.CurrentConnection.Url)
                {
                    PnPConnection.CurrentConnection.RestoreCachedContext(PnPConnection.CurrentConnection.Url);
                }
                web = ClientContext.Web;
            }

            PnPConnection.CurrentConnection.Context.ExecuteQueryRetry();

            return web;
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            if (PnPConnection.CurrentConnection.Context.Url != PnPConnection.CurrentConnection.Url)
            {
                PnPConnection.CurrentConnection.RestoreCachedContext(PnPConnection.CurrentConnection.Url);
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            PnPConnection.CurrentConnection.CacheContext();
        }
    }
}