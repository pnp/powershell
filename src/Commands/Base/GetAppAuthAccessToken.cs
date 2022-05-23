using System.Management.Automation;

using System;
using PnP.PowerShell.Commands.Properties;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAppAuthAccessToken")]
    [OutputType(typeof(string))]
    public class GetPnPAppAuthAccessToken : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(ClientContext.GetAccessToken());
        }
    }
}
