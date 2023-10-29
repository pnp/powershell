using Microsoft.SharePoint.Client;
using PnP.Framework.Http;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Rename, "PnPTenantSite")]
    public class RenameTenantSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public SPOSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string NewSiteUrl { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string NewSiteTitle { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SuppressMarketplaceAppCheck { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SuppressWorkflow2013Check { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SuppressBcsCheck { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        protected override void ExecuteCmdlet()
        {
            AdminContext.ExecuteQueryRetry(); // fixes issue where ServerLibraryVersion is not available.

            int optionsBitMask = 0;
            if (SuppressMarketplaceAppCheck.IsPresent)
            {
                optionsBitMask |= 8;
            }
            if (SuppressWorkflow2013Check.IsPresent)
            {
                optionsBitMask |= 16;
            }
            if (SuppressBcsCheck.IsPresent)
            {
                optionsBitMask |= 128;
            }

            var body = new
            {
                SourceSiteUrl = Identity.Url,
                TargetSiteUrl = NewSiteUrl,
                TargetSiteTitle = NewSiteTitle ?? null,
                Option = optionsBitMask,
                Reserve = string.Empty,
                OperationId = Guid.Empty
            };            

            var results = Utilities.REST.RestHelper.PostAsync<SPOSiteRenameJob>(HttpClient, $"{AdminContext.Url.TrimEnd('/')}/_api/SiteRenameJobs?api-version=1.4.7", AdminContext, body, false).GetAwaiter().GetResult();
            if (!Wait.IsPresent)
            {
                if (results != null)
                {
                    WriteObject(results);
                }
            }
            else
            {
                bool wait = true;
                var iterations = 0;

                var method = new HttpMethod("GET");

                var httpClient = PnPHttpClient.Instance.GetHttpClient(AdminContext);

                var requestUrl = $"{AdminContext.Url.TrimEnd('/')}/_api/SiteRenameJobs/GetJobsBySiteUrl(url='{Identity.Url}')?api-version=1.4.7";

                while (wait)
                {
                    iterations++;
                    try
                    {
                        using (HttpRequestMessage request = new HttpRequestMessage(method, requestUrl))
                        {
                            request.Headers.Add("accept", "application/json;odata=nometadata");
                            request.Headers.Add("X-AttemptNumber", iterations.ToString());
                            PnPHttpClient.AuthenticateRequestAsync(request, AdminContext).GetAwaiter().GetResult();

                            HttpResponseMessage response = httpClient.SendAsync(request, new System.Threading.CancellationToken()).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                if (responseString != null)
                                {
                                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseString);

                                    if (jsonElement.TryGetProperty("value", out JsonElement valueProperty))
                                    {
                                        var siteRenameResults = JsonSerializer.Deserialize<List<SPOSiteRenameJob>>(valueProperty.ToString());

                                        if (siteRenameResults != null && siteRenameResults.Count > 0)
                                        {
                                            var siteRenameResponse = siteRenameResults[0];
                                            if (!string.IsNullOrEmpty(siteRenameResponse.ErrorDescription))
                                            {
                                                wait = false;
                                                throw new PSInvalidOperationException(siteRenameResponse.ErrorDescription);
                                            }
                                            if (siteRenameResponse.JobState == "Success")
                                            {
                                                wait = false;
                                                WriteObject(siteRenameResponse);
                                            }
                                            else
                                            {
                                                Task.Delay(TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (iterations * 30 >= 300)
                        {
                            wait = false;
                            throw;
                        }
                        else
                        {
                            Task.Delay(TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
                        }
                    }
                }
            }
        }
    }
}
