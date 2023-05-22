using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAppErrors")]
    public class GetAppErrors : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid ProductId;

        [Parameter(Mandatory = false)]
        public DateTime StartTimeInUtc;

        [Parameter(Mandatory = false)]
        public DateTime EndTimeInUtc;

        protected override void ExecuteCmdlet()
        {
            if (EndTimeInUtc == DateTime.MinValue)
            {
                EndTimeInUtc = DateTime.UtcNow;
            }
            if (StartTimeInUtc == DateTime.MinValue)
            {
                StartTimeInUtc = EndTimeInUtc.AddDays(-3.0);
            }
            if (!IsValidTime(StartTimeInUtc) || !IsValidTime(EndTimeInUtc) || EndTimeInUtc < StartTimeInUtc)
            {
                throw new PSArgumentException("Invalid Date Range");
            }
            var errorEntries = AdminContext.LoadQuery(this.Tenant.GetAppErrors(ProductId, StartTimeInUtc, EndTimeInUtc));
            AdminContext.ExecuteQueryRetry();
            WriteObject(errorEntries);
        }

        private bool IsValidTime(DateTime dt)
        {
            if (dt >= DateTime.UtcNow.AddYears(-50))
            {
                return dt <= DateTime.UtcNow.AddYears(20);
            }
            return false;
        }
    }


}