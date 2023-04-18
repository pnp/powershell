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
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
            }
            else
            {
                siteId = PnPContext.Site.Id;
            }

            if (siteId != Guid.Empty)
            {
                var payload = new
                {
                    roles = Permissions.Select(p => p.ToLower()).ToArray(),
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

                var results = Utilities.REST.RestHelper.PostAsync<AzureADAppPermissionInternal>(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions", AccessToken, payload).GetAwaiter().GetResult();
                WriteObject(results.Convert());
            }
        }
    }
}