using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsData.Update, "PnPUserType")]
    public class UpdateUserType : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName;

        protected override void ExecuteCmdlet()
        {
            var sitePropertiesEnumerable = this.Tenant.UpdateUserTypeFromAzureADForAllSites(LoginName);
            ClientContext.Load(sitePropertiesEnumerable);
            ClientContext.Load(sitePropertiesEnumerable, sp => sp.NextStartIndexFromSharePoint);
            ClientContext.ExecuteQueryRetry();
            if(sitePropertiesEnumerable.Count == 0)
            {
                WriteWarning("User Type is already up to date.");
            } else {
                foreach(var item in sitePropertiesEnumerable)
                {
                    WriteObject($"Updated user in site {item}");
                }
            }
        }
    }
}
