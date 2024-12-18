using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSequence")]
    public class NewTenantSequence : BasePSCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Id;
        protected override void ProcessRecord()
        {
            var result = new ProvisioningSequence();
            if (this.ParameterSpecified(nameof(Id)))
            {
                result.ID = Id;
            } else
            {
                result.ID = $"sequence-{Guid.NewGuid()}";
            }
            WriteObject(result);
        }
    }
}
