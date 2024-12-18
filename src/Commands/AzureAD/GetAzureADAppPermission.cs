using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADAppPermission")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
    [Alias("Get-PnPEntraIDAppPermission")]
    public class GetAzureADAppPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureADAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(RequestHelper);
                if (app == null)
                {
                    WriteError(new PSArgumentException("Azure AD App not found"), ErrorCategory.ObjectNotFound);
                }
                WriteObject(ConvertToPSObject(app));
            }
            else
            {
                List<PSObject> apps = new List<PSObject>();
                var result = RequestHelper.GetResultCollection<AzureADApp>("/v1.0/applications");
                if (result != null && result.Any())
                {
                    apps.AddRange(result.Select(p => ConvertToPSObject(p)));
                }
                WriteObject(apps, true);
            }
        }

        private PSObject ConvertToPSObject(AzureADApp app)
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