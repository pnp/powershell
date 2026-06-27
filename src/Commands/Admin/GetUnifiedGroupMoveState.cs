using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPUnifiedGroupMoveState")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(PSObject))]
	public class GetUnifiedGroupMoveState : PnPSharePointOnlineAdminCmdlet
	{
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string GroupAlias { get; set; }

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			var moveState = multiGeoRestApiClient.GetUnifiedGroupMoveState(GroupAlias);
			if (moveState != null)
			{
				WriteObject(UserAndContentMoveStateFormatter.ConvertGroupMoveStateToPSObject(moveState, IsVerboseMode()));
			}
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}
	}
}
