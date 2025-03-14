using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAvailableSiteClassification")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
    [OutputType(typeof(void))]
    public class RemoveSiteClassification : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public List<string> Classifications;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            try
            {
                var existingSettings = PnP.Framework.Graph.SiteClassificationsUtility.GetSiteClassificationsSettings(AccessToken);
                foreach (var classification in Classifications)
                {
                    if (existingSettings.Classifications.Contains(classification))
                    {

                        if (existingSettings.DefaultClassification == classification)
                        {
                            if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveDefaultClassification0, classification), Properties.Resources.Confirm))
                            {
                                existingSettings.DefaultClassification = "";
                                existingSettings.Classifications.Remove(classification);
                            }
                        }
                        else
                        {
                            existingSettings.Classifications.Remove(classification);
                        }
                    }
                }
                if (existingSettings.Classifications.Any())
                {
                    PnP.Framework.Graph.SiteClassificationsUtility.UpdateSiteClassificationsSettings(AccessToken, existingSettings);
                }
                else
                {
                    LogError("At least one classification is required. If you want to disable classifications, use Disable-PnPSiteClassification.");
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    LogError("Site Classification is not enabled for this tenant");
                }
            }
        }
    }
}