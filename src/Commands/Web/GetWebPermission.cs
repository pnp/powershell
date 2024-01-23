using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Core.Model.Security;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPWebPermission")]
    [OutputType(typeof(IRoleDefinition))]
    public class GetWebPermission : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByName")]
        public WebPipeBind Identity;

        [Parameter(Mandatory = true)]
        public int PrincipalId;

        protected override void ExecuteCmdlet()
        {

            Web web = CurrentWeb;
            if (ParameterSpecified(nameof(Identity)))
            {
                web = Identity.GetWeb(ClientContext);
            }

            var roleAssignments = web.RoleAssignments;
            web.Context.Load(roleAssignments);
            web.Context.ExecuteQueryRetry();

            RoleAssignment roleAssignment = roleAssignments.FirstOrDefault((RoleAssignment ra) => ra.PrincipalId.Equals(PrincipalId));
            if (roleAssignment != null)
            {
                web.Context.Load(roleAssignment.RoleDefinitionBindings);
                web.Context.ExecuteQueryRetry();

                if (roleAssignment.RoleDefinitionBindings.Count > 0)
                {
                    WriteObject(roleAssignment.RoleDefinitionBindings, true);
                }
            }
        }
    }
}
