using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.REST;

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

        public string GetExternalConnectionId(GraphHelper requestHelper)
        {
            return _identity ?? _searchExternalConnection?.Id ?? GetExternalConnection(requestHelper)?.Id;
        }

        public Model.Graph.MicrosoftSearch.ExternalConnection GetExternalConnection(GraphHelper requestHelper)
        {
            if(_searchExternalConnection != null)
            {
                return _searchExternalConnection;
            }
            else
            {
                var externalConnectionResult = requestHelper.Get<Model.Graph.MicrosoftSearch.ExternalConnection>($"v1.0/external/connections/{_identity}");
                return externalConnectionResult;
            }
        }
    }
}
