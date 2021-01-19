using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Remove, "PnPRoleDefinition")]
    public class RemoveRoleDefinition : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public RoleDefinitionPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var roleDefinition = Identity.GetRoleDefinition(ClientContext.Site);
            if (roleDefinition != null)
            {
                try
                {
                    if (Force || ShouldContinue($@"Remove Role Definition ""{roleDefinition.Name}""?", "Confirm"))
                    {
                        roleDefinition.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                        WriteVerbose($@"Removed Role Definition ""{roleDefinition.Name}""");
                    }
                }
                catch (ServerException e)
                {
                    WriteWarning($@"Exception occurred while trying to remove the Role Definition: ""{e.Message}"". Will be skipped.");
                }
            }
            else
            {
                WriteWarning($"Unable to remove Role Definition as it wasn't found. Will be skipped.");
            }
        }
    }
}
