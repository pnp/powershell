using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPWeb")]
    public class SetWeb : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public string SiteLogoUrl;

        [Parameter(Mandatory = false)]
        public string AlternateCssUrl;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string MasterUrl;

        [Parameter(Mandatory = false)]
        public string CustomMasterUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter QuickLaunchEnabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter MembersCanShare;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoCrawl;

        [Parameter(Mandatory = false)]
        public HeaderLayoutType HeaderLayout = HeaderLayoutType.Standard;

        [Parameter(Mandatory = false)]
        public SPVariantThemeType HeaderEmphasis = SPVariantThemeType.None;

        [Parameter(Mandatory = false)]
        public SwitchParameter NavAudienceTargetingEnabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter MegaMenuEnabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisablePowerAutomate;

        [Parameter(Mandatory = false)]
        public SwitchParameter CommentsOnSitePagesDisabled;

        protected override void ExecuteCmdlet()
        {
            var dirty = false;

            foreach (var key in MyInvocation.BoundParameters.Keys)
            {
                switch (key)
                {
                    case nameof(SiteLogoUrl):
                        SelectedWeb.SiteLogoUrl = SiteLogoUrl;
                        dirty = true;
                        break;

                    case nameof(AlternateCssUrl):
                        SelectedWeb.AlternateCssUrl = AlternateCssUrl;
                        dirty = true;
                        break;

                    case nameof(Title):
                        SelectedWeb.Title = Title;
                        dirty = true;
                        break;

                    case nameof(Description):
                        SelectedWeb.Description = Description;
                        dirty = true;
                        break;

                    case nameof(MasterUrl):
                        SelectedWeb.MasterUrl = MasterUrl;
                        dirty = true;
                        break;

                    case nameof(CustomMasterUrl):
                        SelectedWeb.CustomMasterUrl = CustomMasterUrl;
                        dirty = true;
                        break;

                    case nameof(QuickLaunchEnabled):
                        SelectedWeb.QuickLaunchEnabled = QuickLaunchEnabled.ToBool();
                        dirty = true;
                        break;
                    case nameof(MembersCanShare):
                        SelectedWeb.MembersCanShare = MembersCanShare.ToBool();
                        dirty = true;
                        break;

                    case nameof(NoCrawl):
                        SelectedWeb.NoCrawl = NoCrawl.ToBool();
                        dirty = true;
                        break;

                    case nameof(HeaderLayout):
                        SelectedWeb.HeaderLayout = HeaderLayout;
                        dirty = true;
                        break;

                    case nameof(HeaderEmphasis):
                        SelectedWeb.HeaderEmphasis = HeaderEmphasis;
                        dirty = true;
                        break;

                    case nameof(NavAudienceTargetingEnabled):
                        SelectedWeb.NavAudienceTargetingEnabled = NavAudienceTargetingEnabled.ToBool();
                        dirty = true;
                        break;

                    case nameof(MegaMenuEnabled):
                        SelectedWeb.MegaMenuEnabled = MegaMenuEnabled.ToBool();
                        dirty = true;
                        break;

                    case nameof(DisablePowerAutomate):
                        SelectedWeb.DisableFlows = DisablePowerAutomate.ToBool();
                        dirty = true;
                        break;

                    case nameof(CommentsOnSitePagesDisabled):
                        SelectedWeb.CommentsOnSitePagesDisabled = CommentsOnSitePagesDisabled.ToBool();
                        dirty = true;
                        break;
                }
            }

            if (dirty)
            {
                SelectedWeb.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
