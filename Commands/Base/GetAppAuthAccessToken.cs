using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using System;
using PnP.PowerShell.Commands.Properties;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAppAuthAccessToken")]
    public class GetPnPAppAuthAccessToken : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(ClientContext.GetAccessToken());
        }
    }
}
