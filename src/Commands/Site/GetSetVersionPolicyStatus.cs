using Microsoft.SharePoint.Client;
using System;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteVersionPolicyStatus")]
    [OutputType(typeof(PnP.PowerShell.Commands.Model.SharePoint.SetVersionPolicyStatus))]
    public class GetSetVersionPolicyStatus : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Site, s => s.Url);
            var site = ClientContext.Site;
            var ret = site.GetProgressForSetVersionPolicyForDocLibs();
            ClientContext.ExecuteQueryRetry();

            var progress = JsonSerializer.Deserialize<SetVersionPolicyStatus>(ret.Value);
            progress.Url = site.Url;

            if (string.Equals(progress.LastProcessTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.LastProcessTimeInUTC = string.Empty;
            }

            if (string.Equals(progress.CompleteTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.CompleteTimeInUTC = string.Empty;
            }

            WriteObject(progress);
        }
    }
}

