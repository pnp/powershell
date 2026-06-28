using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Set, "PnPMultiGeoExperience", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(string))]
	public class SetMultiGeoExperience : PnPSharePointOnlineAdminCmdlet
	{
		private const string UpgradeConfirmationMessage = "This operation will upgrade your instance's multi-geo experience to include SharePoint Online Multi-Geo. This upgrade action is not reversible. Confirm that you want to continue this upgrade operation.";
		private const string UpgradeCompletedMessage = "This upgrade operation will take some time to take effect. Please run the cmdlet Get-SPOMultiGeoExperience to check the latest mode.";

		[Parameter(Mandatory = false)]
		public SwitchParameter AllInstances { get; set; }

		protected override void ExecuteCmdlet()
		{
			if (!ShouldProcess(AllInstances.ToBool() ? "all instances' multi-geo experience" : "current instance's multi-geo experience", "Upgrade to include SharePoint Online Multi-Geo"))
			{
				return;
			}

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			multiGeoRestApiClient.EnsureGeoExperienceUpgradeSupported();

			if (!ShouldContinue(UpgradeConfirmationMessage, string.Empty))
			{
				return;
			}

			multiGeoRestApiClient.UpgradeGeoExperience(AllInstances.ToBool());
			WriteObject(UpgradeCompletedMessage);
		}
	}
}
