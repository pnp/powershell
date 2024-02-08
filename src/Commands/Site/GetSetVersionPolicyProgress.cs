using Microsoft.SharePoint.Client;

using System;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteVersionPolicyProgress")]
    [OutputType(typeof(PnP.PowerShell.Commands.Model.SharePoint.SetVersionPolicyProgressClient))]
    public class GetSetVersionPolicyProgress : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Site, s => s.Url);
            var site = ClientContext.Site;
            var ret = site.GetProgressForSetVersionPolicyForDocLibs();
            ClientContext.ExecuteQueryRetry();

            var progress = JsonSerializer.Deserialize<SetVersionPolicyProgress>(ret.Value);
            progress.Url = site.Url;

            if (string.Equals(progress.LastProcessTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.LastProcessTimeInUTC = string.Empty;
            }

            if (string.Equals(progress.CompleteTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.CompleteTimeInUTC = string.Empty;
            }

            // Display different property names in the results using Client object
            var progressClient = new SetVersionPolicyProgressClient()
            {
                Url = progress.Url,
                WorkItemId = progress.WorkItemId,
                Status = progress.Status,
                RequestTimeInUTC = progress.RequestTimeInUTC,
                LastProcessTimeInUTC = progress.LastProcessTimeInUTC,
                CompleteTimeInUTC = progress.CompleteTimeInUTC,
                LibrariesProcessedInTotal = progress.ListsProcessedInTotal,
                LibrariesFailedInTotal = progress.ListsFailedInTotal,
                EnableAutomaticMode = progress.EnableAutoTrim,
                ExpireAfterDays = progress.ExpireAfterDays,
                MajorVersionLimit = progress.MajorVersionLimit,
                MajorWithMinorVersionsLimit = progress.MajorWithMinorVersionsLimit
            };

            WriteObject(progressClient);
        }
    }
}

