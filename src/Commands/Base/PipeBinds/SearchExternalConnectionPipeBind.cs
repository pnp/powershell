using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class SearchExternalConnectionPipeBind
    {
        public readonly Model.Graph.MicrosoftSearch.ExternalConnection SearchExternalConnection;
        public readonly string Identity;

        public SearchExternalConnectionPipeBind()
        {
            SearchExternalConnection = null;
            Identity = null;
        }

        public SearchExternalConnectionPipeBind(Model.Graph.MicrosoftSearch.ExternalConnection searchExternalConnection)
        {
            SearchExternalConnection = searchExternalConnection;
        }

        public SearchExternalConnectionPipeBind(string identity)
        {
            Identity = identity;
        }

        public Model.Graph.MicrosoftSearch.ExternalConnection GetExternalConnection(PSCmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            if(SearchExternalConnection != null)
            {
                return SearchExternalConnection;
            }
            else
            {
                var externalConnectionResult = Utilities.REST.GraphHelper.Get<Model.Graph.MicrosoftSearch.ExternalConnection>(cmdlet, connection, $"v1.0/external/connections/{Identity}", accessToken);
                return externalConnectionResult;
            }
        }
    }
}
