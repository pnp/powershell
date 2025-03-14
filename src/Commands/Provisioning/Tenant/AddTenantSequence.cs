using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSequence")]
    public class AddTenantSequence : BasePSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ProvisioningHierarchy Template;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets, ValueFromPipeline = true)]
        public ProvisioningSequence Sequence;

        protected override void ProcessRecord()
        {
            if (Template.Sequences.FirstOrDefault(s => s.ID == Sequence.ID) == null)
            {
                Template.Sequences.Add(Sequence);
                WriteObject(Template);
            }
            else
            {
                LogError($"Sequence with ID {Sequence.ID} already exists in template");
            }
        }
    }
}