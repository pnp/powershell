using PnP.PowerShell.Commands.Utilities.REST;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Online.SharePoint.TenantAdministration;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utilities for working with Site Templates, Site Designs and Site Scripts
    /// </summary>
    internal static class SiteTemplates
    {
        #region Site Scripts

        /// <summary>
        /// Invokes the provided site script on the provided site
        /// </summary>
        /// <param name="connection">Connection that can be used to make HTTP requests</param>
        /// <param name="accessToken">Access Token that can be used to authorize HTTP requests</param>
        /// <param name="script">The Site Script to invoke</param>
        /// <param name="siteUrl">The URL of the SharePoint site to invoke the Site Script on</param>
        /// <returns>HttpResponseMessage with the</returns>
        public static RestResultCollection<InvokeSiteScriptActionResponse> InvokeSiteScript(ApiRequestHelper requestHelper, TenantSiteScript script, string siteUrl)
        {
            return InvokeSiteScript(requestHelper, script.Content, siteUrl);
        }

        /// <summary>
        /// Invokes the provided site script on the provided site
        /// </summary>
        /// <param name="connection">Connection that can be used to make HTTP requests</param>
        /// <param name="accessToken">Access Token that can be used to authorize HTTP requests</param>
        /// <param name="scriptContent">The Site Script content to invoke</param>
        /// <param name="siteUrl">The URL of the SharePoint site to invoke the Site Script on</param>
        /// <returns></returns>
        public static RestResultCollection<InvokeSiteScriptActionResponse> InvokeSiteScript(ApiRequestHelper requestHelper, string scriptContent, string siteUrl)
        {
            // Properly encode the contents of the provided site script
            var escapedScript = Regex.Replace(scriptContent.Replace("\\\"", "\\\\\\\""), "(?<!\\\\)\"", "\\\"", RegexOptions.Singleline);
            
            // Construct the HTTP Post body
            var postBody = new StringContent(string.Concat(@"{ ""script"": """, escapedScript, " \"}"));
            postBody.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Execute the request to apply the site script
            var results = requestHelper.Post<RestResultCollection<InvokeSiteScriptActionResponse>>($"{siteUrl.TrimEnd('/')}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ExecuteTemplateScript()", postBody, new Dictionary<string, string>{{ "Accept", "application/json" }});
            return results;
        }

        #endregion
    }
}
