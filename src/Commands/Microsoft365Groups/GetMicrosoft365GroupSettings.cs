using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupSettings")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Directory.Read.All")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Directory.ReadWrite.All")]
    public class GetMicrosoft365GroupSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Identity;
        
        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var groupId = Identity.GetGroupId(this, Connection, AccessToken);
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken, groupId.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            else
            {
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken);
                WriteObject(groupSettings?.Value, true);
            }
        }
    }
}