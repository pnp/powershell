using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class ToDoList
    {
        /// <summary>
        /// Unique identifier of the Todo task list
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the Todo task list
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// True if the user is owner of the given task list.
        /// </summary>
        [JsonPropertyName("isOwner")]
        public bool IsOwner { get; set; }

        /// <summary>
        /// True if the task list is shared with other users
        /// </summary>
        [JsonPropertyName("isShared")]
        public bool IsShared { get; set; }

        /// <summary>
        /// True if the task list is shared with other users
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("wellknownListName")]
        public WellknownListName WellknownListName { get; set; }
    }

    public enum WellknownListName
    {
        None,
        DefaultList,
        FlaggedEmails,
        UnknownFutureValue
    }
}
