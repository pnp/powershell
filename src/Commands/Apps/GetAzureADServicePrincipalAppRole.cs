using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADServicePrincipalAppRole")]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    [OutputType(typeof(List<AzureADServicePrincipalAppRole>))]
    public class GetAzureADServicePrincipalAppRole : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = false)]
        public ServicePrincipalAppRoleBind Identity;

        protected override void ExecuteCmdlet()
        {
            var principal = Principal.GetServicePrincipal(Connection, AccessToken);

            if(principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            WriteVerbose($"Requesting app roles for service principal {principal.DisplayName}");

            if(ParameterSpecified(nameof(Identity)))
            {
                var appRole = Identity.GetAppRole(Connection, AccessToken, principal);
                WriteObject(appRole, false);
            }
            else
            {
                WriteObject(principal.AppRoles, true);
            }
        }
    }
}