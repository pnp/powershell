using PnP.Framework.Graph.Model;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;

using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailableSiteClassification")]
    [RequiredMinimalApiPermissions("Directory.Read.All")]
    [RequiredMinimalApiPermissions("Directory.ReadWrite.All")]
    [OutputType(typeof(SiteClassificationsSettings))]
    [Alias("Get-PnPSiteClassification")]
    [WriteAliasWarning("Please use 'Get-PnPAvailableSiteClassification'. The alias 'Get-PnPSiteClassification' will be removed in a future release.")]
    public class GetAvailableSiteClassification : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            try
            {
                WriteObject(PnP.Framework.Graph.SiteClassificationsUtility.GetSiteClassificationsSettings(AccessToken), true);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    WriteError(new ErrorRecord(new InvalidOperationException("Site Classification is not enabled for this tenant. Use Enable-PnPSiteClassification to enable classifications."), "SITECLASSIFICATION_NOT_ENABLED", ErrorCategory.ResourceUnavailable, null));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}