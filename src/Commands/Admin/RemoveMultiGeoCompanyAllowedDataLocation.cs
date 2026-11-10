using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Remove, "PnPMultiGeoCompanyAllowedDataLocation", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	public class RemoveMultiGeoCompanyAllowedDataLocation : PnPSharePointOnlineAdminCmdlet
	{
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Location { get; set; }

		protected override void ExecuteCmdlet()
		{
			if (!ShouldProcess(Resources.CrossGeoWarningRemoveAdl, Resources.CrossGeoWarningRemoveAdl, string.Format(CultureInfo.InvariantCulture, Resources.CrossGeoWarningRemoveAdlQuery, Location)))
			{
				if (!IsWhatIf())
				{
					WriteObject(Resources.CrossGeoWarningRemoveAdlCancelMessage);
				}

				return;
			}

			if (!ShouldProcess(Resources.CrossGeoWarning2RemoveAdl, Resources.CrossGeoWarning2RemoveAdl, Resources.CrossGeoWarning2RemoveAdlQuery))
			{
				WriteObject(Resources.CrossGeoWarningRemoveAdlCancelMessage);
				return;
			}

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			multiGeoRestApiClient.RemoveAllowedDataLocation(Location);
			WriteWarning(Resources.CrossGeoWarningRemoveAdlSuccessMessage);
		}

		private bool IsWhatIf()
		{
			return MyInvocation.BoundParameters.TryGetValue("WhatIf", out var whatIfValue) && whatIfValue is SwitchParameter whatIf && whatIf.ToBool();
		}
	}
}
