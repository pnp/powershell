using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPAuthenticationRealm")]
    [OutputType(typeof(string))]
    public class GetAuthenticationRealm : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public string Url;

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Url))
            {
                Url = ClientContext.Url;
            }

            var client = Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");            
            
            var response = client.GetAsync(Url).GetAwaiter().GetResult();

            var bearerResponseHeaderValues = response.Headers.GetValues("WWW-Authenticate");
            string bearerResponseHeader = string.Join("", bearerResponseHeaderValues);
            const string bearer = "Bearer realm=\"";
            var bearerIndex = bearerResponseHeader.IndexOf(bearer, StringComparison.Ordinal);

            var realmIndex = bearerIndex + bearer.Length;
            if (bearerResponseHeader.Length >= realmIndex + 36)
            {
                var targetRealm = bearerResponseHeader.Substring(realmIndex, 36);

                Guid realmGuid;

                if (Guid.TryParse(targetRealm, out realmGuid))
                {
                    WriteObject(targetRealm);
                }
            }
        }
    }
}

