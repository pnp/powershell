using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPAvailableSiteClassification")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Add-PnPSiteClassification")]
    [WriteAliasWarning("Please use 'Add-PnPAvailableSiteClassification'. The alias 'Add-PnPSiteClassification' will be removed in a future release.")]
    [OutputType(typeof(void))]
    public class AddSiteClassification : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public List<string> Classifications;

        protected override void ExecuteCmdlet()
        {
            try
            {
                var settings = PnP.Framework.Graph.SiteClassificationsUtility.GetSiteClassificationsSettings(AccessToken);
                foreach (var classification in Classifications)
                {
                    if (!settings.Classifications.Contains(classification))
                    {
                        settings.Classifications.Add(classification);
                    }
                }
                PnP.Framework.Graph.SiteClassificationsUtility.UpdateSiteClassificationsSettings(AccessToken, settings);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    WriteError(new ErrorRecord(new InvalidOperationException("Site Classification is not enabled for this tenant"), "SITECLASSIFICATION_NOT_ENABLED", ErrorCategory.ResourceUnavailable, null));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}