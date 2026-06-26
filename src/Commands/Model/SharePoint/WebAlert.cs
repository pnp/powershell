using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Represents a SharePoint web alert retrieved through REST API
    /// </summary>
    public class WebAlert
    {
        /// <summary>
        /// Unique identifier of the alert
        /// </summary>
        [JsonPropertyName("ID")]
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the alert
        /// </summary>
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// The type of alert (List, ListItem, etc.)
        /// </summary>
        [JsonPropertyName("AlertType")]
        public int AlertType { get; set; }

        /// <summary>
        /// The name of the alert template
        /// </summary>
        [JsonPropertyName("AlertTemplateName")]
        public string AlertTemplateName { get; set; }

        /// <summary>
        /// The event type that triggers the alert
        /// </summary>
        [JsonPropertyName("EventType")]
        public int EventType { get; set; }

        /// <summary>
        /// The frequency of alert notifications
        /// </summary>
        [JsonPropertyName("AlertFrequency")]
        public int AlertFrequency { get; set; }

        /// <summary>
        /// Whether the user should always be notified
        /// </summary>
        [JsonPropertyName("AlwaysNotify")]
        public bool AlwaysNotify { get; set; }

        /// <summary>
        /// The delivery channels for the alert
        /// </summary>
        [JsonPropertyName("DeliveryChannels")]
        public int DeliveryChannels { get; set; }

        /// <summary>
        /// The filter applied to the alert
        /// </summary>
        [JsonPropertyName("Filter")]
        public string Filter { get; set; }

        /// <summary>
        /// The status of the alert
        /// </summary>
        [JsonPropertyName("Status")]
        public int Status { get; set; }

        /// <summary>
        /// The item ID that the alert is associated with (if applicable)
        /// </summary>
        [JsonPropertyName("ItemID")]
        public int ItemId { get; set; }

        /// <summary>
        /// The user ID that owns the alert
        /// </summary>
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        /// <summary>
        /// Additional properties of the alert
        /// </summary>
        [JsonPropertyName("Properties")]
        public List<WebAlertProperty> Properties { get; set; }

        /// <summary>
        /// The user information
        /// </summary>
        [JsonPropertyName("User")]
        public WebAlertUser User { get; set; }

        /// <summary>
        /// The list information
        /// </summary>
        [JsonPropertyName("List")]
        public WebAlertList List { get; set; }

        /// <summary>
        /// The item information
        /// </summary>
        [JsonPropertyName("Item")]
        public WebAlertItem Item { get; set; }

        /// <summary>
        /// The User Principal Name of the user - computed property
        /// </summary>
        [JsonIgnore]
        public string UserPrincipalName => User?.UserPrincipalName;
    }

    /// <summary>
    /// Represents a property key-value pair for an alert
    /// </summary>
    public class WebAlertProperty
    {
        /// <summary>
        /// Property key
        /// </summary>
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        /// <summary>
        /// Property value
        /// </summary>
        [JsonPropertyName("Value")]
        public string Value { get; set; }

        /// <summary>
        /// Property value type
        /// </summary>
        [JsonPropertyName("ValueType")]
        public string ValueType { get; set; }
    }

    /// <summary>
    /// Represents user information for an alert
    /// </summary>
    public class WebAlertUser
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// User Principal Name (email)
        /// </summary>
        [JsonPropertyName("UserPrincipalName")]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// User title/display name
        /// </summary>
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// User login name
        /// </summary>
        [JsonPropertyName("LoginName")]
        public string LoginName { get; set; }
    }

    /// <summary>
    /// Represents list information for an alert
    /// </summary>
    public class WebAlertList
    {
        /// <summary>
        /// List ID
        /// </summary>
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// List title
        /// </summary>
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Root folder information
        /// </summary>
        [JsonPropertyName("RootFolder")]
        public WebAlertRootFolder RootFolder { get; set; }
    }

    /// <summary>
    /// Represents root folder information for a list
    /// </summary>
    public class WebAlertRootFolder
    {
        /// <summary>
        /// Server relative URL of the root folder
        /// </summary>
        [JsonPropertyName("ServerRelativeUrl")]
        public string ServerRelativeUrl { get; set; }
    }

    /// <summary>
    /// Represents item information for an alert
    /// </summary>
    public class WebAlertItem
    {
        /// <summary>
        /// Item ID
        /// </summary>
        [JsonPropertyName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// File reference (server relative URL)
        /// </summary>
        [JsonPropertyName("FileRef")]
        public string FileRef { get; set; }

        /// <summary>
        /// Item GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public Guid Guid { get; set; }
    }
}
