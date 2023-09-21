using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.EntraID;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDAppPermission")]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    [Alias("Get-PnPAzureADAppPermission")]
    public class GetEntraIDAppPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public EntraIDAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ConvertToPSObject(Identity.GetApp(this, Connection, AccessToken)));
            }
            else
            {
                List<PSObject> apps = new List<PSObject>();
                var result = GraphHelper.GetResultCollectionAsync<App>(Connection, "/v1.0/applications", AccessToken).GetAwaiter().GetResult();
                if (result != null && result.Any())
                {
                    apps.AddRange(result.Select(p => ConvertToPSObject(p)));
                }
                WriteObject(apps, true);
            }
        }

        private PSObject ConvertToPSObject(App app)
        {
            var permissionScopes = new PermissionScopes();
            var o = new PSObject();
            o.Properties.Add(new PSNoteProperty("AppId", app.AppId));
            o.Properties.Add(new PSNoteProperty("DisplayName", app.DisplayName));
            var graphPermissions = app.RequiredResourceAccess.FirstOrDefault(p => p.Id == PermissionScopes.ResourceAppId_Graph);
            if (graphPermissions != null)
            {
                var p = graphPermissions.ResourceAccess.Select(p1 => permissionScopes.GetIdentifier(PermissionScopes.ResourceAppId_Graph, p1.Id, p1.Type)).ToArray();
                o.Properties.Add(new PSNoteProperty("MicrosoftGraph", p));
            }
            var sharePointPermissions = app.RequiredResourceAccess.FirstOrDefault(p => p.Id == PermissionScopes.ResourceAppId_SPO);
            if (sharePointPermissions != null)
            {
                var p = sharePointPermissions.ResourceAccess.Select(p2 => permissionScopes.GetIdentifier(PermissionScopes.ResourceAppId_SPO, p2.Id, p2.Type)).ToArray();
                o.Properties.Add(new PSNoteProperty("SharePoint", p));
            }
            return o;
        }
    }
}