using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantTheme")]
    [CmdletHelp("Removes a theme",
        DetailedDescription = @"Removes the specified theme from the tenant configuration",
        Category = CmdletHelpCategory.TenantAdmin)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPTenantTheme -Name ""MyCompanyTheme""",
        Remarks = @"Removes the specified theme.", SortOrder = 1)]
    public class RemoveTenantTheme : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "The name of the theme to retrieve")]
        [Alias("Name")]
        public ThemePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Tenant.DeleteTenantTheme(Identity.Name);
            ClientContext.ExecuteQueryRetry();
        }
    }
}