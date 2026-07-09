using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Mail;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class ToDoTask
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public Body Body { get; set; }

        [JsonPropertyName("bodyLastModifiedDateTime")]
        public System.DateTime? BodyLastModifiedDateTime { get; set; }

        [JsonPropertyName("categories")]
        public string[] Categories { get; set; }

        [JsonPropertyName("completedDateTime")]
        public DateTimeTimeZone CompletedDateTime { get; set; }

        [JsonPropertyName("createdDateTime")]
        public System.DateTime? CreatedDateTime { get; set; }

        [JsonPropertyName("dueDateTime")]
        public DateTimeTimeZone DueDateTime { get; set; }

        [JsonPropertyName("hasAttachments")]
        public bool? HasAttachments { get; set; }

        [JsonPropertyName("importance")]
        public ToDoTaskImportance? Importance { get; set; }

        [JsonPropertyName("isReminderOn")]
        public bool? IsReminderOn { get; set; }

        [JsonPropertyName("lastModifiedDateTime")]
        public System.DateTime? LastModifiedDateTime { get; set; }

        [JsonPropertyName("reminderDateTime")]
        public DateTimeTimeZone ReminderDateTime { get; set; }

        [JsonPropertyName("startDateTime")]
        public DateTimeTimeZone StartDateTime { get; set; }

        [JsonPropertyName("status")]
        public ToDoTaskStatus? Status { get; set; }
    }
}
