﻿using System;
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
        [Obsolete("The -Web parameter will be removed in a future release. Use Connect-PnPOnline -Url [subweburl] instead to connect to a subweb.")]
        public WebPipeBind Web;

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

#pragma warning disable CS0618
            if (ParameterSpecified(nameof(Web)))
            {
                var subWeb = Web.GetWeb(ClientContext);
                subWeb.EnsureProperty(w => w.Url);
                PnPConnection.Current.CloneContext(subWeb.Url);
                web = PnPConnection.Current.Context.Web;
            }
#pragma warning restore CS0618
            else
            {
                if (PnPConnection.Current.Context.Url != PnPConnection.Current.Url)
                {
                    PnPConnection.Current.RestoreCachedContext(PnPConnection.Current.Url);
                }
                web = ClientContext.Web;
            }

            PnPConnection.Current.Context.ExecuteQueryRetry();

            return web;
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            if (PnPConnection.Current.Context.Url != PnPConnection.Current.Url)
            {
                PnPConnection.Current.RestoreCachedContext(PnPConnection.Current.Url);
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            PnPConnection.Current.CacheContext();
        }
    }
}