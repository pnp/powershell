using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsLifecycle.Stop, "PnPUserAndContentMove")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(string))]
	public class StopUserAndContentMove : PnPSharePointOnlineAdminCmdlet
	{
		[Parameter(Mandatory = true, Position = 1)]
		public string UserPrincipalName { get; set; }

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			multiGeoRestApiClient.CancelUserMoveJob(UserPrincipalName);
			WriteObject("The given move job has been stopped. Please run start cmdlet to restart the move.");
		}
	}
}
