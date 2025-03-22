using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSequenceSite")]
    public class AddTenantSequenceSite : BasePSCmdlet
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