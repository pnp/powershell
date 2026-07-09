using PnP.PowerShell.Commands.Utilities.REST;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Online.SharePoint.TenantAdministration;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
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
        /// <param name="requestHelper">Helper that can be used to make HTTP requests</param>
        /// <param name="script">The Site Script to invoke</param>
        /// <param name="siteUrl">The URL of the SharePoint site to invoke the Site Script on</param>
        public static RestResultCollection<InvokeSiteScriptActionResponse> InvokeSiteScript(ApiRequestHelper requestHelper, TenantSiteScript script, string siteUrl)
        {
            return InvokeSiteScript(requestHelper, script.Content, siteUrl);
        }

        /// <summary>
        /// Invokes the provided site script on the provided site
        /// </summary>
        /// <param name="requestHelper">Helper that can be used to make HTTP requests</param>
        /// <param name="scriptContent">The Site Script content to invoke</param>
        /// <param name="siteUrl">The URL of the SharePoint site to invoke the Site Script on</param>
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

        #region Built-in Site Designs (store 1)

        /// <summary>
        /// Returns all built-in Microsoft site designs from the SharePoint SiteScriptUtility store (store 1)
        /// </summary>
        /// <param name="requestHelper">Helper that can be used to make HTTP requests</param>
        /// <param name="siteUrl">Any SharePoint site URL used to scope the REST request</param>
        public static RestResultCollection<BuiltInSiteDesign> GetBuiltInSiteDesigns(ApiRequestHelper requestHelper, string siteUrl)
        {
            var postBody = new StringContent(@"{""store"": 1}");
            postBody.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return requestHelper.Post<RestResultCollection<BuiltInSiteDesign>>(
                $"{siteUrl.TrimEnd('/')}/_api/Microsoft.SharePoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns",
                postBody,
                new Dictionary<string, string> { { "Accept", "application/json" } });
        }

        /// <summary>
        /// Applies a built-in Microsoft site design (store 1) to the given site via the SiteScriptUtility REST API
        /// </summary>
        /// <param name="requestHelper">Helper that can be used to make HTTP requests</param>
        /// <param name="siteDesignId">The GUID of the built-in site design to apply</param>
        /// <param name="webUrl">The URL of the SharePoint site to apply the design to</param>
        public static RestResultCollection<InvokeSiteScriptActionResponse> ApplyBuiltInSiteDesign(ApiRequestHelper requestHelper, Guid siteDesignId, string webUrl)
        {
            var postBody = new StringContent($@"{{""siteDesignId"": ""{siteDesignId}"", ""webUrl"": ""{webUrl}"", ""store"": 1}}");
            postBody.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return requestHelper.Post<RestResultCollection<InvokeSiteScriptActionResponse>>(
                $"{webUrl.TrimEnd('/')}/_api/Microsoft.SharePoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign",
                postBody,
                new Dictionary<string, string> { { "Accept", "application/json" } });
        }

        #endregion
    }
}
