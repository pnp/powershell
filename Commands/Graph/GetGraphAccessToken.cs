using PnP.PowerShell.CmdletHelpAttributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPGraphAccessToken")]
    public class GetGraphAccessToken : PnPGraphCmdlet
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