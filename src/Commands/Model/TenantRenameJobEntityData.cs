using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Request body for scheduling a tenant rename job. Carries the same properties, in the same order, as the
	/// SharePoint Online Management Shell sends, so the service receives an identical payload.
	/// </summary>
	internal class TenantRenameJobEntityData
	{
		public Guid JobId { get; set; }

		public string TargetDomainPrefix { get; set; }

		public bool SkipDomainCheck { get; set; }

		public MoveOption Option { get; set; }

		public string Reserve { get; set; }

		public bool UseV2TenantRename { get; set; }

		public bool UseV3TenantRename { get; set; }

		public bool IsVanity { get; set; }

		public string IncludeGestures { get; set; }

		public DateTime ScheduledDateTimeInUtc { get; set; }

		public DateTime RequestedAt { get; set; }
	}
}
