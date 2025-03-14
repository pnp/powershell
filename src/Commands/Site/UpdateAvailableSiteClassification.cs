using PnP.Framework.Graph.Model;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsData.Update, "PnPAvailableSiteClassification")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
    [Alias("Update-SiteClassification")]
    [WriteAliasWarning("Please use 'Update-PnPAvailableSiteClassification'. The alias 'Update-PnPSiteClassification' will be removed in a future release.")]
    [OutputType(typeof(void))]
    public class UpdateAvailableSiteClassification : PnPGraphCmdlet
    {
        const string ParameterSet_SETTINGS = "Settings";
        const string ParameterSet_SPECIFIC = "Specific";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SETTINGS)]
        public SiteClassificationsSettings Settings;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFIC)]
        public List<string> Classifications;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFIC)]
        public string DefaultClassification;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFIC)]
        public string UsageGuidelinesUrl = "";

        protected override void ExecuteCmdlet()
        {
            try
            {
                var changed = false;
                var siteClassificationSettings = PnP.Framework.Graph.SiteClassificationsUtility.GetSiteClassificationsSettings(AccessToken);

                if (ParameterSetName == ParameterSet_SETTINGS)
                {
                    if (siteClassificationSettings.Classifications != Settings.Classifications)
                    {
                        siteClassificationSettings.Classifications = Settings.Classifications;
                        changed = true;
                    }
                    if (siteClassificationSettings.DefaultClassification != Settings.DefaultClassification)
                    {
                        siteClassificationSettings.DefaultClassification = Settings.DefaultClassification;
                        changed = true;
                    }
                    if (siteClassificationSettings.UsageGuidelinesUrl != Settings.UsageGuidelinesUrl)
                    {
                        siteClassificationSettings.UsageGuidelinesUrl = Settings.UsageGuidelinesUrl;
                        changed = true;
                    }
                }
                else
                {
                    if (ParameterSpecified(nameof(Classifications)))
                    {
                        if (siteClassificationSettings.Classifications != Classifications)
                        {
                            siteClassificationSettings.Classifications = Classifications;
                            changed = true;
                        }
                    }
                    if (ParameterSpecified(nameof(DefaultClassification)))
                    {
                        if (siteClassificationSettings.Classifications.Contains(DefaultClassification))
                        {
                            if (siteClassificationSettings.DefaultClassification != DefaultClassification)
                            {
                                siteClassificationSettings.DefaultClassification = DefaultClassification;
                                changed = true;
                            }
                        }
                        else
                        {
                            LogError("You are trying to set the default classification to a value that is not available in the list of possible values. Use Get-PnPAvailableSiteClassification see which site classifications you can use.");
                        }
                    }
                    if (ParameterSpecified(nameof(UsageGuidelinesUrl)))
                    {
                        if (siteClassificationSettings.UsageGuidelinesUrl != UsageGuidelinesUrl)
                        {
                            siteClassificationSettings.UsageGuidelinesUrl = UsageGuidelinesUrl;
                            changed = true;
                        }
                    }
                }
                if (changed)
                {
                    PnP.Framework.Graph.SiteClassificationsUtility.UpdateSiteClassificationsSettings(AccessToken, siteClassificationSettings);
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    LogError(new InvalidOperationException("Site Classification is not enabled for this tenant. Use Enable-PnPSiteClassification to enable classifications."));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}