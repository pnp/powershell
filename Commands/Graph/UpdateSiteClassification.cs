using PnP.Framework.Graph.Model;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsData.Update, "PnPSiteClassification")]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class UpdateSiteClassification : PnPGraphCmdlet
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
                var settings = PnP.Framework.Graph.SiteClassificationsUtility.GetSiteClassificationsSettings(AccessToken);
                if (ParameterSpecified(nameof(Classifications)))
                {
                    if (settings.Classifications != Classifications)
                    {
                        settings.Classifications = Classifications;
                        changed = true;
                    }
                }
                if (ParameterSpecified(nameof(DefaultClassification)))
                {
                    if (settings.Classifications.Contains(DefaultClassification))
                    {
                        if (settings.DefaultClassification != DefaultClassification)
                        {
                            settings.DefaultClassification = DefaultClassification;
                            changed = true;
                        }
                    }
                }
                if (ParameterSpecified(nameof(UsageGuidelinesUrl)))
                {
                    if (settings.UsageGuidelinesUrl != UsageGuidelinesUrl)
                    {
                        settings.UsageGuidelinesUrl = UsageGuidelinesUrl;
                        changed = true;
                    }
                }
                if (changed)
                {
                    if (settings.Classifications.Contains(settings.DefaultClassification))
                    {
                        PnP.Framework.Graph.SiteClassificationsUtility.UpdateSiteClassificationsSettings(AccessToken, settings);
                    }
                    else
                    {
                        WriteError(new ErrorRecord(new InvalidOperationException("You are trying to set the default classification to a value that is not available in the list of possible values."), "SITECLASSIFICATION_DEFAULTVALUE_INVALID", ErrorCategory.InvalidArgument, null));
                    }
                }
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