using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDActivityReportDirectoryAudit")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AuditLog.Read.All")]
    [Alias("Get-PnPAzureADActivityReportDirectoryAudit")]
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
                var auditResults = GraphRequestHelper.Get<Model.AzureAD.AzureADDirectoryAudit>(auditLogUrl);
                WriteObject(auditResults, false);
            }
            else
            {
                var auditResults = GraphRequestHelper.GetResultCollection<Model.AzureAD.AzureADDirectoryAudit>(auditLogUrl);
                WriteObject(auditResults, true);
            }
        }
    }
}
