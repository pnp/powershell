using System;

namespace PnP.PowerShell.Commands.Model
{
	internal class TenantRenameJobEntityData
	{
		public string TargetDomainPrefix { get; set; }

		public DateTime ScheduledDateTimeInUtc { get; set; }
	}
}
