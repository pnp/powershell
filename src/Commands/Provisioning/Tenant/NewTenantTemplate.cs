using PnP.Framework.Provisioning.Model;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantTemplate")]
    public class NewTenantTemplate : PSCmdlet
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
            var result = new ProvisioningHierarchy();
            result.Author = Author;
            result.Description = Description;
            result.DisplayName = DisplayName;
            result.Generator = Generator;
            WriteObject(result);
        }
    }
}