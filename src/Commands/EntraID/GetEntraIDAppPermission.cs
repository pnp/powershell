using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDAppPermission")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
    [Alias("Get-PnPAzureADAppPermission")]
    public class GetAzureADAppPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public EntraIDAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(GraphRequestHelper);
                if (app == null)
                {
                    LogError(new PSArgumentException("Azure AD App not found"));
                }
                WriteObject(ConvertToPSObject(app));
            }
            else
            {
                List<PSObject> apps = new List<PSObject>();
                var result = GraphRequestHelper.GetResultCollection<AzureADApp>("/v1.0/applications");
                if (result != null && result.Any())
                {
                    apps.AddRange(result.Select(p => ConvertToPSObject(p)));
                }
                WriteObject(apps, true);
            }
        }

        /// <summary>
        /// The resources whose permissions can be reported by name, being the ones whose available permissions ship with the module.
        /// </summary>
        private static readonly (string ResourceAppId, string PropertyName)[] resources = new[]
        {
            (PermissionScopes.ResourceAppId_Graph, "MicrosoftGraph"),
            (PermissionScopes.ResourceAppId_SPO, "SharePoint"),
            (PermissionScopes.ResourceAppID_O365Management, "Office365Management")
        };

        private readonly PermissionScopes permissionScopes = new PermissionScopes();

        private PSObject ConvertToPSObject(AzureADApp app)
        {
            var o = new PSObject();
            o.Properties.Add(new PSNoteProperty("AppId", app.AppId));
            o.Properties.Add(new PSNoteProperty("DisplayName", app.DisplayName));
            foreach (var (resourceAppId, propertyName) in resources)
            {
                var resourcePermissions = app.RequiredResourceAccess?.FirstOrDefault(p => p.Id == resourceAppId);
                if (resourcePermissions != null)
                {
                    o.Properties.Add(new PSNoteProperty(propertyName, resourcePermissions.ResourceAccess.Select(p => permissionScopes.GetIdentifier(resourceAppId, p.Id, p.Type)).ToArray()));
                }
            }
            return o;
        }
    }
}