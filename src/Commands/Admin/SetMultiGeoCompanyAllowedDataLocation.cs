using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Set, "PnPMultiGeoCompanyAllowedDataLocation", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(string))]
	public class SetMultiGeoCompanyAllowedDataLocation : PnPSharePointOnlineAdminCmdlet
	{
		private const string SharePointAppId = "00000003-0000-0ff1-ce00-000000000000";

		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Location { get; set; }

		[Parameter(Mandatory = true, Position = 1)]
		[ValidateNotNullOrEmpty]
		public string InitialDomain { get; set; }

		protected override void ExecuteCmdlet()
		{
			var tenantName = InitialDomain.Split('.')[0];
			var mySiteHostUrl = string.Format(CultureInfo.InvariantCulture, "https://{0}-my.sharepoint.com", tenantName);
			var sharePointHostUrl = string.Format(CultureInfo.InvariantCulture, "https://{0}.sharepoint.com", tenantName);
			var warningMessage = string.Format(CultureInfo.InvariantCulture, Resources.CrossGeoWarningAddAdlMessage, mySiteHostUrl, sharePointHostUrl);

			if (!ShouldProcess(warningMessage, warningMessage, Resources.CrossGeoWarningAddAdlMessageQuery))
			{
				return;
			}

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			multiGeoRestApiClient.AddAllowedDataLocation(new MultiGeoCompanyAllowedDataLocationEntityData
			{
				AppId = SharePointAppId,
				Domain = InitialDomain,
				Location = Location
			});

			WriteObject(Resources.CrossGeoWarningAddAdlSuccessMessage);
		}
	}
}
