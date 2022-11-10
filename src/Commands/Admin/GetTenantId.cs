using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantId", DefaultParameterSetName = ParameterSet_FROMCONNECTION)]
    public class GetTenantId : BasePSCmdlet
    {
        private const string ParameterSet_BYURL = "By URL";
        private const string ParameterSet_FROMCONNECTION = "From connection";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYURL)]
        public string TenantUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYURL)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FROMCONNECTION, HelpMessage = "Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.")]
        public PnPConnection Connection = null;

        protected override void ProcessRecord()
        {
            // If a specific connection has been provided, use that, otherwise use the current connection
            if(Connection == null)
            {
                Connection = PnPConnection.Current;
            }

            try
            {
                if (string.IsNullOrEmpty(TenantUrl) && Connection != null)
                {
                    WriteObject(TenantExtensions.GetTenantIdByUrl(Connection.Url, Connection.AzureEnvironment));
                }
                else if (!string.IsNullOrEmpty(TenantUrl))
                {
                    if(!TenantUrl.Contains("."))
                    {
                        TenantUrl = $"https://{TenantUrl}.sharepoint.com";
                    }
                    if(!TenantUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                    {
                        TenantUrl = $"https://{TenantUrl}";
                    }
                    if(TenantUrl.IndexOf(".sharepoint.", StringComparison.InvariantCultureIgnoreCase) == -1)
                    {
                        throw new InvalidOperationException($"Please provide the sharepoint domain, e.g. contoso.sharepoint.com");    
                    }

                    WriteObject(TenantExtensions.GetTenantIdByUrl(TenantUrl, AzureEnvironment));
                }
                else
                {
                    throw new InvalidOperationException($"Either a connection needs to be made by Connect-PnPOnline, a connection needs to be provided through -{nameof(Connection)} or -{nameof(TenantUrl)} needs to be specified");
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