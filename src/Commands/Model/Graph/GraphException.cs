using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.Graph
{
    public class GraphException : Exception
    {
        public GraphError Error { get; set; }

        public string AccessToken { get; set; }

        public System.Net.Http.HttpResponseMessage HttpResponse { get; set; }
    }

    public class GraphError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public GraphError InnerError { get; set; }

        public Dictionary<string, object> AdditionalData { get; set; }

        public string ThrowSite { get; set; }
    }
}
