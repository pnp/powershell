using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsData.Update, "PnPUserType")]
    [OutputType(typeof(string))]
    public class UpdateUserType : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName;

        protected override void ExecuteCmdlet()
        {
            var sitePropertiesEnumerable = this.Tenant.UpdateUserTypeFromAzureADForAllSites(LoginName);
            AdminContext.Load(sitePropertiesEnumerable);
            AdminContext.Load(sitePropertiesEnumerable, sp => sp.NextStartIndexFromSharePoint);
            AdminContext.ExecuteQueryRetry();
            if(sitePropertiesEnumerable.Count == 0)
            {
                LogWarning("User Type is already up to date.");
            } else {
                foreach(var item in sitePropertiesEnumerable)
                {
                    WriteObject($"Updated user in site {item}");
                }
            }
        }
    }
}
