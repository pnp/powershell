using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Copilot
{
    public class CopilotAgent
    {
        [JsonPropertyName("schemaVersion")]
        public string SchemaVersion { get; set; } = "0.2.0";

        [JsonPropertyName("customCopilotConfig")]
        public CopilotAgentCustomCopilotConfig CustomCopilotConfig { get; set; } = new CopilotAgentCustomCopilotConfig();

        public string ServerRelativeUrl { get; set; }

        public CopilotAgentType AgentType
        {
            get
            {
                if (ServerRelativeUrl.Contains("siteassets/copilot", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return CopilotAgentType.Site;
                }
                else
                {
                    return CopilotAgentType.DocumentLibrary;
                }
            }
        }
    }

    public enum CopilotAgentType
    {
        Site,
        DocumentLibrary
    }

    public class CopilotAgentCustomCopilotConfig
    {
        [JsonPropertyName("conversationStarters")]
        public CopilotAgentConversationStarters ConversationStarters { get; set; } = new CopilotAgentConversationStarters();

        [JsonPropertyName("gptDefinition")]
        public CopilotAgentGPTDefinition GPTDefinition { get; set; } = new CopilotAgentGPTDefinition();

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }

    public class CopilotAgentConversationStarters
    {
        [JsonPropertyName("conversationStarterList")]
        public List<CopilotAgentTextValue> conversationStarterList { get; set; } = new List<CopilotAgentTextValue>();

        [JsonPropertyName("welcomeMessage")]
        public CopilotAgentTextValue WelcomeMessage { get; set; } = new CopilotAgentTextValue();
    }

    public class CopilotAgentTextValue
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class CopilotAgentGPTDefinition
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        [JsonPropertyName("capabilities")]
        public List<CopilotAgentCapabilities> Capabilities { get; set; } = new List<CopilotAgentCapabilities>();
    }

    public class CopilotAgentCapabilities
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("items_by_sharepoint_ids")]
        public List<CopilotAgentSourceItem> ItemsBySharePointIds { get; set; } = new List<CopilotAgentSourceItem>();

        [JsonPropertyName("items_by_url")]
        public List<CopilotAgentSourceItem> ItemsByUrl { get; set; } = new List<CopilotAgentSourceItem>();
    }

    public class CopilotAgentSourceItem
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("site_id")]
        public string SiteId { get; set; }
        [JsonPropertyName("web_id")]
        public string WebId { get; set; }
        [JsonPropertyName("list_id")]
        public string ListId { get; set; }
        [JsonPropertyName("unique_id")]
        public string UniqueId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}