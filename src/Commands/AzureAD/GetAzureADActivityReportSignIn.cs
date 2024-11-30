using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADActivityReportSignIn")]
    [RequiredApiApplicationPermissions("graph/AuditLog.Read.All")]
    [Alias("Get-PnPEntraIDActivityReportSignIn")]
    public class GetAzureADActivityReportSignIn : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public string Filter;

        protected override void ExecuteCmdlet()
        {
            var signInUrl = "/v1.0/auditLogs/signIns";

            if (!string.IsNullOrEmpty(Identity))
            {
                signInUrl += $"/{Identity}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                signInUrl += $"?$filter={Filter}";
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var auditResults = RequestHelper.Get<Model.AzureAD.AzureADSignIn>(signInUrl);
                WriteObject(auditResults, false);
            }
            else
            {
                var auditResults = RequestHelper.GetResultCollection<Model.AzureAD.AzureADSignIn>(signInUrl);
                WriteObject(auditResults, true);
            }
        }
    }
}
