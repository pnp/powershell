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
	[OutputType(typeof(PSObject))]
	public class GetGeoMoveCrossCompatibilityStatus : PnPSharePointOnlineAdminCmdlet
	{
		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			foreach (var compatibilityCheck in multiGeoRestApiClient.GetGeoMoveCompatibilityChecks())
			{
				WriteObject(ConvertToPSObject(compatibilityCheck));
			}

		}

		private static PSObject ConvertToPSObject(GeoMoveTenantCompatibilityCheck compatibilityCheck)
		{
			var result = new PSObject();
			result.Properties.Add(new PSNoteProperty("SourceDataLocation", compatibilityCheck.SourceDataLocation));
			result.Properties.Add(new PSNoteProperty("DestinationDataLocation", compatibilityCheck.DestinationDataLocation));
			result.Properties.Add(new PSNoteProperty("CompatibilityStatus", compatibilityCheck.GeoMoveTenantCompatibilityResult));
			return result;
		}
	}
}