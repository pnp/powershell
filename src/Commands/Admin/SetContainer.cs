using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainer", DefaultParameterSetName = ParameterSet_InformationBarriersMode, SupportsShouldProcess = true)]
    public class SetContainer : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_InformationBarriersMode = "Information barriers mode";
        private const string ParameterSet_SensitivityLabel = "Sensitivity label";
        private const string ParameterSet_RemoveLabel = "Remove sensitivity label";
        private const string ParameterSet_RestrictContentOrgWideSearch = "Restrict content org wide search";
        private const string ParameterSet_BlockDownloadPolicy = "Block download policy";
        private const string ParameterSet_RestrictedAccessControl = "Restricted access control";
        private const string ParameterSet_RestrictedAccessControlGroupsToAdd = "Add restricted access control groups";
        private const string ParameterSet_RestrictedAccessControlGroupsToRemove = "Remove restricted access control groups";
        private const string ParameterSet_ClearRestrictedAccessControl = "Clear restricted access control";
        private const string ParameterSet_ConditionalAccess = "Conditional access";
        private const string ParameterSet_SharingDomainRestriction = "Sharing domain restriction";
        private const string ParameterSet_PrincipalOwnerTransfer = "Principal owner transfer";
        private const string ParameterSet_AddInformationBarrierSegments = "Add information barrier segments";
        private const string ParameterSet_RemoveInformationBarrierSegments = "Remove information barrier segments";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SensitivityLabel)]
        [ValidateNotNullOrWhiteSpace]
        public string SensitivityLabel { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_RemoveLabel)]
        public SwitchParameter RemoveLabel { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_RestrictContentOrgWideSearch)]
        public bool RestrictContentOrgWideSearch { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BlockDownloadPolicy)]
        public bool BlockDownloadPolicy { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BlockDownloadPolicy)]
        public bool ExcludeBlockDownloadPolicyContainerOwners { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BlockDownloadPolicy)]
        public bool ReadOnlyForBlockDownloadPolicy { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_RestrictedAccessControl)]
        public bool EnableRestrictedAccessControl { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_RestrictedAccessControl)]
        [ValidateNotNullOrEmpty]
        public Guid[] RestrictedAccessControlGroups { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_RestrictedAccessControlGroupsToAdd)]
        [ValidateNotNullOrEmpty]
        public Guid[] RestrictedAccessControlGroupsToAdd { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_RestrictedAccessControlGroupsToRemove)]
        [ValidateNotNullOrEmpty]
        public Guid[] RestrictedAccessControlGroupsToRemove { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ClearRestrictedAccessControl)]
        public SwitchParameter ClearRestrictedAccessControl { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public SPOConditionalAccessPolicyType ConditionalAccessPolicy { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public SPOLimitedAccessFileType LimitedAccessFileType { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public bool AllowEditing { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public bool ReadOnlyForUnmanagedDevices { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public string AuthenticationContextName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ConditionalAccess)]
        public SwitchParameter Force { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SharingDomainRestriction)]
        public SharingDomainRestrictionModes SharingDomainRestrictionMode { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SharingDomainRestriction)]
        public string SharingAllowedDomainList { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SharingDomainRestriction)]
        public string SharingBlockedDomainList { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_PrincipalOwnerTransfer)]
        [ValidateNotNullOrWhiteSpace]
        public string CurrentPrincipalOwner { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_PrincipalOwnerTransfer)]
        [ValidateNotNullOrWhiteSpace]
        public string NewPrincipalOwner { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AddInformationBarrierSegments)]
        [ValidateNotNullOrEmpty]
        public Guid[] AddInformationSegment { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_RemoveInformationBarrierSegments)]
        [ValidateNotNullOrEmpty]
        public Guid[] RemoveInformationSegment { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrWhiteSpace]
        public string InformationBarriersMode { get; set; }

        protected override void ExecuteCmdlet()
        {
            if ((ParameterSetName == ParameterSet_InformationBarriersMode && !ParameterSpecified(nameof(InformationBarriersMode)))
                || (ParameterSetName == ParameterSet_RemoveLabel && !RemoveLabel)
                || (ParameterSetName == ParameterSet_ClearRestrictedAccessControl && !ClearRestrictedAccessControl))
            {
                ThrowArgumentError("Specify at least one setting to change.", null);
            }

            // Always read the container from the server: the whole properties object is written back, so a container object passed in,
            // possibly modified by an earlier call, would otherwise resend stale values or one-off instructions such as an owner transfer
            var container = Identity.GetContainer(Tenant, refresh: true);
            if (container == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException(Properties.Resources.ContainerNotFound, nameof(Identity)), "ContainerNotFound", ErrorCategory.ObjectNotFound, Identity.Id ?? Identity.Url));
            }

            if (!string.IsNullOrWhiteSpace(Identity.Id))
            {
                container.ContainerId = Identity.Id;
            }
            if (!string.IsNullOrWhiteSpace(Identity.Url))
            {
                container.ContainerSiteUrl = Identity.Url;
            }

            if (ParameterSpecified(nameof(SensitivityLabel)))
            {
                container.SensitivityLabel = SensitivityLabel;
            }

            if (RemoveLabel)
            {
                container.SensitivityLabel = null;
            }

            if (ParameterSpecified(nameof(RestrictContentOrgWideSearch)))
            {
                container.RestrictContentOrgWideSearch = RestrictContentOrgWideSearch;
            }

            if (ParameterSetName == ParameterSet_BlockDownloadPolicy)
            {
                ApplyBlockDownloadPolicy(container);
            }

            SPOConditionalAccessPolicyType? conditionalAccessPolicy = null;
            var switchesToLimitedAccess = false;
            if (ParameterSetName == ParameterSet_ConditionalAccess)
            {
                conditionalAccessPolicy = ResolveConditionalAccessPolicy(container, out switchesToLimitedAccess);
            }

            if (ParameterSetName == ParameterSet_SharingDomainRestriction)
            {
                ApplySharingDomainRestriction(container);
            }

            if (ParameterSetName == ParameterSet_PrincipalOwnerTransfer)
            {
                container.TransferFromPrincipalOwnerIdentifier = CurrentPrincipalOwner;
                container.NewPrincipalOwnerIdentifier = NewPrincipalOwner;
            }

            ApplyRestrictedAccessControl(container);

            if (ParameterSpecified(nameof(AddInformationSegment)))
            {
                container.IBSegmentsToAdd = AddInformationSegment;
            }

            if (ParameterSpecified(nameof(RemoveInformationSegment)))
            {
                container.IBSegmentsToRemove = RemoveInformationSegment;
            }

            if (ParameterSpecified(nameof(InformationBarriersMode)))
            {
                container.IBModeToSet = InformationBarriersMode;
            }

            if (!ShouldProcess(container.ContainerId, "Set container properties"))
            {
                return;
            }

            if (switchesToLimitedAccess && !Force && !ShouldContinue($"-{nameof(LimitedAccessFileType)}, -{nameof(AllowEditing)} and -{nameof(ReadOnlyForUnmanagedDevices)} require the conditional access policy of the container to be {SPOConditionalAccessPolicyType.AllowLimitedAccess}. Set it to {SPOConditionalAccessPolicyType.AllowLimitedAccess}?", Properties.Resources.Confirm))
            {
                return;
            }

            if (ParameterSetName == ParameterSet_ConditionalAccess)
            {
                ApplyConditionalAccessPolicy(container, conditionalAccessPolicy);
            }

            Tenant.SetSPOContainerProperties(container);
            AdminContext.ExecuteQueryRetry();
        }

        private void ApplyBlockDownloadPolicy(SPContainerProperties container)
        {
            var optionSpecified = ParameterSpecified(nameof(ExcludeBlockDownloadPolicyContainerOwners)) || ParameterSpecified(nameof(ReadOnlyForBlockDownloadPolicy));
            if (!ParameterSpecified(nameof(BlockDownloadPolicy)) || !BlockDownloadPolicy)
            {
                if (optionSpecified)
                {
                    ThrowArgumentError($"-{nameof(ExcludeBlockDownloadPolicyContainerOwners)} and -{nameof(ReadOnlyForBlockDownloadPolicy)} can only be used together with -{nameof(BlockDownloadPolicy)} $true.", null);
                }
                if (!ParameterSpecified(nameof(BlockDownloadPolicy)))
                {
                    return;
                }
            }

            container.ExcludeBlockDownloadPolicyContainerOwners = BlockDownloadPolicy && ExcludeBlockDownloadPolicyContainerOwners;
            container.ReadOnlyForBlockDownloadPolicy = BlockDownloadPolicy && ReadOnlyForBlockDownloadPolicy;
            container.BlockDownloadPolicy = BlockDownloadPolicy;
        }

        // Validates the conditional access parameters and works out the policy to set. Switching a container to AllowLimitedAccess
        // because a limited access setting was passed without -ConditionalAccessPolicy is confirmed by the caller, after ShouldProcess.
        private SPOConditionalAccessPolicyType? ResolveConditionalAccessPolicy(SPContainerProperties container, out bool switchesToLimitedAccess)
        {
            switchesToLimitedAccess = false;
            SPOConditionalAccessPolicyType? policy = ParameterSpecified(nameof(ConditionalAccessPolicy)) ? ConditionalAccessPolicy : null;

            var limitedAccessSettingSpecified = ParameterSpecified(nameof(LimitedAccessFileType)) || ParameterSpecified(nameof(AllowEditing)) || ParameterSpecified(nameof(ReadOnlyForUnmanagedDevices));
            if (!policy.HasValue && !limitedAccessSettingSpecified && string.IsNullOrWhiteSpace(AuthenticationContextName))
            {
                ThrowArgumentError("Specify at least one setting to change.", null);
            }

            if (limitedAccessSettingSpecified)
            {
                if (policy.HasValue && policy != SPOConditionalAccessPolicyType.AllowLimitedAccess)
                {
                    ThrowArgumentError($"-{nameof(LimitedAccessFileType)}, -{nameof(AllowEditing)} and -{nameof(ReadOnlyForUnmanagedDevices)} can only be used with -{nameof(ConditionalAccessPolicy)} {SPOConditionalAccessPolicyType.AllowLimitedAccess}.", nameof(ConditionalAccessPolicy));
                }
                if (!policy.HasValue)
                {
                    switchesToLimitedAccess = container.ConditionalAccessPolicy != SPOConditionalAccessPolicyType.AllowLimitedAccess;
                    policy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                }
            }

            if (!string.IsNullOrWhiteSpace(AuthenticationContextName) && policy != SPOConditionalAccessPolicyType.AuthenticationContext)
            {
                ThrowArgumentError($"-{nameof(AuthenticationContextName)} can only be used with -{nameof(ConditionalAccessPolicy)} {SPOConditionalAccessPolicyType.AuthenticationContext}.", nameof(AuthenticationContextName));
            }
            return policy;
        }

        private void ApplyConditionalAccessPolicy(SPContainerProperties container, SPOConditionalAccessPolicyType? policy)
        {
            if (policy.HasValue)
            {
                container.ConditionalAccessPolicy = policy.Value;
                if (policy != SPOConditionalAccessPolicyType.AuthenticationContext)
                {
                    container.AuthenticationContextName = null;
                }
                else if (ParameterSpecified(nameof(AuthenticationContextName)))
                {
                    container.AuthenticationContextName = string.IsNullOrWhiteSpace(AuthenticationContextName) ? null : AuthenticationContextName;
                }
                if (policy == SPOConditionalAccessPolicyType.AllowFullAccess)
                {
                    container.AllowEditing = true;
                }
                if (policy != SPOConditionalAccessPolicyType.AllowLimitedAccess)
                {
                    container.ReadOnlyForUnmanagedDevices = false;
                }
            }

            if (ParameterSpecified(nameof(LimitedAccessFileType)))
            {
                container.LimitedAccessFileType = LimitedAccessFileType;
            }
            if (ParameterSpecified(nameof(AllowEditing)))
            {
                container.AllowEditing = AllowEditing;
            }
            if (ParameterSpecified(nameof(ReadOnlyForUnmanagedDevices)))
            {
                container.ReadOnlyForUnmanagedDevices = ReadOnlyForUnmanagedDevices;
            }
        }

        private void ApplySharingDomainRestriction(SPContainerProperties container)
        {
            if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.None)
            {
                if (!string.IsNullOrWhiteSpace(SharingAllowedDomainList) || !string.IsNullOrWhiteSpace(SharingBlockedDomainList))
                {
                    ThrowArgumentError($"-{nameof(SharingDomainRestrictionMode)} {SharingDomainRestrictionModes.None} removes the domain lists and cannot be combined with them.", nameof(SharingDomainRestrictionMode));
                }
                container.SharingDomainRestrictionMode = SharingDomainRestrictionModes.None;
                container.SharingAllowedDomainList = null;
                container.SharingBlockedDomainList = null;
                return;
            }

            var allowList = SharingDomainRestrictionMode == SharingDomainRestrictionModes.AllowList;
            var domainList = allowList ? SharingAllowedDomainList : SharingBlockedDomainList;
            var listParameterName = allowList ? nameof(SharingAllowedDomainList) : nameof(SharingBlockedDomainList);
            var otherList = allowList ? SharingBlockedDomainList : SharingAllowedDomainList;
            if (string.IsNullOrWhiteSpace(domainList) || otherList != null)
            {
                ThrowArgumentError($"-{nameof(SharingDomainRestrictionMode)} {SharingDomainRestrictionMode} requires -{listParameterName}, and only that list.", listParameterName);
            }

            var domains = domainList.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
            if (domains.Length == 0)
            {
                ThrowArgumentError($"-{listParameterName} does not contain any domains.", listParameterName);
            }

            AdminContext.Load(Tenant, t => t.SharingDomainRestrictionMode, t => t.SharingAllowedDomainList, t => t.SharingBlockedDomainList);
            AdminContext.ExecuteQueryRetry();
            if (!IsDomainListAllowedByTenant(domains))
            {
                ThrowArgumentError($"The domains in -{listParameterName} conflict with the sharing domain restriction of the tenant, which is set to {Tenant.SharingDomainRestrictionMode}.", listParameterName);
            }

            container.SharingDomainRestrictionMode = SharingDomainRestrictionMode;
            if (allowList)
            {
                container.SharingAllowedDomainList = string.Join(",", domains);
            }
            else
            {
                container.SharingBlockedDomainList = string.Join(",", domains);
            }
        }

        // A container may only narrow the sharing domain restriction of the tenant, never widen it
        private bool IsDomainListAllowedByTenant(string[] domains)
        {
            switch (Tenant.SharingDomainRestrictionMode)
            {
                case SharingDomainRestrictionModes.AllowList:
                    if (SharingDomainRestrictionMode != SharingDomainRestrictionModes.AllowList || string.IsNullOrWhiteSpace(Tenant.SharingAllowedDomainList))
                    {
                        return false;
                    }
                    var tenantAllowedDomains = Tenant.SharingAllowedDomainList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    return domains.All(domain => tenantAllowedDomains.Contains(domain, StringComparer.OrdinalIgnoreCase));

                case SharingDomainRestrictionModes.BlockList:
                    if (SharingDomainRestrictionMode != SharingDomainRestrictionModes.AllowList || string.IsNullOrWhiteSpace(Tenant.SharingBlockedDomainList))
                    {
                        return true;
                    }
                    var tenantBlockedDomains = Tenant.SharingBlockedDomainList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    return !domains.Any(domain => tenantBlockedDomains.Contains(domain, StringComparer.OrdinalIgnoreCase));

                default:
                    return true;
            }
        }

        private void ApplyRestrictedAccessControl(SPContainerProperties container)
        {
            if (ClearRestrictedAccessControl)
            {
                container.ClearRestrictedAccessControl = true;
            }

            var groupsSpecified = ParameterSpecified(nameof(RestrictedAccessControlGroups)) || ParameterSpecified(nameof(RestrictedAccessControlGroupsToAdd)) || ParameterSpecified(nameof(RestrictedAccessControlGroupsToRemove));
            if (ParameterSpecified(nameof(EnableRestrictedAccessControl)))
            {
                if (!EnableRestrictedAccessControl && ParameterSpecified(nameof(RestrictedAccessControlGroups)))
                {
                    ThrowArgumentError($"-{nameof(RestrictedAccessControlGroups)} cannot be used with -{nameof(EnableRestrictedAccessControl)} $false.", nameof(RestrictedAccessControlGroups));
                }
                container.EnableRestrictedAccessControl = EnableRestrictedAccessControl;
            }
            else if (groupsSpecified && !container.EnableRestrictedAccessControl)
            {
                ThrowArgumentError($"Restricted access control is not enabled on this container. Enable it with -{nameof(EnableRestrictedAccessControl)} $true before changing its groups.", nameof(EnableRestrictedAccessControl));
            }

            if (!container.EnableRestrictedAccessControl)
            {
                return;
            }

            if (RestrictedAccessControlGroups != null && RestrictedAccessControlGroups.Length > 0)
            {
                container.RestrictedAccessControlGroups = RestrictedAccessControlGroups;
            }
            if (RestrictedAccessControlGroupsToAdd != null)
            {
                container.RestrictedAccessControlGroupsToAdd = RestrictedAccessControlGroupsToAdd;
            }
            if (RestrictedAccessControlGroupsToRemove != null)
            {
                container.RestrictedAccessControlGroupsToRemove = RestrictedAccessControlGroupsToRemove;
            }
        }

        private void ThrowArgumentError(string message, string parameterName)
        {
            ThrowTerminatingError(new ErrorRecord(new PSArgumentException(message, parameterName), "InvalidContainerSettings", ErrorCategory.InvalidArgument, Identity));
        }
    }
}
