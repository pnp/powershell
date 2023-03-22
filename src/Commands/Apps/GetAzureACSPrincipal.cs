using Microsoft.SharePoint.Client;
using PnP.Core.Admin.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureACSPrincipal")]
    [OutputType(typeof(List<IACSPrincipal>))]
    public class GetAzureACSPrincipal : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureACSPrincipalScope Scope;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSubsites;
        protected override void ExecuteCmdlet()
        {
            var tenantAdminSiteUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);

            VanityUrlOptions vanityUrlOptions = new()
            {
                AdminCenterUri = new Uri(tenantAdminSiteUrl)
            };

            using var context = ClientContext.Clone(Connection.Url);

            using var pnpContext = Framework.PnPCoreSdk.Instance.GetPnPContext(context);

            if (Scope == AzureACSPrincipalScope.Tenant)
            {
                // First load a list possible principal app ids from Azure AD
                var legacyServicePrincipals = pnpContext.GetSiteCollectionManager().GetLegacyServicePrincipals();
                if (legacyServicePrincipals != null)
                {
                    // Pass in the list of app ids to get the final list of principals
                    var principals = pnpContext.GetSiteCollectionManager().GetTenantACSPrincipals(legacyServicePrincipals, vanityUrlOptions);
                    WriteObject(principals, true);
                }
            }

            else if (Scope == AzureACSPrincipalScope.All)
            {
                var legacyServicePrincipals = pnpContext.GetSiteCollectionManager().GetLegacyServicePrincipals();
                if (legacyServicePrincipals != null)
                {
                    // Pass in the list of app ids to get the final list of principals
                    var principals = pnpContext.GetSiteCollectionManager().GetTenantAndSiteCollectionACSPrincipals(legacyServicePrincipals, IncludeSubsites, vanityUrlOptions);
                    WriteObject(principals, true);
                }
            }
            else
            {
                var principals = pnpContext.GetSiteCollectionManager().GetSiteCollectionACSPrincipals(IncludeSubsites, vanityUrlOptions);
                WriteObject(principals, true);
            }
        }
    }
}
