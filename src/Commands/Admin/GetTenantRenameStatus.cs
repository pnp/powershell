using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Globalization;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantRenameStatus")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    [OutputType(typeof(TenantRenameStatus))]
    public class GetTenantRenameStatus : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
            var tenantRenameJob = multiGeoRestApiClient.GetTenantRenameJobV2();

            // An entity with neither an id nor a state is the service saying "nothing here", not a job to report
            if (tenantRenameJob == null || (tenantRenameJob.JobId == Guid.Empty && string.IsNullOrEmpty(tenantRenameJob.JobState)))
            {
                WriteError(new ErrorRecord(new PSInvalidOperationException(Properties.Resources.TenantRenameJobNotFound), "TenantRenameJobNotFound", ErrorCategory.ObjectNotFound, null));
                return;
            }

            var status = new TenantRenameStatus
            {
                RenameJobId = tenantRenameJob.JobId,
                Status = tenantRenameJob.JobState,
                RequestedAt = AsUtc(tenantRenameJob.RequestedAt),
                RequestedBy = tenantRenameJob.TriggeredBy,
                ScheduledAt = AsUtc(tenantRenameJob.ScheduledDateTimeInUtc),
                NewDomainName = tenantRenameJob.TargetDomainPrefix,
                TotalSites = tenantRenameJob.TotalSitesCount
            };

            Exception siteProgressException = null;
            if (tenantRenameJob.UseGetSpoTenantRenameStatusV2)
            {
                // The site counters returned with the job are not maintained when this flag is set; SharePoint Online keeps them per site rename job instead
                WriteVerbose("Retrieving the progress of the site renames. This could take a few minutes.");
                try
                {
                    var success = multiGeoRestApiClient.GetSiteRenameJobsCountByParentIdAndState(tenantRenameJob.JobId, SiteRenameState.Success);
                    var inProgress = multiGeoRestApiClient.GetSiteRenameJobsCountByParentIdAndState(tenantRenameJob.JobId, SiteRenameState.InProgress);
                    var suspended = multiGeoRestApiClient.GetSiteRenameJobsCountByParentIdAndState(tenantRenameJob.JobId, SiteRenameState.Suspended);
                    var failed = multiGeoRestApiClient.GetSiteRenameJobsCountByParentIdAndState(tenantRenameJob.JobId, SiteRenameState.Failed);

                    status.Renamed = success;
                    status.InProgress = inProgress;
                    status.AttentionRequired = checked(failed + suspended);
                }
                catch (Exception ex) when (ex is InvalidOperationException or HttpRequestException or TimeoutException or JsonException)
                {
                    siteProgressException = ex;
                }
            }
            else
            {
                status.Renamed = tenantRenameJob.SuccessSitesCount;
                status.InProgress = tenantRenameJob.InprogressSitesCount;
                status.AttentionRequired = checked(tenantRenameJob.FailedSitesCount + tenantRenameJob.SuspendedSitesCount);
            }

            // Write the job first, so it still reaches the pipeline when the site progress failed under -ErrorAction Stop
            WriteObject(status);

            if (siteProgressException != null)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Properties.Resources.TenantRenameSiteProgressUnavailable, siteProgressException.Message);
                WriteError(new ErrorRecord(new PSInvalidOperationException(message, siteProgressException), "TenantRenameSiteProgressUnavailable", ErrorCategory.ReadError, tenantRenameJob.JobId));
            }
        }

        private static DateTime AsUtc(DateTime value)
        {
            return value.Kind switch
            {
                DateTimeKind.Utc => value,
                DateTimeKind.Local => value.ToUniversalTime(),
                _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
            };
        }
    }
}
