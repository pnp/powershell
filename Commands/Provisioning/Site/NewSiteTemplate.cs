using PnP.Framework.Provisioning.Model;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsCommon.New, "SiteTemplate")]
    [Alias("New-ProvisioningTemplate")]
    public class NewSiteTemplate : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var result = new ProvisioningTemplate();
            WriteObject(result);
        }
    }
}
