using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSequenceSite", SupportsShouldProcess = true)]
    public class AddTenantSequenceSite : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ProvisioningSitePipeBind Site;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ProvisioningSequence Sequence;

        protected override void ProcessRecord()
        {
            Sequence.SiteCollections.Add(Site.Site);
            WriteObject(Sequence);
        }
    }
}