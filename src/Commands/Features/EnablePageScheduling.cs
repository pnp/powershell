using System.Management.Automation;
using System;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPageScheduling")]
    public class EnablePageScheduling : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            CurrentWeb.ActivateFeature(new Guid("E87CA965-5E07-4A23-B007-DDD4B5AFB9C7"));
        }
    }
}
