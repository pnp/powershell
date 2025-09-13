using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPWebHeader")]
    [OutputType(typeof(void))]
    public class SetWebHeader : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public string SiteLogoUrl;

        [Parameter(Mandatory = false)]
        public string SiteThumbnailUrl;

        [Parameter(Mandatory = false)]
        public HeaderLayoutType HeaderLayout = HeaderLayoutType.Standard;

        [Parameter(Mandatory = false)]
        public SPVariantThemeType HeaderEmphasis = SPVariantThemeType.None;

        [Parameter(Mandatory = false)]
        public SwitchParameter HideTitleInHeader;

        [Parameter(Mandatory = false)]
        public string HeaderBackgroundImageUrl;

        [Parameter(Mandatory = false)]
        public double HeaderBackgroundImageFocalX;

        [Parameter(Mandatory = false)]
        public double HeaderBackgroundImageFocalY;

        [Parameter(Mandatory = false)]
        public LogoAlignment LogoAlignment;

        protected override void ExecuteCmdlet()
        {
            var requiresWebUpdate = false;
    
            if(ParameterSpecified(nameof(SiteLogoUrl)))
            {
                SetSiteImage(SiteLogoUrl, "site logo", 1);
            }

            if(ParameterSpecified(nameof(SiteThumbnailUrl)))
            {
                SetSiteImage(SiteThumbnailUrl, "thumbnailurl", 0);
            }
            
            if(ParameterSpecified(nameof(LogoAlignment)))
            {
                LogDebug($"Setting site logo alignment to '{LogoAlignment}'");
                CurrentWeb.LogoAlignment = LogoAlignment;                
                requiresWebUpdate = true;
            }            

            if(ParameterSpecified(nameof(HeaderLayout)))
            {
                LogDebug($"Setting header layout to '{HeaderLayout}'");
                CurrentWeb.HeaderLayout = HeaderLayout;
                requiresWebUpdate = true;
            }

            if(ParameterSpecified(nameof(HeaderEmphasis)))
            {
                LogDebug($"Setting header emphasis to '{HeaderEmphasis}'");
                CurrentWeb.HeaderEmphasis = HeaderEmphasis;
                requiresWebUpdate = true;
            }

            if(ParameterSpecified(nameof(HideTitleInHeader)))
            {
                LogDebug($"Setting hide title in header to '{HideTitleInHeader}'");
                CurrentWeb.HideTitleInHeader = HideTitleInHeader;
                requiresWebUpdate = true;
            }

            if(ParameterSpecified(nameof(HeaderBackgroundImageUrl)) || ParameterSpecified(nameof(HeaderBackgroundImageFocalX)) || ParameterSpecified(nameof(HeaderBackgroundImageFocalY)))
            {
                var setSiteBackgroundImageInstructions = new List<string>();
                
                if(ParameterSpecified(nameof(HeaderBackgroundImageUrl)))
                {
                    LogDebug($"Setting header background image to '{HeaderBackgroundImageUrl}'");
                    setSiteBackgroundImageInstructions.Add("\"relativeLogoUrl\":\"" + UrlUtilities.UrlEncode(HeaderBackgroundImageUrl) + "\"");
                }
                else
                {
                    LogDebug($"Setting header background image isFocalPatch to 'true'");
                    setSiteBackgroundImageInstructions.Add("\"isFocalPatch\":true");
                }
                if(ParameterSpecified(nameof(HeaderBackgroundImageFocalX)))
                {
                    LogDebug($"Setting header background image focal point X to '{HeaderBackgroundImageFocalX.ToString().Replace(',', '.')}'");
                    setSiteBackgroundImageInstructions.Add("\"focalx\":" + HeaderBackgroundImageFocalX.ToString().Replace(',', '.'));
                }
                if(ParameterSpecified(nameof(HeaderBackgroundImageFocalY)))
                {
                    LogDebug($"Setting header background image focal point Y to '{HeaderBackgroundImageFocalY.ToString().Replace(',', '.')}'");
                    setSiteBackgroundImageInstructions.Add("\"focaly\":" + HeaderBackgroundImageFocalY.ToString().Replace(',', '.'));
                }

                if (setSiteBackgroundImageInstructions.Count > 0)
                {
                    var stringContent = new StringContent("{" + string.Join(",", setSiteBackgroundImageInstructions) + ",\"type\":2,\"aspect\":0}");
                    stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    CurrentWeb.EnsureProperties(p => p.Url);
                    var result = SharePointRequestHelper.PostHttpContent($"{CurrentWeb.Url.TrimEnd('/')}/_api/siteiconmanager/setsitelogo", stringContent);
                    LogDebug($"Response from setsitelogo request: {result.StatusCode}");
                }
            }

            if (requiresWebUpdate)
            {
                LogDebug("Updating web");
                CurrentWeb.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
        private void SetSiteImage(string imageUrl, string imageType, int aspect)
        {
            LogDebug($"Setting site {imageType} image to '{imageUrl}'");

            var stringContent = new StringContent($"{{\"relativeLogoUrl\":\"{imageUrl}\",\"type\":0,\"aspect\":{aspect}}}");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            CurrentWeb.EnsureProperties(p => p.Url);
            var result = SharePointRequestHelper.PostHttpContent($"{CurrentWeb.Url.TrimEnd('/')}/_api/siteiconmanager/setsitelogo", stringContent);
            LogDebug($"Response from {imageType} request: {result.StatusCode}");
        }
    }
}
