using System;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    /// <summary>
    /// Inherit from this base class if the PowerShell commandlet should allow switching the webcontext to a subsite of the current context for the duration of the execution of the command by specifying the -Web argument
    /// </summary>
    /// <typeparam name="TType">Type of object which will be returned in the output</typeparam>
    public abstract class PnPWebRetrievalsCmdlet<TType> : PnPRetrievalsCmdlet<TType> where TType : ClientObject
    {
        private Web _currentWeb;

        [Parameter(Mandatory = false)]
        [Obsolete("The -Web parameter will be removed in a future release. Use Connect-PnPOnline -Url [subweburl] instead to connect to a subweb.")]
        public WebPipeBind Web = new WebPipeBind();

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
                Connection.CloneContext(subWeb.Url);
                web = Connection.Context.Web;
            }
#pragma warning restore CS0618
            else
            {
                if (Connection.Context.Url != Connection.Url)
                {
                    Connection.RestoreCachedContext(Connection.Url);
                }
                web = ClientContext.Web;
            }

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