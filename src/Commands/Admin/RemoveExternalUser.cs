using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPExternalUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveExternalUser : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string[] UniqueIDs;
        protected override void ExecuteCmdlet()
        {
            var office365Tenant = new Office365Tenant(AdminContext);

            var results = office365Tenant.RemoveExternalUsers(UniqueIDs);
            if (ShouldContinue("Remove External Users ?", Properties.Resources.Confirm))
            {
                var resultObject = new PSObject();
                AdminContext.Load(results);
                AdminContext.ExecuteQueryRetry();
                if (results.RemoveSucceeded.Length > 0)
                {
                    resultObject.Properties.Add(new PSNoteProperty("Succeeded", results.RemoveSucceeded));
                }
                if (results.RemoveFailed.Length > 0)
                {
                    resultObject.Properties.Add(new PSNoteProperty("Failed", results.RemoveFailed));
                }
                WriteObject(resultObject);
            }
        }
    }
}