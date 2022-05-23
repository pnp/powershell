using System;
using System.Management.Automation;

using System.Net;
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
            WebRequest request = WebRequest.Create(new Uri(Url) + "/_vti_bin/client.svc");
            request.Headers.Add("Authorization: Bearer ");

            try
            {
                using (request.GetResponse())
                {
                }
            }
            catch (WebException e)
            {
                var bearerResponseHeader = e.Response.Headers["WWW-Authenticate"];

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
}
