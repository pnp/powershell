namespace PnP.PowerShell.Commands.Model
{
    /// <summary>Describes a persisted login cache registration.</summary>
    public class TokenCacheConfiguration
    {
        /// <summary>Gets or sets the SharePoint tenant URL associated with the persisted login.</summary>
        public string Url { get; set; }

        /// <summary>Gets or sets the client ID associated with the persisted login.</summary>
        public string ClientId { get; set; }

        /// <summary>Gets or sets whether the persisted login uses delegated or app-only authentication.</summary>
        public string AuthenticationType { get; set; } = "Delegated";

        /// <summary>Gets or sets whether the persisted login is enabled.</summary>
        public bool Enabled { get; set; }
    }
}