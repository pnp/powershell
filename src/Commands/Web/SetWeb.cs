using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPWeb")]
    [OutputType(typeof(void))]
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

        [Parameter(Mandatory = false)]
        public SwitchParameter HideTitleInHeader;

        [Parameter(Mandatory = false)]
        public SwitchParameter HorizontalQuickLaunch;
        protected override void ExecuteCmdlet()
        {
            var dirty = false;

            foreach (var key in MyInvocation.BoundParameters.Keys)
            {
                switch (key)
                {
                    case nameof(SiteLogoUrl):
                        {
                            CurrentWeb.SiteLogoUrl = SiteLogoUrl;
                            dirty = true;
                            break;
                        }
                    case nameof(AlternateCssUrl):
                        {
                            CurrentWeb.AlternateCssUrl = AlternateCssUrl;
                            dirty = true;
                            break;
                        }
                    case nameof(Title):
                        {
                            CurrentWeb.Title = Title;
                            dirty = true;
                            break;
                        }
                    case nameof(Description):
                        {
                            CurrentWeb.Description = Description;
                            dirty = true;
                            break;
                        }
                    case nameof(MasterUrl):
                        {
                            CurrentWeb.MasterUrl = MasterUrl;
                            dirty = true;
                            break;
                        }
                    case nameof(CustomMasterUrl):
                        {
                            CurrentWeb.CustomMasterUrl = CustomMasterUrl;
                            dirty = true;
                            break;
                        }
                    case nameof(QuickLaunchEnabled):
                        {
                            CurrentWeb.QuickLaunchEnabled = QuickLaunchEnabled.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(MembersCanShare):
                        {
                            CurrentWeb.MembersCanShare = MembersCanShare.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(NoCrawl):
                        {
                            CurrentWeb.NoCrawl = NoCrawl.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(HeaderLayout):
                        {
                            CurrentWeb.HeaderLayout = HeaderLayout;
                            dirty = true;
                            break;
                        }
                    case nameof(HeaderEmphasis):
                        {
                            CurrentWeb.HeaderEmphasis = HeaderEmphasis;
                            dirty = true;
                            break;
                        }
                    case nameof(NavAudienceTargetingEnabled):
                        {
                            CurrentWeb.NavAudienceTargetingEnabled = NavAudienceTargetingEnabled.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(MegaMenuEnabled):
                        {
                            CurrentWeb.MegaMenuEnabled = MegaMenuEnabled.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(DisablePowerAutomate):
                        {
                            CurrentWeb.DisableFlows = DisablePowerAutomate.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(CommentsOnSitePagesDisabled):
                        {
                            CurrentWeb.CommentsOnSitePagesDisabled = CommentsOnSitePagesDisabled.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(HideTitleInHeader):
                        {
                            CurrentWeb.HideTitleInHeader = HideTitleInHeader.ToBool();
                            dirty = true;
                            break;
                        }
                    case nameof(HorizontalQuickLaunch):
                        {
                            CurrentWeb.HorizontalQuickLaunch = HorizontalQuickLaunch.ToBool();
                            dirty = true;
                            break;
                        }
                }
            }

            if (dirty)
            {
                CurrentWeb.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
