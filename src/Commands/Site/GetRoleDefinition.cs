using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPRoleDefinition")]
    [OutputType(typeof(RoleDefinition))]
    public class GetRoleDefinition : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public RoleDefinitionPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var roleDefinition = Identity.GetRoleDefinition(ClientContext.Site);
                ClientContext.Load(roleDefinition);
                ClientContext.ExecuteQueryRetry();
                WriteObject(roleDefinition);
            }
            else
            {
                var roleDefinitions = ClientContext.Site.RootWeb.RoleDefinitions;
                ClientContext.Load(roleDefinitions);
                ClientContext.ExecuteQueryRetry();
                WriteObject(roleDefinitions, true);
            }
        }
    }
}
