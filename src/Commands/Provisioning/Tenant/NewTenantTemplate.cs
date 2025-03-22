using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantTemplate")]
    public class NewTenantTemplate : BasePSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Author;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Generator;

        protected override void ProcessRecord()
        {
            var result = new ProvisioningHierarchy
            {
                Author = Author,
                Description = Description,
                DisplayName = DisplayName,
                Generator = Generator
            };
            WriteObject(result);
        }
    }
}