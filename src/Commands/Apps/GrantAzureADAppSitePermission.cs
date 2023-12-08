using System;
using System.Linq;
using System.Management.Automation;

using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Grant, "PnPAzureADAppSitePermission")]
    [RequiredMinimalApiPermissions("Sites.FullControl.All")]
    [Alias("Grant-PnPEntraIDAppSitePermission")]
    [OutputType(typeof(AzureADAppPermissionInternal))]
    public class GrantPnPAzureADAppSitePermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid AppId;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<AzureADNewSitePermissionRole>))]
        public string[] Permissions;

        protected override void ExecuteCmdlet()
        {
            Guid siteId = Guid.Empty;
            if (ParameterSpecified(nameof(Site)))
            {
                WriteVerbose($"Using Microsoft Graph to lookup the site Id of the passed in site using -{nameof(Site)}");
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
                WriteVerbose($"Site passed in using -{nameof(Site)} resolved to Id {siteId}");
            }
            else
            {
                WriteVerbose($"No specific site passed in through -{nameof(Site)}, taking the currently connected to site");
                siteId = PnPContext.Site.Id;
                WriteVerbose($"Currently connected to site has Id {siteId}");
            }

            if (siteId == Guid.Empty)
            {
                WriteVerbose("Id of the site to provide permissions on could not be defined. Please ensure you're passing in a valid site using -{nameof(Site)}");
                return;
            }

            // Construct the payload of the Graph request
            var payload = new
            {
                roles = Permissions.Select(p => p.ToString().ToLowerInvariant()).ToArray(),
                grantedToIdentities = new[] {
                        new {
                            application = new {
                                id = AppId.ToString(),
                                displayName = DisplayName
                            }
                        }
                    },
                grantedToIdentitiesV2 = new[] {
                        new {
                            application = new {
                                id = AppId.ToString(),
                                displayName = DisplayName
                            }
                        }
                    }
            };

            WriteVerbose($"Granting App with Id {AppId} the permission{(payload.roles.Length != 1 ? "s" : "")} {string.Join(',', payload.roles)}");

            // Make the Graph Grant request
            var result = Utilities.REST.RestHelper.PostAsync<AzureADAppPermissionInternal>(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions", AccessToken, payload).GetAwaiter().GetResult();
            WriteObject(result.Convert());
        }
    }
}