using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Rules
{
	[Cmdlet(VerbsCommon.Set, "PnPListRule")]
	[OutputType(typeof(ListRule))]
	public class SetListRule : PnPWebCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind List { get; set; }

		[Parameter(Mandatory = true)]
		public ListRulePipeBind Identity { get; set; }

		[Parameter(Mandatory = false)]
		public string Title { get; set; }

		[Parameter(Mandatory = false)]
		public string Description { get; set; }

		[Parameter(Mandatory = false)]
		public string TriggerEventType { get; set; }

		[Parameter(Mandatory = false)]
		public string ActionType { get; set; }

		[Parameter(Mandatory = false)]
		public string[] EmailRecipients { get; set; }

		[Parameter(Mandatory = false)]
		public string EmailSubject { get; set; }

		[Parameter(Mandatory = false)]
		public string EmailBody { get; set; }

		[Parameter(Mandatory = false)]
		public string Condition { get; set; }

		[Parameter(Mandatory = false)]
		public bool? Enabled { get; set; }

		protected override void ExecuteCmdlet()
		{
			var list = List?.GetList(CurrentWeb);
			if (list == null)
			{
				throw new PSArgumentException("Unable to retrieve the specified list", nameof(List));
			}

			list.EnsureProperty(l => l.Id);

			if (Identity == null || Identity.Id == Guid.Empty)
			{
				throw new PSArgumentException("Identity must be specified with a valid Rule ID", nameof(Identity));
			}

			try
			{
				// Build the update object with only specified parameters
				var updateData = new System.Collections.Generic.Dictionary<string, object>();

				if (ParameterSpecified(nameof(Title)))
					updateData["title"] = Title;

				if (ParameterSpecified(nameof(Description)))
					updateData["description"] = Description;

				if (ParameterSpecified(nameof(Enabled)))
					updateData["isEnabled"] = Enabled.Value;

				if (ParameterSpecified(nameof(TriggerEventType)) || ParameterSpecified(nameof(Condition)))
				{
					var triggerCondition = new System.Collections.Generic.Dictionary<string, object>();
					if (ParameterSpecified(nameof(TriggerEventType)))
						triggerCondition["eventType"] = TriggerEventType;
					if (ParameterSpecified(nameof(Condition)))
						triggerCondition["condition"] = Condition;
					
					updateData["triggerCondition"] = triggerCondition;
				}

				if (ParameterSpecified(nameof(ActionType)) || ParameterSpecified(nameof(EmailRecipients)) || 
					ParameterSpecified(nameof(EmailSubject)) || ParameterSpecified(nameof(EmailBody)))
				{
					var actionParameters = new System.Collections.Generic.Dictionary<string, object>();
					if (ParameterSpecified(nameof(ActionType)))
						actionParameters["actionType"] = ActionType;
					if (ParameterSpecified(nameof(EmailRecipients)))
						actionParameters["emailRecipients"] = EmailRecipients;
					if (ParameterSpecified(nameof(EmailSubject)))
						actionParameters["emailSubject"] = EmailSubject;
					if (ParameterSpecified(nameof(EmailBody)))
						actionParameters["emailBody"] = EmailBody;
					
					updateData["actionParameters"] = actionParameters;
				}

				updateData["ruleId"] = Identity.Id;

				var jsonContent = JsonSerializer.Serialize(updateData, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				// Call the UpdateRule endpoint
				var endpoint = $"web/lists(guid'{list.Id}')/UpdateRule";
				var response = RestHelper.ExecutePostRequest(ClientContext, endpoint, jsonContent);
				var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				// Parse and return the updated rule
				var updatedRule = JsonSerializer.Deserialize<ListRule>(responseContent, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				WriteObject(updatedRule);
			}
			catch (Exception ex)
			{
				WriteError(new ErrorRecord(
					new Exception($"Failed to update rule: {ex.Message}", ex),
					"FailedToUpdateRule",
					ErrorCategory.WriteError,
					list));
			}
		}
	}
}
