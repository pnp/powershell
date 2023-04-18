using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Model;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantTheme")]
    public class GetTenantTheme : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public string Name;

        [Parameter(Mandatory = false, Position = 1)]
        public SwitchParameter AsJson;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Name)))
            {
                var theme = Tenant.GetTenantTheme(Name);
                AdminContext.Load(theme);
                AdminContext.ExecuteQueryRetry();
                if (AsJson)
                {
                    WriteObject(JsonSerializer.Serialize(theme.Palette));
                }
                else
                {
                    WriteObject(new SPOTheme(theme.Name, theme.Palette, theme.IsInverted));
                }
            }
            else
            {
                var themes = Tenant.GetAllTenantThemes();
                AdminContext.Load(themes);
                AdminContext.ExecuteQueryRetry();
                if (AsJson)
                {
                    WriteObject(JsonSerializer.Serialize(themes.Select(t => new SPOTheme(t.Name, t.Palette, t.IsInverted))));
                }
                else
                {
                    WriteObject(themes.Select(t => new SPOTheme(t.Name, t.Palette, t.IsInverted)), true);
                }
            }
        }
    }
}