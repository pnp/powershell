using PnP.Framework.Provisioning.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSequence")]
    public class AddTenantSequence : PSCmdlet
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
                WriteError(new ErrorRecord(new Exception($"Sequence with ID {Sequence.ID} already exists in template"), "DUPLICATESEQUENCEID", ErrorCategory.InvalidData, Sequence));
            }
        }
    }
}