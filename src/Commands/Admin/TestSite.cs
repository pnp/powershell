using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsDiagnostic.Test, "PnPSite", SupportsShouldProcess = true)]
    public class TestSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public SitePipeBind Identity;

        [Parameter(Mandatory = false)]
        public Guid RuleId;

        [Parameter(Mandatory = false)]
        public SwitchParameter RunAlways;

        protected override void ExecuteCmdlet()
        {
            var siteUrl = Connection.Url;
            if (ParameterSpecified(nameof(Identity)))
            {
                siteUrl = Identity.Url;
            }

            var site = Tenant.GetSiteByUrl(siteUrl);
            AdminContext.Load(site);
            AdminContext.ExecuteQueryRetry();

            var builder = new StringBuilder();

            builder.Append($"Site {site.Url}");
            if (RuleId != Guid.Empty)
            {
                builder.Append($", RuleId {RuleId}");
            }
            if (ShouldContinue(builder.ToString(), Properties.Resources.Confirm))
            {
                var result = new PSObject();
                result.Properties.Add(new PSNoteProperty("SiteUrl", site.Url));

                var summary = site.RunHealthCheck(RuleId, false, RunAlways);
                AdminContext.Load(summary);
                AdminContext.ExecuteQueryRetry();
                var results = new List<PSObject>();
                foreach (var summaryItem in summary.Results)
                {
                    var summaryObject = new PSObject();

                    var ruleObject = new PSObject();
                    ruleObject.Properties.Add(new PSNoteProperty("Name", summaryItem.RuleName));
                    ruleObject.Properties.Add(new PSNoteProperty("Id", summaryItem.RuleId));
                    ruleObject.Properties.Add(new PSNoteProperty("HelpLink", summaryItem.RuleHelpLink));
                    ruleObject.Properties.Add(new PSNoteProperty("IsRepairable", summaryItem.RuleIsRepairable));
                    summaryObject.Properties.Add(new PSNoteProperty("Rule", ruleObject));
                    summaryObject.Properties.Add(new PSNoteProperty("TimeStamp", summaryItem.TimeStamp));
                    summaryObject.Properties.Add(new PSNoteProperty("Status", summaryItem.Status));
                    summaryObject.Properties.Add(new PSNoteProperty("Message", summaryItem.MessageAsText));
                    results.Add(summaryObject);
                }
                result.Properties.Add(new PSNoteProperty("Results", results));
                result.Properties.Add(new PSNoteProperty("PassedCount", summary.PassedCount));
                result.Properties.Add(new PSNoteProperty("FailedWarningCount", summary.FailedWarningCount));
                result.Properties.Add(new PSNoteProperty("FailedErrorCount", summary.FailedErrorCount));

                WriteObject(result);
            }
        }
    }
}