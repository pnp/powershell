using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Remove, "PnPGeoAdministrator", DefaultParameterSetName = ParameterSetUser)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	public class RemoveGeoAdministrator : PnPSharePointOnlineAdminCmdlet
	{
		private const string ParameterSetUser = "User";
		private const string ParameterSetGroup = "Group";
		private const string ParameterSetObjectId = "ObjectId";

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetUser)]
		[ValidateNotNullOrEmpty]
		public string UserPrincipalName { get; set; }

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetGroup)]
		[ValidateNotNullOrEmpty]
		public string GroupAlias { get; set; }

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetObjectId)]
		[ValidateNotNullOrEmpty]
		public Guid ObjectId { get; set; }

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			switch (ParameterSetName)
			{
				case ParameterSetGroup:
					multiGeoRestApiClient.RemoveGeoAdministrator(GroupAlias, isGroup: true);
					break;

				case ParameterSetUser:
					multiGeoRestApiClient.RemoveGeoAdministrator(UserPrincipalName, isGroup: false);
					break;

				case ParameterSetObjectId:
					multiGeoRestApiClient.RemoveGeoAdministrator(ObjectId);
					break;

				default:
					throw new ArgumentException("Parameter set cannot be resolved using the specified named parameters.");
			}
		}
	}
}
