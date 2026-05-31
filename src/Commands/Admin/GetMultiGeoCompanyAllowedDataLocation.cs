using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPMultiGeoCompanyAllowedDataLocation")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(MultiGeoCompanyAllowedDataLocation))]
	public class GetMultiGeoCompanyAllowedDataLocation : PnPSharePointOnlineAdminCmdlet
	{
		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			WriteObject(multiGeoRestApiClient.GetAllowedDataLocations(), true);
		}
	}
}