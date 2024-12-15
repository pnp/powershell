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
            
            // Validate that our ClientContext and PnPConnection are both for the same site
            if (Connection.Context.Url != Connection.Url)
            {
                // ClientContext is for a different site than our PnPConnection, try to make the connection match the ClientContext URL
                Connection.RestoreCachedContext(Connection.Context.Url);
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