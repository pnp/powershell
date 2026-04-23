using Microsoft.Online.SharePoint.TenantAdministration;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
	internal sealed class SiteVersionPolicyOptions
	{
		public bool InheritVersionPolicyFromTenant { get; set; }
		public bool? EnableAutoExpirationVersionTrim { get; set; }
		public int? ExpireVersionsAfterDays { get; set; }
		public int? MajorVersionLimit { get; set; }
		public int? MajorWithMinorVersionsLimit { get; set; }
		public string[] FileTypesForVersionExpiration { get; set; }
		public string[] RemoveVersionExpirationFileTypeOverride { get; set; }
		public bool ApplyToNewDocumentLibraries { get; set; }
		public bool ApplyToExistingDocumentLibraries { get; set; }
	}

	internal static class SiteVersionPolicyUtilities
	{
		public static bool HasVersionPolicyParameters(SiteVersionPolicyOptions options)
		{
			if (options == null)
			{
				return false;
			}

			return options.InheritVersionPolicyFromTenant ||
				options.EnableAutoExpirationVersionTrim.HasValue ||
				options.ExpireVersionsAfterDays.HasValue ||
				options.MajorVersionLimit.HasValue ||
				options.MajorWithMinorVersionsLimit.HasValue ||
				options.FileTypesForVersionExpiration != null ||
				options.RemoveVersionExpirationFileTypeOverride != null ||
				options.ApplyToNewDocumentLibraries ||
				options.ApplyToExistingDocumentLibraries;
		}

		public static bool ApplyToSiteProperties(SiteProperties siteProperties, SiteVersionPolicyOptions options, string siteUrl, Func<string, bool> confirmAction = null)
		{
			if (!HasVersionPolicyParameters(options))
			{
				return false;
			}

			var normalizedFileTypes = NormalizeFileTypes(options.FileTypesForVersionExpiration, nameof(options.FileTypesForVersionExpiration));
			var normalizedOverridesToRemove = NormalizeFileTypes(options.RemoveVersionExpirationFileTypeOverride, nameof(options.RemoveVersionExpirationFileTypeOverride));

			if (options.InheritVersionPolicyFromTenant)
			{
				if (options.EnableAutoExpirationVersionTrim.HasValue ||
					options.ExpireVersionsAfterDays.HasValue ||
					options.MajorVersionLimit.HasValue ||
					options.MajorWithMinorVersionsLimit.HasValue ||
					normalizedFileTypes != null ||
					normalizedOverridesToRemove != null ||
					options.ApplyToNewDocumentLibraries ||
					options.ApplyToExistingDocumentLibraries)
				{
					throw new PSArgumentException($"Don't specify other version policy related parameters when {nameof(options.InheritVersionPolicyFromTenant)} is specified.", nameof(options.InheritVersionPolicyFromTenant));
				}

				siteProperties.InheritVersionPolicyFromTenant = true;
				siteProperties.EnableAutoExpirationVersionTrim = false;
				siteProperties.ApplyToNewDocumentLibraries = false;
				siteProperties.ApplyToExistingDocumentLibraries = false;
				siteProperties.MajorVersionLimit = -1;
				siteProperties.MajorWithMinorVersionsLimit = -1;
				siteProperties.ExpireVersionsAfterDays = -1;
				return true;
			}

			if (normalizedOverridesToRemove != null)
			{
				if (options.EnableAutoExpirationVersionTrim.HasValue ||
					options.ExpireVersionsAfterDays.HasValue ||
					options.MajorVersionLimit.HasValue ||
					options.MajorWithMinorVersionsLimit.HasValue ||
					normalizedFileTypes != null ||
					options.ApplyToExistingDocumentLibraries)
				{
					throw new PSArgumentException($"Don't specify other version policy related parameters when {nameof(options.RemoveVersionExpirationFileTypeOverride)} is specified.", nameof(options.RemoveVersionExpirationFileTypeOverride));
				}

				if (!options.ApplyToNewDocumentLibraries)
				{
					throw new PSArgumentException($"You must specify {nameof(options.ApplyToNewDocumentLibraries)} when {nameof(options.RemoveVersionExpirationFileTypeOverride)} is specified.", nameof(options.ApplyToNewDocumentLibraries));
				}

				siteProperties.InheritVersionPolicyFromTenant = false;
				siteProperties.EnableAutoExpirationVersionTrim = false;
				siteProperties.ApplyToNewDocumentLibraries = true;
				siteProperties.ApplyToExistingDocumentLibraries = false;
				siteProperties.MajorVersionLimit = -1;
				siteProperties.MajorWithMinorVersionsLimit = -1;
				siteProperties.ExpireVersionsAfterDays = -1;
				siteProperties.FileTypesForVersionExpiration = null;
				siteProperties.RemoveVersionExpirationFileTypeOverride = normalizedOverridesToRemove;
				return true;
			}

			if (!options.EnableAutoExpirationVersionTrim.HasValue)
			{
				if (normalizedFileTypes != null)
				{
					throw new PSArgumentException($"The parameter {nameof(options.FileTypesForVersionExpiration)} must be combined with {nameof(options.EnableAutoExpirationVersionTrim)}.", nameof(options.FileTypesForVersionExpiration));
				}

				if (options.ExpireVersionsAfterDays.HasValue ||
					options.MajorVersionLimit.HasValue ||
					options.MajorWithMinorVersionsLimit.HasValue ||
					options.ApplyToNewDocumentLibraries ||
					options.ApplyToExistingDocumentLibraries)
				{
					throw new PSArgumentException($"You must specify {nameof(options.EnableAutoExpirationVersionTrim)} when setting site version policy parameters.", nameof(options.EnableAutoExpirationVersionTrim));
				}

				return false;
			}

			var applyToNewDocumentLibraries = options.ApplyToNewDocumentLibraries || !options.ApplyToExistingDocumentLibraries;
			var applyToExistingDocumentLibraries = options.ApplyToExistingDocumentLibraries || !options.ApplyToNewDocumentLibraries;

			if (!(confirmAction?.Invoke(GetConfirmationPrompt(siteUrl, applyToNewDocumentLibraries, applyToExistingDocumentLibraries)) ?? true))
			{
				return false;
			}

			if (applyToExistingDocumentLibraries && normalizedFileTypes != null)
			{
				throw new PSArgumentException($"The parameter {nameof(options.FileTypesForVersionExpiration)} can't be used when {nameof(options.ApplyToExistingDocumentLibraries)} is specified.", nameof(options.FileTypesForVersionExpiration));
			}

			siteProperties.InheritVersionPolicyFromTenant = false;
			siteProperties.EnableAutoExpirationVersionTrim = options.EnableAutoExpirationVersionTrim.Value;
			siteProperties.ApplyToNewDocumentLibraries = applyToNewDocumentLibraries;
			siteProperties.ApplyToExistingDocumentLibraries = applyToExistingDocumentLibraries;
			siteProperties.RemoveVersionExpirationFileTypeOverride = null;

			if (normalizedFileTypes != null)
			{
				siteProperties.FileTypesForVersionExpiration = normalizedFileTypes;
			}

			if (options.EnableAutoExpirationVersionTrim.Value)
			{
				if (options.ExpireVersionsAfterDays.HasValue)
				{
					throw new PSArgumentException($"Don't specify {nameof(options.ExpireVersionsAfterDays)} when {nameof(options.EnableAutoExpirationVersionTrim)} is true.", nameof(options.ExpireVersionsAfterDays));
				}

				if (options.MajorVersionLimit.HasValue)
				{
					throw new PSArgumentException($"Don't specify {nameof(options.MajorVersionLimit)} when {nameof(options.EnableAutoExpirationVersionTrim)} is true.", nameof(options.MajorVersionLimit));
				}

				if (options.MajorWithMinorVersionsLimit.HasValue)
				{
					throw new PSArgumentException($"Don't specify {nameof(options.MajorWithMinorVersionsLimit)} when {nameof(options.EnableAutoExpirationVersionTrim)} is true.", nameof(options.MajorWithMinorVersionsLimit));
				}

				siteProperties.ExpireVersionsAfterDays = -1;
				siteProperties.MajorVersionLimit = -1;
				siteProperties.MajorWithMinorVersionsLimit = -1;
				return true;
			}

			if (!options.ExpireVersionsAfterDays.HasValue)
			{
				throw new PSArgumentException($"You must specify {nameof(options.ExpireVersionsAfterDays)} when {nameof(options.EnableAutoExpirationVersionTrim)} is false.", nameof(options.ExpireVersionsAfterDays));
			}

			if (!options.MajorVersionLimit.HasValue)
			{
				throw new PSArgumentException($"You must specify {nameof(options.MajorVersionLimit)} when {nameof(options.EnableAutoExpirationVersionTrim)} is false.", nameof(options.MajorVersionLimit));
			}

			if (options.ExpireVersionsAfterDays.Value != 0 && (options.ExpireVersionsAfterDays.Value < 30 || options.ExpireVersionsAfterDays.Value > 36500))
			{
				throw new PSArgumentException($"{nameof(options.ExpireVersionsAfterDays)} must be 0 or between 30 and 36500.", nameof(options.ExpireVersionsAfterDays));
			}

			if (options.MajorVersionLimit.Value < 1 || options.MajorVersionLimit.Value > 50000)
			{
				throw new PSArgumentException($"{nameof(options.MajorVersionLimit)} must be between 1 and 50000.", nameof(options.MajorVersionLimit));
			}

			siteProperties.ExpireVersionsAfterDays = options.ExpireVersionsAfterDays.Value;
			siteProperties.MajorVersionLimit = options.MajorVersionLimit.Value;

			if (applyToExistingDocumentLibraries)
			{
				if (!options.MajorWithMinorVersionsLimit.HasValue)
				{
					throw new PSArgumentException($"You must specify {nameof(options.MajorWithMinorVersionsLimit)} when {nameof(options.ApplyToExistingDocumentLibraries)} is specified and {nameof(options.EnableAutoExpirationVersionTrim)} is false.", nameof(options.MajorWithMinorVersionsLimit));
				}

				if (options.MajorWithMinorVersionsLimit.Value < 0 || options.MajorWithMinorVersionsLimit.Value > 50000)
				{
					throw new PSArgumentException($"{nameof(options.MajorWithMinorVersionsLimit)} must be between 0 and 50000.", nameof(options.MajorWithMinorVersionsLimit));
				}

				siteProperties.MajorWithMinorVersionsLimit = options.MajorWithMinorVersionsLimit.Value;
			}
			else
			{
				if (options.MajorWithMinorVersionsLimit.HasValue)
				{
					throw new PSArgumentException($"Don't specify {nameof(options.MajorWithMinorVersionsLimit)} when applying version policy to new document libraries only.", nameof(options.MajorWithMinorVersionsLimit));
				}

				siteProperties.MajorWithMinorVersionsLimit = -1;
			}

			return true;
		}

		private static string GetConfirmationPrompt(string siteUrl, bool applyToNewDocumentLibraries, bool applyToExistingDocumentLibraries)
		{
			if (applyToNewDocumentLibraries && applyToExistingDocumentLibraries)
			{
				return $"Set the site version policy for new and existing document libraries on {siteUrl}?";
			}

			if (applyToNewDocumentLibraries)
			{
				return $"Set the site version policy for new document libraries on {siteUrl}?";
			}

			return $"Set the site version policy for existing document libraries on {siteUrl}?";
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