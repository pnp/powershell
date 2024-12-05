using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantSequenceSite")]
    public class GetTenantSequenceSite : BasePSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ProvisioningSequence Sequence;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets, ValueFromPipeline = true)]
        public ProvisioningSitePipeBind Identity;
        protected override void ProcessRecord()
        {
            if (!ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Sequence.SiteCollections, true);
            }
            else
            {
                WriteObject(Identity.GetSiteFromSequence(Sequence));
            }
        }
    }
}