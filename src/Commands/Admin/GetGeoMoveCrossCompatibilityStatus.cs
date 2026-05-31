using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPGeoMoveCrossCompatibilityStatus")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(GeoMoveTenantCompatibilityCheck))]
	public class GetGeoMoveCrossCompatibilityStatus : PnPSharePointOnlineAdminCmdlet
	{
		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			WriteObject(multiGeoRestApiClient.GetGeoMoveCompatibilityChecks(), true);
		}
	}
}