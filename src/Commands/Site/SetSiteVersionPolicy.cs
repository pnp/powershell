using Microsoft.SharePoint.Client;

using System;
using System.Linq.Expressions;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteVersionPolicy")]
    [RequiredApiDelegatedOrApplicationPermissions("sharepoint/AllSites.FullControl")]
    [OutputType(typeof(void))]
    public class SetSiteVersionPolicy : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public bool EnableAutoExpirationVersionTrim;

        [Parameter(Mandatory = false)]
        public int ExpireVersionsAfterDays;

        [Parameter(Mandatory = false)]
        public int MajorVersions;

        [Parameter(Mandatory = false)]
        public int MajorWithMinorVersions;

        [Parameter(Mandatory = false)]
        public SwitchParameter InheritFromTenant;

        [Parameter(Mandatory = false)]
        public SwitchParameter ApplyToNewDocumentLibraries;

        [Parameter(Mandatory = false)]
        public SwitchParameter ApplyToExistingDocumentLibraries;

        [Parameter(Mandatory = false)]
        public SwitchParameter CancelForExistingDocumentLibraries;

        protected override void ExecuteCmdlet()
        {
            var context = ClientContext;
            var site = ClientContext.Site;

            if (ParameterSpecified(nameof(InheritFromTenant)))
            {
                if (ParameterSpecified(nameof(EnableAutoExpirationVersionTrim)) ||
                    ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                    ParameterSpecified(nameof(MajorVersions)) ||
                    ParameterSpecified(nameof(MajorWithMinorVersions)) ||
                    ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) ||
                    ParameterSpecified(nameof(ApplyToExistingDocumentLibraries)) ||
                    ParameterSpecified(nameof(CancelForExistingDocumentLibraries)))
                {
                    throw new PSArgumentException($"Don't specify version policy related parameters (EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions, MajorWithMinorVersions, ApplyToNewDocumentLibraries, ApplyToExistingDocumentLibraries, CancelForExistingDocumentLibraries) when InheritFromTenant is specified.");
                }

                site.EnsureProperty(s => s.VersionPolicyForNewLibrariesTemplate);
                site.VersionPolicyForNewLibrariesTemplate.InheritTenantSettings();
                context.ExecuteQueryRetry();
                WriteWarning("The setting for new document libraries takes effect immediately. Please run Get-PnPSiteVersionPolicy to display the newly set values.");
            }
            else
            {
                if (ParameterSpecified(nameof(CancelForExistingDocumentLibraries)))
                {
                    if (ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) ||
                        ParameterSpecified(nameof(ApplyToExistingDocumentLibraries)) ||
                        ParameterSpecified(nameof(EnableAutoExpirationVersionTrim)) ||
                        ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                        ParameterSpecified(nameof(MajorVersions)) ||
                        ParameterSpecified(nameof(MajorWithMinorVersions)))
                    {
                        throw new PSArgumentException($"Don't specify the version policy related parameters (ApplyToNewDocumentLibraries, ApplyToExistingDocumentLibraries, EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions) when CancelForExistingDocumentLibraries is specified.");
                    }

                    site.CancelSetVersionPolicyForDocLibs();
                    context.ExecuteQueryRetry();
                }
                else
                {
                    // There are 4 scenarios for parameters ApplyToNewDocumentLibraries and ApplyToExistingDocumentLibraries
                    // Scenario 1: ApplyToNewDocumentLibraries only
                    // Scenario 2: ApplyToExistingDocumentLibraries only
                    // Scenario 3: Both ApplyToNewDocumentLibraries and ApplyToExistingDocumentLibraries
                    // Scenario 4: Neither ApplyToNewDocumentLibraries or ApplyToExistingDocumentLibraries
                    // For Scenario 3 & 4, they should be the same, set both new document libraries and existing document libraries
                    // Only scenario 1 does not require MajorWithMinorVersions when EnableAutoExpirationVersionTrim is false because minor version is disabled on new document libraries

                    if (ParameterSpecified(nameof(EnableAutoExpirationVersionTrim)))
                    {
                        // Validate parameters when EnableAutoExpirationVersionTrim is specified
                        if (EnableAutoExpirationVersionTrim)
                        {
                            if (ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                                ParameterSpecified(nameof(MajorVersions)) ||
                                ParameterSpecified(nameof(MajorWithMinorVersions)))
                            {
                                throw new PSArgumentException($"Don't specify ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions when EnableAutoExpirationVersionTrim is true.");
                            }
                        }
                        else
                        {
                            if (ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) &&
                                !ParameterSpecified(nameof(ApplyToExistingDocumentLibraries)))
                            {
                                // If Scenario 1: ApplyToNewDocumentLibraries only
                                // MinorVerions is not needed
                                if (!ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                                    !ParameterSpecified(nameof(MajorVersions)) ||
                                    ParameterSpecified(nameof(MajorWithMinorVersions)))
                                {
                                    throw new PSArgumentException($"You must specify ExpireVersionsAfterDays, MajorVersions and don't specify MajorWithMinorVersions when EnableAutoExpirationVersionTrim is false for new document libraries only.");
                                }
                            }
                            else
                            {
                                if (!ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                                    !ParameterSpecified(nameof(MajorVersions)) ||
                                    !ParameterSpecified(nameof(MajorWithMinorVersions)))
                                {
                                    throw new PSArgumentException($"You must specify ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions when EnableAutoExpirationVersionTrim is false for document libraries that including existing ones.");
                                }
                            }
                        }

                        // Do setting when EnableAutoExpirationVersionTrim is specified
                        if (!(!ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) &&
                            ParameterSpecified(nameof(ApplyToExistingDocumentLibraries))))
                        {
                            // If NOT "Scenario 2: ApplyToExistingDocumentLibraries only"
                            // Do setting for new document libraries
                            if (EnableAutoExpirationVersionTrim)
                            {
                                site.EnsureProperty(s => s.VersionPolicyForNewLibrariesTemplate);
                                site.VersionPolicyForNewLibrariesTemplate.SetAutoExpiration();
                                context.ExecuteQueryRetry();
                            }
                            else
                            {
                                site.EnsureProperty(s => s.VersionPolicyForNewLibrariesTemplate);
                                if (ExpireVersionsAfterDays == 0)
                                {
                                    site.VersionPolicyForNewLibrariesTemplate.SetNoExpiration(MajorVersions);
                                    context.ExecuteQueryRetry();
                                }
                                else
                                {
                                    site.VersionPolicyForNewLibrariesTemplate.SetExpireAfter(MajorVersions, ExpireVersionsAfterDays);
                                    context.ExecuteQueryRetry();
                                }
                            }

                            WriteWarning("The setting for new libraries takes effect immediately. Please run Get-PnPSiteVersionPolicy to display the newly set values.");
                        }

                        if (!(ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) &&
                            !ParameterSpecified(nameof(ApplyToExistingDocumentLibraries))))
                        {
                            // If NOT "Scenario 1: ApplyToNewDocumentLibraries only"
                            // Create setting request for existing document libraries
                            if (EnableAutoExpirationVersionTrim)
                            {
                                site.StartSetVersionPolicyForDocLibs(true, -1, -1, -1);
                                context.ExecuteQueryRetry();
                            }
                            else
                            {
                                site.StartSetVersionPolicyForDocLibs(false, MajorVersions, MajorWithMinorVersions, ExpireVersionsAfterDays);
                                context.ExecuteQueryRetry();
                            }

                            WriteWarning("The setting for existing libraries takes at least 24 hours to take effect. Please run Get-PnPSiteVersionPolicyStatus to check the status.");
                            WriteWarning("The setting for existing libraries does not trim existing versions.");
                        }
                    }
                    else
                    {
                        if (ParameterSpecified(nameof(ApplyToNewDocumentLibraries)) ||
                            ParameterSpecified(nameof(ApplyToExistingDocumentLibraries)) )
                        {
                            throw new PSArgumentException($"You must specify EnableAutoExpirationVersionTrim and other version policy related parameters (ExpireVersionsAfterDays, MajorVersions, MajorWithMinorVersions) when ApplyToNewDocumentLibraries or ApplyToExistingDocumentLibraries is specified.");
                        }

                        if (ParameterSpecified(nameof(ExpireVersionsAfterDays)) ||
                            ParameterSpecified(nameof(MajorVersions)) ||
                            ParameterSpecified(nameof(MajorWithMinorVersions)))
                        {
                            throw new PSArgumentException($"You must specify EnableAutoExpirationVersionTrim when ExpireVersionsAfterDays, MajorVersions or MajorWithMinorVersions is specified.");
                        }
                    }
                }
            }
        }
    }
}
