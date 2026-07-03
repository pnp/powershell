using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Add, "PnPGeoAdministrator", DefaultParameterSetName = UserParameterSet)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	public class AddGeoAdministrator : PnPSharePointOnlineAdminCmdlet
	{
		private const string UserParameterSet = "User";
		private const string GroupParameterSet = "Group";
		private const string ObjectIdParameterSet = "ObjectId";

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = GroupParameterSet)]
		[ValidateNotNullOrEmpty]
		public string GroupAlias { get; set; }

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = UserParameterSet)]
		[ValidateNotNullOrEmpty]
		public string UserPrincipalName { get; set; }

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ObjectIdParameterSet)]
		[ValidateNotNullOrEmpty]
		public Guid ObjectId { get; set; }

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			switch (ParameterSetName)
			{
				case GroupParameterSet:
					multiGeoRestApiClient.AddGeoAdministrator(new GeoAdministratorEntityData
					{
						LoginName = GroupAlias,
						MemberType = GroupMemberType.Group
					});
					break;

				case UserParameterSet:
					multiGeoRestApiClient.AddGeoAdministrator(new GeoAdministratorEntityData
					{
						LoginName = UserPrincipalName,
						MemberType = GroupMemberType.User
					});
					break;

				case ObjectIdParameterSet:
					if (ObjectId == Guid.Empty)
					{
						throw new PSArgumentException("ObjectId cannot be an empty GUID.", nameof(ObjectId));
					}

					multiGeoRestApiClient.EnsureGeoAdministratorObjectIdSupported();
					multiGeoRestApiClient.AddGeoAdministrator(new GeoAdministratorEntityData
					{
						ObjectId = ObjectId
					});
					break;

				default:
					throw new ArgumentException("Parameter set cannot be resolved using the specified named parameters.");
			}
		}
	}
}
