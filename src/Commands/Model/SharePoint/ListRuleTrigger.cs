using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	/// <summary>
	/// Represents the trigger conditions for a SharePoint rule
	/// </summary>
	public class ListRuleTrigger
	{
		/// <summary>
		/// The type of event that triggers the rule (e.g., "create", "update", "delete")
		/// </summary>
		[JsonPropertyName("eventType")]
		public string EventType { get; set; }

		/// <summary>
		/// Optional conditions that must be met for the rule to trigger
		/// </summary>
		[JsonPropertyName("condition")]
		public string Condition { get; set; }

		/// <summary>
		/// Specifies which fields to monitor for changes
		/// </summary>
		[JsonPropertyName("fieldValues")]
		public object FieldValues { get; set; }
	}
}
