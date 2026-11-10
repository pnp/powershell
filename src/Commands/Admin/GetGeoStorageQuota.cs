using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPGeoStorageQuota")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(StorageQuota))]
	public class GetGeoStorageQuota : PnPSharePointOnlineAdminCmdlet
	{
		private const string LocalGeoLocation = "LOCAL";

		[Parameter(Mandatory = false)]
		public SwitchParameter AllLocations { get; set; }

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			if (AllLocations)
			{
				WriteObject(multiGeoRestApiClient.GetStorageQuotas(), true);
				return;
			}

			WriteObject(multiGeoRestApiClient.GetStorageQuotaByLocation(LocalGeoLocation));
		}
	}
}
