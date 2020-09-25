using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken")]
    [Obsolete("Use Get-PnPGraphAccessToken instead")]
    public class GetPnPAccessToken : PnPGraphCmdlet
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