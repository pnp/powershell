using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class SearchExternalConnectionPipeBind
    {
        private readonly Model.Graph.MicrosoftSearch.ExternalConnection _searchExternalConnection;
        private readonly string _identity;

        public SearchExternalConnectionPipeBind()
        {
            _searchExternalConnection = null;
            _identity = null;
        }

        public SearchExternalConnectionPipeBind(Model.Graph.MicrosoftSearch.ExternalConnection searchExternalConnection)
        {
            _searchExternalConnection = searchExternalConnection;
        }

        public SearchExternalConnectionPipeBind(string identity)
        {
            _identity = identity;
        }

        public Model.Graph.MicrosoftSearch.ExternalConnection GetExternalConnection(PSCmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            if(_searchExternalConnection != null)
            {
                return _searchExternalConnection;
            }
            else
            {
                var externalConnectionResult = Utilities.REST.GraphHelper.Get<Model.Graph.MicrosoftSearch.ExternalConnection>(cmdlet, connection, $"v1.0/external/connections/{_identity}", accessToken);
                return externalConnectionResult;
            }
        }
    }
}
