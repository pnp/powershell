using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.CmdletHelpAttributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsCommon.New, "PnPProvisioningTemplate")]
    public class NewProvisioningTemplate : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var result = new ProvisioningTemplate();
            WriteObject(result);
        }
    }
}
