using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADUser")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/User.ReadWrite.All")]
    [Alias("Remove-PnPEntraIDUser")]
    public class RemoveAzureADUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADUserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter WhatIf;

        protected override void ExecuteCmdlet()
        {
            WriteVerbose($"Looking up user provided through the {nameof(Identity)} parameter");
            User user = Identity.GetUser(AccessToken, Connection.AzureEnvironment);

            if (user == null)
            {
                WriteWarning($"User provided through the {nameof(Identity)} parameter could not be found");
                return;
            }

            if(WhatIf.ToBool())
            {
                WriteVerbose($"Would delete user with Id {user.Id} if {nameof(WhatIf)} was not present");
                return;
            }

            WriteVerbose($"Deleting user with Id {user.Id}");

            var graphResult = GraphHelper.Delete(this, Connection, $"v1.0/users/{user.Id}", AccessToken);

            if(graphResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                WriteVerbose("User deleted successfully");
            }
            else
            {
                throw new PSArgumentException("User could not be deleted", nameof(Identity));
            }
        }
    }
}