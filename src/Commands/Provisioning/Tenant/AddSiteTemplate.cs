using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteTemplate")]
    public class AddSiteTemplate : BasePSCmdlet
    {
        [Parameter(Mandatory = true)]
        public ProvisioningTemplate SiteTemplate;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ProvisioningHierarchy TenantTemplate;

        protected override void ProcessRecord()
        {
            if(TenantTemplate.Templates.FirstOrDefault(t => t.Id == SiteTemplate.Id) == null)
            {
                TenantTemplate.Templates.Add(SiteTemplate);
            } else { 
                WriteError(new ErrorRecord(new Exception($"Template with ID {SiteTemplate.Id} already exists in template"), "DUPLICATETEMPLATE", ErrorCategory.InvalidData, SiteTemplate));
            }
        }
    }
}