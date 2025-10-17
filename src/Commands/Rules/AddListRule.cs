using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Rules
{
	[Cmdlet(VerbsCommon.Add, "PnPListRule")]
	[OutputType(typeof(Rule))]
	public class AddListRule : PnPWebCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind List { get; set; }

		[Parameter(Mandatory = true)]
		public string Title { get; set; }

		[Parameter(Mandatory = false)]
		public string Description { get; set; }

		[Parameter(Mandatory = true)]
		public string TriggerEventType { get; set; }

		[Parameter(Mandatory = true)]
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
		public SwitchParameter Enabled = true;

		protected override void ExecuteCmdlet()
		{
			var list = List?.GetList(CurrentWeb);
			if (list == null)
			{
				throw new PSArgumentException("Unable to retrieve the specified list", nameof(List));
			}

			list.EnsureProperty(l => l.Id);

			try
			{
				// Build the rule object
				var rule = new
				{
					title = Title,
					description = Description ?? string.Empty,
					isEnabled = Enabled.ToBool(),
					triggerCondition = new
					{
						eventType = TriggerEventType,
						condition = Condition ?? string.Empty
					},
					actionParameters = new
					{
						actionType = ActionType,
						emailRecipients = EmailRecipients ?? Array.Empty<string>(),
						emailSubject = EmailSubject ?? string.Empty,
						emailBody = EmailBody ?? string.Empty
					}
				};

				var jsonContent = JsonSerializer.Serialize(rule, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				// Call the CreateRuleEx endpoint
				var endpoint = $"web/lists(guid'{list.Id}')/CreateRuleEx";
				var response = RestHelper.ExecutePostRequest(ClientContext, endpoint, jsonContent);
				var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				// Parse and return the created rule
				var createdRule = JsonSerializer.Deserialize<Rule>(responseContent, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				WriteObject(createdRule);
			}
			catch (Exception ex)
			{
				WriteError(new ErrorRecord(
					new Exception($"Failed to create rule: {ex.Message}", ex),
					"FailedToCreateRule",
					ErrorCategory.WriteError,
					list));
			}
		}
	}
}
