using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Response to adding content types to sync from the content type hub
    /// </summary>
    public class AddContentTypesFromContentTypeHubResponse
    {
        public IList<string> FailedContentTypeErrors { get; set; }

        public IList<string> FailedContentTypeIDs { get; set; }

        public Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.eFailedReason FailedReason { get; set; }

        public bool IsPassed { get; set; }

        /// <summary>
        /// The results. Similar to the root properties, for backwards compatibility.
        /// </summary>
        [JsonIgnore]
        public Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypeSyndicationResult Value { get; set; }
    }
}