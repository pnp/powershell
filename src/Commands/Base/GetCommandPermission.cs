using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
	[Cmdlet(VerbsCommon.Get, "PnPCommandPermission")]
	[OutputType(typeof(CommandPermission))]
	public class GetCommandPermission : BasePSCmdlet
	{
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
		[ArgumentCompleter(typeof(PnPCommandNameCompleter))]
		public string CommandName { get; set; }

		protected override void ExecuteCmdlet()
		{
			var command = GetCommandTypes().FirstOrDefault(candidate => candidate.Name.Equals(CommandName, StringComparison.InvariantCultureIgnoreCase));
			if (command.Type == null)
			{
				ThrowTerminatingError(new ErrorRecord(
					new PSArgumentException($"The PnP PowerShell cmdlet '{CommandName}' was not found."),
					"CommandNotFound",
					ErrorCategory.ObjectNotFound,
					CommandName));
				return;
			}

			var delegatedAttributes = GetPermissionAttributes<RequiredApiDelegatedPermissions>(command.Type)
				.Concat(GetPermissionAttributes<RequiredApiDelegatedOrApplicationPermissions>(command.Type))
				.ToArray();
			var applicationAttributes = GetPermissionAttributes<RequiredApiApplicationPermissions>(command.Type)
				.Concat(GetPermissionAttributes<RequiredApiDelegatedOrApplicationPermissions>(command.Type))
				.ToArray();
			var hasDeclaredPermissions = delegatedAttributes.Length > 0 || applicationAttributes.Length > 0;

			var result = new CommandPermission
			{
				CommandName = command.Name,
				DelegatedAvailable = !Attribute.IsDefined(command.Type, typeof(ApiNotAvailableUnderDelegatedPermissions)),
				ApplicationAvailable = !Attribute.IsDefined(command.Type, typeof(ApiNotAvailableUnderApplicationPermissions)),
				DelegatedPermissions = ToPermissionSets(delegatedAttributes),
				ApplicationPermissions = ToPermissionSets(applicationAttributes),
				PermissionSource = hasDeclaredPermissions ? "Declared" : "Unknown"
			};

			if (!hasDeclaredPermissions)
			{
				AddSharePointGuidance(command.Type, result);
			}

			WriteObject(result);
		}

		internal static IEnumerable<(string Name, Type Type)> GetCommandTypes()
		{
			return typeof(BasePSCmdlet).Assembly.GetTypes()
				.Select(type => (Type: type, Attribute: type.GetCustomAttributes(typeof(CmdletAttribute), false).FirstOrDefault() as CmdletAttribute))
				.Where(candidate => candidate.Attribute != null)
				.Select(candidate => (Name: $"{candidate.Attribute.VerbName}-{candidate.Attribute.NounName}", candidate.Type));
		}

		private static IEnumerable<RequiredApiPermissionsBase> GetPermissionAttributes<T>(Type commandType) where T : RequiredApiPermissionsBase
		{
			return Attribute.GetCustomAttributes(commandType, typeof(T)).Cast<RequiredApiPermissionsBase>();
		}

		private static CommandPermissionSet[] ToPermissionSets(IEnumerable<RequiredApiPermissionsBase> attributes)
		{
			return attributes.Select(attribute => new CommandPermissionSet
			{
				Permissions = attribute.PermissionScopes?.Where(permission => permission != null).ToArray() ?? Array.Empty<RequiredApiPermission>()
			}).ToArray();
		}

		private static void AddSharePointGuidance(Type commandType, CommandPermission result)
		{
			if (!typeof(PnPSharePointCmdlet).IsAssignableFrom(commandType))
			{
				return;
			}

			result.PermissionSource = "Inferred";
			result.DelegatedPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, "AllSites.FullControl")];
			result.ApplicationPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, "Sites.FullControl.All")];

			if (commandType.Namespace?.Contains(".UserProfiles", StringComparison.InvariantCultureIgnoreCase) == true)
			{
				result.AdditionalRoles = ["SharePoint Administrator for tenant-wide user profile operations"];
				result.Guidance = "Suggested maximum SharePoint permissions. A lower scope can suffice for some read operations, and support for application permissions varies by user profile API.";
			}
			else if (typeof(PnPSharePointOnlineAdminCmdlet).IsAssignableFrom(commandType))
			{
				result.AdditionalRoles = ["SharePoint Administrator or Global Administrator"];
				result.Guidance = "Suggested maximum permissions for a SharePoint administration cmdlet. The operation can require a lower permission, and the signed-in user must also hold the applicable administrator role.";
			}
			else if (commandType.Namespace?.Contains(".Taxonomy", StringComparison.InvariantCultureIgnoreCase) == true)
			{
				result.AdditionalRoles = ["Term Store Administrator, Group Manager, or Contributor for write operations"];
				result.Guidance = "Suggested maximum SharePoint permissions. Read operations can require a lower scope. These cmdlets use SharePoint CSOM, so Microsoft Graph TermStore permissions do not apply.";
			}
			else
			{
				result.AdditionalRoles = ["Permissions on the target SharePoint site, web, list, or item"];
				result.Guidance = "Suggested maximum SharePoint permissions. The cmdlet can require AllSites.Read, AllSites.Write, AllSites.Manage, Sites.Read.All, Sites.ReadWrite.All, or target-specific permissions instead.";
			}
		}

		private static CommandPermissionSet CreatePermissionSet(ResourceTypeName resourceType, string scope)
		{
			return new CommandPermissionSet
			{
				Permissions = [new RequiredApiPermission(resourceType, scope)]
			};
		}
	}
}