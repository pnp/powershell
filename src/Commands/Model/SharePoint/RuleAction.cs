using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	/// <summary>
	/// Represents the actions to execute when a SharePoint rule is triggered
	/// </summary>
	public class RuleAction
	{
		/// <summary>
		/// The type of action to perform (e.g., "sendEmail", "createAlert")
		/// </summary>
		[JsonPropertyName("actionType")]
		public string ActionType { get; set; }

		/// <summary>
		/// The email addresses to send notifications to
		/// </summary>
		[JsonPropertyName("emailRecipients")]
		public List<string> EmailRecipients { get; set; }

		/// <summary>
		/// The subject line for email notifications
		/// </summary>
		[JsonPropertyName("emailSubject")]
		public string EmailSubject { get; set; }

		/// <summary>
		/// The body content for email notifications
		/// </summary>
		[JsonPropertyName("emailBody")]
		public string EmailBody { get; set; }

		/// <summary>
		/// Additional parameters for the action
		/// </summary>
		[JsonPropertyName("parameters")]
		public Dictionary<string, object> Parameters { get; set; }
	}
}
