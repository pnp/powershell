using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "PnPOfficeManagementApiAccessToken")]
    public class GetOfficeManagementApiAccessToken : PnPOfficeManagementApiCmdlet
    { 
        [Parameter(Mandatory = false)]
        public SwitchParameter Decoded;

        protected override void ExecuteCmdlet()
        {
            if (Decoded.IsPresent)
            {
                WriteObject(Token.ParsedToken);
            }
            else
            {
                WriteObject(AccessToken);
            }
        }
    }
}