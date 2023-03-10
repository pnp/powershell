using System;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    public abstract class PnPWebCmdlet : PnPSharePointCmdlet
    {
        private Web _currentWeb;

        internal void ThrowIfWebParameterUsed()
        {
#pragma warning disable CS0618

            if (ParameterSpecified(nameof(Web)))
            {
                throw new PSArgumentException("The -Web parameter is not supported in this case");
            }
#pragma warning restore CS0618
        }
        
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
            
            if (Connection.Context.Url != Connection.Url)
            {
                Connection.RestoreCachedContext(Connection.Url);
            }
            web = ClientContext.Web;            

            Connection.Context.ExecuteQueryRetry();

            return web;
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            if (Connection.Context.Url != Connection.Url)
            {
                Connection.RestoreCachedContext(Connection.Url);
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            Connection.CacheContext();
        }
    }
}