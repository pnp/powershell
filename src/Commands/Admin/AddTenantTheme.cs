using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Model;
using System.Linq;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantTheme")]
    public class AddTenantTheme : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ThemePipeBind Identity;

        [Parameter(Mandatory = true)]
        public ThemePalettePipeBind Palette;

        [Parameter(Mandatory = true)]
        public bool IsInverted;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite { get; set; }

        protected override void ExecuteCmdlet()
        {
            var theme = new SPOTheme(Identity.Name, Palette.ThemePalette, IsInverted);

            var themes = Tenant.GetAllTenantThemes();
            AdminContext.Load(themes);
            AdminContext.ExecuteQueryRetry();
            if (themes.FirstOrDefault(t => t.Name == Identity.Name) != null)
            {
                if (Overwrite.ToBool())
                {
                    Tenant.UpdateTenantTheme(Identity.Name, JsonSerializer.Serialize(theme));
                    AdminContext.ExecuteQueryRetry();
                }
                else
                {
                    LogError("Theme exists");
                }
            }
            else
            {
                Tenant.AddTenantTheme(Identity.Name, JsonSerializer.Serialize(theme));
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}