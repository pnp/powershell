using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
	[Cmdlet(VerbsCommon.Set, "PnPListVersionPolicy", DefaultParameterSetName = SetPolicyParameterSet)]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[OutputType(typeof(void))]
	public class SetListVersionPolicy : ListVersionPolicyCmdletBase
	{
		private const string SetPolicyParameterSet = "SetPolicy";
		private const string RemovePolicyParameterSet = "RemovePolicy";
		private const string SyncPolicyParameterSet = "SyncPolicy";

		[Parameter(Mandatory = true, ParameterSetName = SetPolicyParameterSet)]
		public bool EnableAutoExpirationVersionTrim;

		[Parameter(Mandatory = false, ParameterSetName = SetPolicyParameterSet)]
		public int? ExpireVersionsAfterDays;

		[Parameter(Mandatory = false, ParameterSetName = SetPolicyParameterSet)]
		public int? MajorVersionLimit;

		[Parameter(Mandatory = false, ParameterSetName = SetPolicyParameterSet)]
		public int? MajorWithMinorVersionsLimit;

		[Parameter(Mandatory = false, ParameterSetName = SetPolicyParameterSet)]
		[Parameter(Mandatory = false, ParameterSetName = SyncPolicyParameterSet)]
		public string[] FileTypes;

		[Parameter(Mandatory = true, ParameterSetName = SyncPolicyParameterSet)]
		public SwitchParameter Sync;

		[Parameter(Mandatory = false, ParameterSetName = SyncPolicyParameterSet)]
		public SwitchParameter ExcludeDefaultPolicy;

		[Parameter(Mandatory = false)]
		public SwitchParameter NoWait;

		[Parameter(Mandatory = true, ParameterSetName = RemovePolicyParameterSet)]
		[ValidateNotNullOrEmpty]
		public string[] RemoveVersionExpirationFileTypeOverride;

		protected override void ExecuteCmdlet()
		{
			var targetSiteUrl = GetTargetSiteUrl();
			var list = GetListOrWarn(l => l.BaseType);
			if (list is null)
			{
				return;
			}

			if (list.BaseType != BaseType.DocumentLibrary)
			{
				throw new PSArgumentException("The specified list must be a document library.", nameof(Identity));
			}

			var normalizedFileTypes = NormalizeFileTypes(FileTypes, nameof(FileTypes));
			var libraryParameters = new SPOListParameters
			{
				Id = list.Id,
				Title = list.Title
			};

			SpoOperation operation;
			switch (ParameterSetName)
			{
				case SyncPolicyParameterSet:
				{
					operation = Tenant.SyncVersionPolicyForLibrary(targetSiteUrl, libraryParameters, normalizedFileTypes, ExcludeDefaultPolicy.IsPresent);
					break;
				}
				case RemovePolicyParameterSet:
				{
					var normalizedOverridesToRemove = NormalizeFileTypes(RemoveVersionExpirationFileTypeOverride, nameof(RemoveVersionExpirationFileTypeOverride));
					operation = Tenant.RemoveFileTypeVersionPolicyForLibrary(targetSiteUrl, libraryParameters, normalizedOverridesToRemove);
					break;
				}
				default:
				{
					ValidateSetPolicyParameters();

					var versionPolicySettings = new SPOFileVersionPolicySettings
					{
						EnableAutoExpirationVersionTrim = EnableAutoExpirationVersionTrim,
						ExpireVersionsAfterDays = EnableAutoExpirationVersionTrim ? -1 : ExpireVersionsAfterDays.Value,
						MajorVersionLimit = EnableAutoExpirationVersionTrim ? -1 : MajorVersionLimit.Value,
						MajorWithMinorVersionsLimit = EnableAutoExpirationVersionTrim ? -1 : MajorWithMinorVersionsLimit.Value,
						FileTypesForVersionExpiration = normalizedFileTypes
					};

					operation = Tenant.SetFileVersionPolicyForLibrary(targetSiteUrl, libraryParameters, versionPolicySettings);
					break;
				}
			}

			AdminContext.Load(operation);
			AdminContext.ExecuteQueryRetry();

			if (!NoWait.ToBool())
			{
				PollOperation(operation);
			}
		}

		private void ValidateSetPolicyParameters()
		{
			if (EnableAutoExpirationVersionTrim)
			{
				if (ParameterSpecified(nameof(ExpireVersionsAfterDays)))
				{
					throw new PSArgumentException($"Don't specify {nameof(ExpireVersionsAfterDays)} when {nameof(EnableAutoExpirationVersionTrim)} is true.", nameof(ExpireVersionsAfterDays));
				}

				if (ParameterSpecified(nameof(MajorVersionLimit)))
				{
					throw new PSArgumentException($"Don't specify {nameof(MajorVersionLimit)} when {nameof(EnableAutoExpirationVersionTrim)} is true.", nameof(MajorVersionLimit));
				}

				if (ParameterSpecified(nameof(MajorWithMinorVersionsLimit)))
				{
					throw new PSArgumentException($"Don't specify {nameof(MajorWithMinorVersionsLimit)} when {nameof(EnableAutoExpirationVersionTrim)} is true.", nameof(MajorWithMinorVersionsLimit));
				}
			}
			else
			{
				if (!ExpireVersionsAfterDays.HasValue)
				{
					throw new PSArgumentException($"You must specify {nameof(ExpireVersionsAfterDays)} when {nameof(EnableAutoExpirationVersionTrim)} is false.", nameof(ExpireVersionsAfterDays));
				}

				if (!MajorVersionLimit.HasValue)
				{
					throw new PSArgumentException($"You must specify {nameof(MajorVersionLimit)} when {nameof(EnableAutoExpirationVersionTrim)} is false.", nameof(MajorVersionLimit));
				}

				if (!MajorWithMinorVersionsLimit.HasValue)
				{
					throw new PSArgumentException($"You must specify {nameof(MajorWithMinorVersionsLimit)} when {nameof(EnableAutoExpirationVersionTrim)} is false.", nameof(MajorWithMinorVersionsLimit));
				}

				if (ExpireVersionsAfterDays.Value != 0 && (ExpireVersionsAfterDays.Value < 30 || ExpireVersionsAfterDays.Value > 36500))
				{
					throw new PSArgumentException($"{nameof(ExpireVersionsAfterDays)} must be 0 or between 30 and 36500.", nameof(ExpireVersionsAfterDays));
				}

				if (MajorVersionLimit.Value < 1 || MajorVersionLimit.Value > 50000)
				{
					throw new PSArgumentException($"{nameof(MajorVersionLimit)} must be between 1 and 50000.", nameof(MajorVersionLimit));
				}

				if (MajorWithMinorVersionsLimit.Value < 0 || MajorWithMinorVersionsLimit.Value > 50000)
				{
					throw new PSArgumentException($"{nameof(MajorWithMinorVersionsLimit)} must be between 0 and 50000.", nameof(MajorWithMinorVersionsLimit));
				}
			}
		}

		private static string[] NormalizeFileTypes(string[] fileTypes, string parameterName)
		{
			if (fileTypes == null)
			{
				return null;
			}

			var normalizedFileTypes = fileTypes
				.Select(fileType => fileType?.Trim())
				.ToArray();

			if (normalizedFileTypes.Length == 0 || normalizedFileTypes.Any(string.IsNullOrWhiteSpace))
			{
				throw new PSArgumentException($"The parameter {parameterName} must contain one or more non-empty file types.", parameterName);
			}

			return normalizedFileTypes
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToArray();
		}
	}
}