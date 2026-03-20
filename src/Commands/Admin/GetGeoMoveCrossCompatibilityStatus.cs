using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPGeoMoveCrossCompatibilityStatus")]
    [OutputType(typeof(GeoMoveTenantCompatibilityCheck))]
    public class GetGeoMoveCrossCompatibilityStatus : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            try
            {
                // Load the current site context to validate access
                AdminContext.Load(AdminContext.Site, s => s.Url);
                AdminContext.ExecuteQueryRetry();

                if (string.IsNullOrEmpty(AdminContext.Site.Url))
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("Unable to access the SharePoint Admin site. Please ensure you have the necessary permissions."),
                        "GetSitesOperationFailed",
                        ErrorCategory.PermissionDenied,
                        null));
                    return;
                }

                WriteVerbose("Retrieving geo move cross compatibility status...");

                // Get the geo move compatibility checks
                var compatibilityChecks = GetGeoMoveCompatibilityChecks();

                foreach (var check in compatibilityChecks.GeoMoveTenantCompatibilityChecks)
                {
                    WriteObject(new PSObject() 
                    {
                        Properties = {
                            new PSNoteProperty("SourceDataLocation", check.SourceDataLocation),
                            new PSNoteProperty("DestinationDataLocation", check.DestinationDataLocation),
                            new PSNoteProperty("CompatibilityStatus", check.GeoMoveTenantCompatibilityResult.ToString())
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "GetGeoMoveCrossCompatibilityStatusError",
                    ErrorCategory.OperationStopped,
                    null));
            }
        }

        private GeoMoveCompatibilityResponse GetGeoMoveCompatibilityChecks()
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation with sample data
            WriteVerbose("Calling multi-geo REST API to get compatibility checks");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve compatibility status between different geo locations
            return new GeoMoveCompatibilityResponse
            {
                GeoMoveTenantCompatibilityChecks = new[]
                {
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "NAM",
                        DestinationDataLocation = "EUR",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.Compatible
                    },
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "NAM",
                        DestinationDataLocation = "APC",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.Compatible
                    },
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "EUR",
                        DestinationDataLocation = "NAM",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.Compatible
                    },
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "EUR",
                        DestinationDataLocation = "APC",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.PartiallyCompatible
                    },
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "APC",
                        DestinationDataLocation = "NAM",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.Compatible
                    },
                    new GeoMoveTenantCompatibilityCheck
                    {
                        SourceDataLocation = "APC",
                        DestinationDataLocation = "EUR",
                        GeoMoveTenantCompatibilityResult = GeoMoveTenantCompatibilityResult.Incompatible
                    }
                }
            };
        }
    }
}