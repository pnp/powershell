using System;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Represents a built-in Microsoft SharePoint site design returned from the SiteScriptUtility REST API (store 1)
    /// </summary>
    public class BuiltInSiteDesign
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Maps the Id back to the known BuiltInSiteTemplates enum value, if recognised
        /// </summary>
        public BuiltInSiteTemplates? Template
        {
            get
            {
                if (BuiltInSiteTemplateSettings.BuiltInSiteTemplateMappings.TryGetValue(Id, out var template))
                    return template;
                return null;
            }
        }
    }
}
