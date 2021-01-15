using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantId")]
    public class GetTenantId : BasePSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string TenantUrl;

        protected override void ProcessRecord()
        {
            try
            {
                if (string.IsNullOrEmpty(TenantUrl) && PnPConnection.CurrentConnection != null)
                {
                    WriteObject(TenantExtensions.GetTenantIdByUrl(PnPConnection.CurrentConnection.Url));
                }
                else if (!string.IsNullOrEmpty(TenantUrl))
                {
                    WriteObject(TenantExtensions.GetTenantIdByUrl(TenantUrl));
                }
                else
                {
                    throw new InvalidOperationException("Either a connection needs to be made by Connect-PnPOnline or TenantUrl needs to be specified");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException is HttpRequestException)
                    {
                        var message = ex.InnerException.Message;

                        using (var jdoc = JsonDocument.Parse(message))
                        {
                            var errorDescription = jdoc.RootElement.GetProperty("error_description").GetString();
                            WriteObject(errorDescription);
                        }
                    }
                }
                throw;
            }
        }
    }
}