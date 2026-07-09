using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Set, "PnPGeoStorageQuota")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	public class SetGeoStorageQuota : PnPSharePointOnlineAdminCmdlet
	{
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string GeoLocation { get; set; }

		[Parameter(Mandatory = true)]
		public long StorageQuotaMB { get; set; }

		protected override void ExecuteCmdlet()
		{
			var quota = new StorageQuotaEntityData
			{
				GeoLocation = GeoLocation,
				GeoAllocatedStorageMB = StorageQuotaMB.ToString(CultureInfo.InvariantCulture)
			};

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			multiGeoRestApiClient.PartialUpdateStorageQuota(quota);
		}
	}
}
