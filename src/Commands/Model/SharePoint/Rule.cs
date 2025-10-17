using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	/// <summary>
	/// Represents a SharePoint list or library rule
	/// </summary>
	public class Rule
	{
		/// <summary>
		/// The unique identifier of the rule
		/// </summary>
		[JsonPropertyName("ruleId")]
		public Guid RuleId { get; set; }

		/// <summary>
		/// The title/name of the rule
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// The description of the rule
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }

		/// <summary>
		/// Whether the rule is enabled or disabled
		/// </summary>
		[JsonPropertyName("isEnabled")]
		public bool IsEnabled { get; set; }

		/// <summary>
		/// The trigger conditions for the rule
		/// </summary>
		[JsonPropertyName("triggerCondition")]
		public RuleTrigger TriggerCondition { get; set; }

		/// <summary>
		/// The actions to execute when the rule is triggered
		/// </summary>
		[JsonPropertyName("actionParameters")]
		public RuleAction ActionParameters { get; set; }

		/// <summary>
		/// The creation date of the rule
		/// </summary>
		[JsonPropertyName("createdDate")]
		public DateTime? CreatedDate { get; set; }

		/// <summary>
		/// The last modified date of the rule
		/// </summary>
		[JsonPropertyName("lastModifiedDate")]
		public DateTime? LastModifiedDate { get; set; }

		/// <summary>
		/// The user who created the rule
		/// </summary>
		[JsonPropertyName("createdBy")]
		public string CreatedBy { get; set; }

		/// <summary>
		/// The user who last modified the rule
		/// </summary>
		[JsonPropertyName("modifiedBy")]
		public string ModifiedBy { get; set; }
	}
}
