using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "PnPUnifiedAuditLog")]
    [RequiredMinimalApiPermissions("https://manage.office.com/ActivityFeed.Read","https://manage.office.com/ActivityFeed.ReadDlp","https://manage.office.com/ServiceHealth.Read","https://manage.office.com/ActivityReports.Read","https://manage.office.com/ThreatIntelligence.Read")]
    [OutputType(typeof(ManagementApiUnifiedLogRecord))]
    public class GetUnifiedAuditLog : PnPOfficeManagementApiCmdlet
    {
        private const string ParameterSet_LogsByDate = "Logs by date";

        [Parameter(Mandatory = false)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LogsByDate)]
        public Enums.AuditContentType ContentType = Enums.AuditContentType.SharePoint;

        [Parameter(
            Mandatory = false,
            ParameterSetName = ParameterSet_LogsByDate)]
        public DateTime StartTime = DateTime.MinValue;

        [Parameter(
            Mandatory = false,
            ParameterSetName = ParameterSet_LogsByDate)]
        public DateTime EndTime = DateTime.MaxValue;

        /// <summary>
        /// Returns the Content Type to query in a string format which is recognized by the Office 365 Management API
        /// </summary>
        protected string ContentTypeString
        {
            get
            {
                switch (ContentType)
                {
                    case Enums.AuditContentType.AzureActiveDirectory: return "Audit.AzureActiveDirectory";
                    case Enums.AuditContentType.SharePoint: return "Audit.SharePoint";
                    case Enums.AuditContentType.Exchange: return "Audit.Exchange";
                    case Enums.AuditContentType.DLP: return "DLP.All";
                    case Enums.AuditContentType.General: default: return "Audit.General";
                }
            }
        }

        /// <summary>
        /// Base URL to the Office 365 Management API being used in this cmdlet
        /// </summary>
        protected string ApiUrl => $"{ApiRootUrl}activity/feed";

        private IEnumerable<ManagementApiSubscription> GetSubscriptions()
        {
            var url = $"{ApiUrl}/subscriptions/list";
            return GraphHelper.Get<IEnumerable<ManagementApiSubscription>>(this, Connection, url, AccessToken);
            return GraphHelper.Get<IEnumerable<ManagementApiSubscription>>(this, Connection, url, AccessToken);
        }

        private void EnsureSubscription(string contentType)
        {
            var subscriptions = GetSubscriptions();
            var subscription = subscriptions.FirstOrDefault(s => s.ContentType == contentType);
            if (subscription == null)
            {
                subscription = GraphHelper.Post<ManagementApiSubscription>(this, Connection, $"{ApiUrl}/subscriptions/start?contentType={contentType}&PublisherIdentifier={TenantId}", AccessToken);
                subscription = GraphHelper.Post<ManagementApiSubscription>(this, Connection, $"{ApiUrl}/subscriptions/start?contentType={contentType}&PublisherIdentifier={TenantId}", AccessToken);
                if (!subscription.Status.Equals("enabled", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Cannot enable subscription for {contentType}");
                }
            }
        }

        protected override void ExecuteCmdlet()
        {
            EnsureSubscription(ContentTypeString);

            var url = $"{ApiUrl}/subscriptions/content?contentType={ContentTypeString}&PublisherIdentifier=${TenantId}";
            if (StartTime != DateTime.MinValue)
            {
                url += $"&startTime={StartTime:yyyy-MM-ddTHH:mm:ss}";
            }
            if (EndTime != DateTime.MaxValue)
            {
                url += $"&endTime={EndTime:yyyy-MM-ddTHH:mm:ss}";
            }

            List<ManagementApiSubscriptionContent> subscriptionContents = new();
            var subscriptionResponse = GraphHelper.GetResponse(this, Connection, url, AccessToken);
            var content = subscriptionResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (subscriptionResponse.IsSuccessStatusCode)
            {
                subscriptionContents.AddRange(collection: JsonSerializer.Deserialize<IEnumerable<ManagementApiSubscriptionContent>>(content, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                while (subscriptionResponse.Headers.Contains("NextPageUri"))
                {
                    subscriptionResponse = GraphHelper.GetResponse(this, Connection, subscriptionResponse.Headers.GetValues("NextPageUri").First(), AccessToken);
                    subscriptionResponse = GraphHelper.GetResponse(this, Connection, subscriptionResponse.Headers.GetValues("NextPageUri").First(), AccessToken);
                    if (subscriptionResponse.IsSuccessStatusCode)
                    {
                        content = subscriptionResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        subscriptionContents.AddRange(collection: JsonSerializer.Deserialize<IEnumerable<ManagementApiSubscriptionContent>>(content));
                    }
                }
            }
            else
            {
                // Request was not successful
                throw new PSInvalidOperationException($"Service responded with HTTP {(int) subscriptionResponse.StatusCode} {subscriptionResponse.ReasonPhrase}: {content}");
            }

            if (subscriptionContents.Any())
            {
                foreach (var subscriptionContent in subscriptionContents)
                {
                    var logs = GraphHelper.Get<IEnumerable<ManagementApiUnifiedLogRecord>>(this, Connection, subscriptionContent.ContentUri, AccessToken, false);
                    var logs = GraphHelper.Get<IEnumerable<ManagementApiUnifiedLogRecord>>(this, Connection, subscriptionContent.ContentUri, AccessToken, false);
                    WriteObject(logs, true);
                }
            }
        }
    }
}