using PnP.Framework.Provisioning.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSequenceSubSite")]
    
    public class AddTenantSequenceSubSite : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamNoGroupSubSite SubSite;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public SiteCollection Site;

        protected override void ProcessRecord()
        {
            if (Site.Sites.Cast<TeamNoGroupSubSite>().FirstOrDefault(s => s.Url == SubSite.Url) == null)
            {
                Site.Sites.Add(SubSite);
            }
            else
            {
                WriteError(new ErrorRecord(new Exception($"Site with URL {SubSite.Url} already exists in sequence"), "DUPLICATEURL", ErrorCategory.InvalidData, SubSite));
            }
        }
    }
}