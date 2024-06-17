﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADActivityReportDirectoryAudit")]
    [RequiredMinimalApiPermissions("AuditLog.Read.All")]
    [Alias("Get-PnPEntraIDActivityReportDirectoryAudit")]
    public class GetAzureADActivityReportDirectoryAudit : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public string Filter;
        protected override void ExecuteCmdlet()
        {
            var auditLogUrl = "/v1.0/auditLogs/directoryaudits";

            if (!string.IsNullOrEmpty(Identity))
            {
                auditLogUrl += $"/{Identity}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                auditLogUrl += $"?$filter={Filter}";
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var auditResults = GraphHelper.GetAsync<Model.AzureAD.AzureADDirectoryAudit>(Connection, auditLogUrl, AccessToken).GetAwaiter().GetResult();
                WriteObject(auditResults, false);
            }
            else
            {
                var auditResults = GraphHelper.GetResultCollectionAsync<Model.AzureAD.AzureADDirectoryAudit>(Connection, auditLogUrl, AccessToken).GetAwaiter().GetResult();
                WriteObject(auditResults, true);
            }
        }
    }
}
