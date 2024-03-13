using Microsoft.SharePoint.Client;

using System;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteVersionPolicyProgress")]
    [OutputType(typeof(PnP.PowerShell.Commands.Model.SharePoint.SetVersionPolicyProgress))]
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

            WriteObject(progress);
        }
    }
}

