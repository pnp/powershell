using System;

namespace PnP.PowerShell.Commands.Model
{
	internal class TenantRenameJobEntityData
	{
		public Guid JobId { get; set; }

		public string TargetDomainPrefix { get; set; }

		public bool SkipDomainCheck { get; set; }

		public int Option { get; set; }

		public string Reserve { get; set; }

		public bool UseV2TenantRename { get; set; }

		public bool UseV3TenantRename { get; set; }

		public string IncludeGestures { get; set; }

		public DateTime ScheduledDateTimeInUtc { get; set; }

		public DateTime RequestedAt { get; set; }
	}
}
