using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPTheme")]
    public class GetTheme : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter DetectCurrentComposedLook;

        protected override void ExecuteCmdlet()
        {
            if (CurrentWeb.PropertyBagContainsKey("_PnP_ProvisioningTemplateComposedLookInfo") && !DetectCurrentComposedLook)
            {
                try
                {
                    WriteWarning("The information presented here is based upon the fact that the theme has been set using either the PnP Provisioning Engine or using the Set-PnPTheme cmdlet. This information is retrieved from a propertybag value and may differ from the actual site.");
                    var composedLook = JsonSerializer.Deserialize<ComposedLook>(CurrentWeb.GetPropertyBagValueString("_PnP_ProvisioningTemplateComposedLookInfo", ""));
                    WriteObject(composedLook);
                }
                catch
                {
                    var themeEntity = CurrentWeb.GetCurrentComposedLook();
                    WriteObject(themeEntity);
                }

            }
            else
            {
                var themeEntity = CurrentWeb.GetCurrentComposedLook();
                WriteObject(themeEntity);
            }
        }
    }
}
