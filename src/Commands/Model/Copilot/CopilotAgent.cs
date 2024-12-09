using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Copilot
{
    public class CopilotAgent
    {
        [JsonPropertyName("schemaVersion")]
        public string SchemaVersion { get; set; } = "0.2.0";

        [JsonPropertyName("customCopilotConfig")]
        public CopilotAgentCustomCopilotConfig CustomCopilotConfig { get; set; }

        public string ServerRelativeUrl {get;set;}
    }

    public class CopilotAgentCustomCopilotConfig
    {
        [JsonPropertyName("conversationStarters")]
        public CopilotAgentConversationStarters ConversationStarters { get; set; }

        [JsonPropertyName("gptDefinition")]
        public CopilotAgentGPTDefinition GPTDefinition { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }

    public class CopilotAgentConversationStarters
    {
        [JsonPropertyName("conversationStarterList")]
        public List<CopilotAgentTextValue> conversationStarterList { get; set; }

        [JsonPropertyName("welcomeMessage")]
        public CopilotAgentTextValue WelcomeMessage { get; set; }
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
        public List<CopilotAgentCapabilities> Capabilities { get; set; }
    }

    public class CopilotAgentCapabilities
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("items_by_sharepoint_ids")]
        public List<CopilotAgentSourceItem> ItemsBySharePointIds { get; set; }

        [JsonPropertyName("items_by_url")]
        public List<CopilotAgentSourceItem> ItemsByUrl { get; set; }
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