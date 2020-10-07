using PnP.Framework.Provisioning.Model;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Get, "TenantSequence")]
    public class GetTenantSequence : BasePSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ProvisioningHierarchy Template;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets, ValueFromPipeline = true)]
        public ProvisioningSequencePipeBind Identity;
        protected override void ProcessRecord()
        {
            if (!ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Template.Sequences, true);
            }
            else
            {
                WriteObject(Identity.GetSequenceFromHierarchy(Template));
            }
        }
    }
}