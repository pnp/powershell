using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
	[Cmdlet(VerbsCommon.Get, "PnPListVersionPolicy")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[OutputType(typeof(ListVersionPolicy))]
	public class GetListVersionPolicy : ListVersionPolicyCmdletBase
	{
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

			var libraryParameters = new SPOListParameters
			{
				Id = list.Id,
				Title = list.Title
			};

			var fileVersionPolicyResult = Tenant.GetFileVersionPolicyForLibrary(targetSiteUrl, libraryParameters);
			AdminContext.ExecuteQueryRetry();

			if (fileVersionPolicyResult?.Value == null)
			{
				return;
			}

			var fileVersionPolicy = fileVersionPolicyResult.Value;
			var listVersionPolicy = new ListVersionPolicy
			{
				VersioningEnabled = fileVersionPolicy.VersioningEnabled,
				MinorVersionsEnabled = null,
				EnableAutoExpirationVersionTrim = null,
				ExpireVersionsAfterDays = null,
				MajorVersionLimit = null,
				MajorWithMinorVersionsLimit = null,
				FileTypesForVersionExpiration = null,
				VersionPolicyFileTypeOverride = null
			};

			if (fileVersionPolicy.VersioningEnabled)
			{
				listVersionPolicy.MinorVersionsEnabled = fileVersionPolicy.MinorVersionsEnabled;
				listVersionPolicy.EnableAutoExpirationVersionTrim = fileVersionPolicy.EnableAutoExpirationVersionTrim;
				listVersionPolicy.FileTypesForVersionExpiration = fileVersionPolicy.FileTypesForVersionExpiration;

				if (fileVersionPolicy.MinorVersionsEnabled)
				{
					listVersionPolicy.MajorWithMinorVersionsLimit = fileVersionPolicy.MajorWithMinorVersionsLimit;
				}

				if (!fileVersionPolicy.EnableAutoExpirationVersionTrim)
				{
					listVersionPolicy.ExpireVersionsAfterDays = fileVersionPolicy.ExpireVersionsAfterDays;
					listVersionPolicy.MajorVersionLimit = fileVersionPolicy.MajorVersionLimit;
				}

				listVersionPolicy.VersionPolicyFileTypeOverride = CreateFileTypeOverrides(fileVersionPolicy.VersionPolicyFileTypeOverride, fileVersionPolicy.MinorVersionsEnabled);
			}

			WriteObject(listVersionPolicy);
		}

		private static Dictionary<string, ListVersionPolicyFileTypeSettings> CreateFileTypeOverrides(SPOFileVersionFileTypePolicySettings[] fileTypeOverrides, bool minorVersionsEnabled)
		{
			var overrides = new Dictionary<string, ListVersionPolicyFileTypeSettings>(StringComparer.OrdinalIgnoreCase);

			if (fileTypeOverrides == null)
			{
				return overrides;
			}

			foreach (var fileTypeOverride in fileTypeOverrides)
			{
				overrides[fileTypeOverride.Name] = new ListVersionPolicyFileTypeSettings
				{
					Name = fileTypeOverride.Name,
					Extensions = fileTypeOverride.Extensions,
					EnableAutoExpirationVersionTrim = fileTypeOverride.EnableAutoExpirationVersionTrim,
					ExpireVersionsAfterDays = fileTypeOverride.EnableAutoExpirationVersionTrim ? null : GetExpireVersionsAfterDays(fileTypeOverride.ExpireVersionsAfter),
					MajorVersionLimit = fileTypeOverride.EnableAutoExpirationVersionTrim ? null : fileTypeOverride.MajorVersionLimit,
					MajorWithMinorVersionsLimit = minorVersionsEnabled ? fileTypeOverride.MajorWithMinorVersionsLimit : null
				};
			}

			return overrides;
		}

		private static int? GetExpireVersionsAfterDays(TimeSpan expireVersionsAfter)
		{
			return (int)Math.Round(expireVersionsAfter.TotalDays, MidpointRounding.AwayFromZero);
		}
	}
}