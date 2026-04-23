using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.New, "PnPSiteManageVersionPolicyJob")]
    public class NewSiteManageVersionPolicyJob : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_TrimUseListPolicy = "TrimUseListPolicy";
        private const string ParameterSet_SyncListPolicy = "SyncListPolicy";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TrimUseListPolicy)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SyncListPolicy)]
        [ValidateNotNull]
        public SitePipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TrimUseListPolicy)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SyncListPolicy)]
        public string[] FileTypes;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TrimUseListPolicy)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SyncListPolicy)]
        public SwitchParameter ExcludeDefaultPolicy;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TrimUseListPolicy)]
        public SwitchParameter TrimUseListPolicy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TrimUseListPolicy)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SyncListPolicy)]
        public SwitchParameter SyncListPolicy;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var siteUrl = ResolveSiteUrl();
            var normalizedFileTypes = NormalizeFileTypes(FileTypes, nameof(FileTypes));

            if (TrimUseListPolicy && !(Force || ShouldContinue($"By executing this command, versions that are expired by the list version policies on {siteUrl} will be permanently deleted. These versions cannot be restored from the recycle bin. Are you sure you want to continue?", Resources.Confirm)))
            {
                WriteObject("Cancelled. No site manage version policy job was created.");
                return;
            }

            var fileVersionBatchDeleteParameters = new FileVersionBatchDeleteParameters
            {
                BatchDeleteMode = TrimUseListPolicy.IsPresent ? FileVersionBatchDeleteMode.ByListPolicy : FileVersionBatchDeleteMode.None,
                SyncListPolicy = SyncListPolicy.ToBool(),
                FileTypeSelections = new VersionPolicySelectionParameters
                {
                    SelectDefault = !ExcludeDefaultPolicy.ToBool(),
                    SelectAllFileTypes = normalizedFileTypes == null,
                    FileTypesSelected = normalizedFileTypes
                },
                DeleteOlderThanDays = -1,
                MajorVersionLimit = -1,
                MajorWithMinorVersionsLimit = -1
            };

            var operation = Tenant.NewFileVersionBatchDeleteJob(siteUrl, fileVersionBatchDeleteParameters);
            AdminContext.Load(operation);
            AdminContext.ExecuteQueryRetry();

            if (!NoWait.ToBool())
            {
                PollOperation(operation);
            }
        }

        private string ResolveSiteUrl()
        {
            if (Identity.Site != null)
            {
                Identity.Site.EnsureProperty(s => s.Url);
                return Identity.Site.Url.TrimEnd('/');
            }

            if (!string.IsNullOrEmpty(Identity.Url))
            {
                using var targetWebContext = Connection.CloneContext(Identity.Url.TrimEnd('/'));
                targetWebContext.Load(targetWebContext.Site, s => s.Url);
                targetWebContext.ExecuteQueryRetry();
                return targetWebContext.Site.Url.TrimEnd('/');
            }

            if (Identity.Id != Guid.Empty)
            {
                var siteProperties = Tenant.GetSitePropertiesById(Identity.Id, false, Connection.TenantAdminUrl);
                if (siteProperties == null || string.IsNullOrEmpty(siteProperties.Url))
                {
                    throw new PSArgumentException("The specified site could not be resolved.", nameof(Identity));
                }

                return siteProperties.Url.TrimEnd('/');
            }

            throw new PSArgumentException("The Identity parameter must be a valid site object, site id, or absolute site url.", nameof(Identity));
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