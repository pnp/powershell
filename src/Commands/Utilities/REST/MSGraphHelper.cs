using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base;
using HttpMethod = System.Net.Http.HttpMethod;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    internal class MSGraphHelper
    {
        private PnPGraphCmdlet Cmdlet { get; set; }
        private PnPContext Context { get; set; }
        public MSGraphHelper(PnPGraphCmdlet cmdlet)
        {
            Cmdlet = cmdlet;
            Context = cmdlet.PnPContext;
        }

        #region GET
        internal string Get(string requestUrl, ApiRequestType requestType = ApiRequestType.Graph, IDictionary<string, string> additionalHeaders = null)
        {
            if (requestUrl.StartsWith("v1.0/"))
            {
                requestUrl = requestUrl[5..];
            }
            var request = new ApiRequest(requestType, requestUrl);
            request.HttpMethod = HttpMethod.Get;
            if (additionalHeaders != null)
            {
                foreach (var kv in additionalHeaders)
                {
                    request.Headers.Remove(kv.Key);
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            try
            {
                var response = Context.Web.ExecuteRequest(request);
                return response.Response;
            }
            catch (Exception ex)
            {
                throw new PSInvalidOperationException(ex.Message);
            }
        }

        internal T Get<T>(string requestUrl, ApiRequestType requestType = ApiRequestType.Graph, IDictionary<string, string> additionalHeaders = null)
        {
            try
            {
                var response = Get(requestUrl, requestType, additionalHeaders);
                return JsonSerializer.Deserialize<T>(response);
            }
            catch (Exception ex)
            {
                throw new PSInvalidOperationException(ex.Message);
            }
        }
         #endregion
    }
   
}