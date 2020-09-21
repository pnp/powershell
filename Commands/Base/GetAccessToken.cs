using PnP.PowerShell.CmdletHelpAttributes;
using System;
using System.Collections.Generic;
#if PNPPSCORE
using System.IdentityModel.Tokens.Jwt;
#else
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
#endif
using System.Linq;
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