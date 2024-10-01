using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAvailableSiteClassification")]
    [RequiredApiApplicationPermissions("graph/Directory.ReadWrite.All")]
    [Alias("Remove-PnPSiteClassitication")]
    [WriteAliasWarning("Please use 'Remove-PnPAvailableSiteClassification'. The alias 'Remove-PnPSiteClassification' will be removed in a future release.")]
    [OutputType(typeof(void))]
    public class RemoveSiteClassification : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public List<string> Classifications;

        [Parameter(Mandatory = false)]
        public SwitchParameter Confirm;

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
                            if ((ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveDefaultClassification0, classification), Properties.Resources.Confirm))
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
                    WriteError(new ErrorRecord(new InvalidOperationException("At least one classification is required. If you want to disable classifications, use Disable-PnPSiteClassification."), "SITECLASSIFICATIONS_ARE_REQUIRED", ErrorCategory.InvalidOperation, null));
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    WriteError(new ErrorRecord(new InvalidOperationException("Site Classification is not enabled for this tenant"), "SITECLASSIFICATION_NOT_ENABLED", ErrorCategory.ResourceUnavailable, null));
                }
            }
        }
    }
}