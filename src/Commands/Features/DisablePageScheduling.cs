using System.Management.Automation;
using System;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPPageScheduling")]
    [OutputType(typeof(void))]
    public class DisablePageScheduling : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            CurrentWeb.DeactivateFeature(new Guid("E87CA965-5E07-4A23-B007-DDD4B5AFB9C7"));
        }
    }
}
