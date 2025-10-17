using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Rules
{
	[Cmdlet(VerbsCommon.Add, "PnPListRule")]
	[OutputType(typeof(ListRule))]
	public class AddListRule : PnPWebCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind List { get; set; }

		[Parameter(Mandatory = true)]
		public string Title { get; set; }

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
				// Build notification receivers in the expected format
				var notificationReceivers = new List<object>();
				if (EmailRecipients != null)
				{
					foreach (var email in EmailRecipients)
					{
						notificationReceivers.Add(new
						{
							name = email, // Using email as name for now - could be enhanced to parse display names
							email = email,
							userId = $"i:0#.f|membership|{email}" // Standard claims format for email users
						});
					}
				}

				var actionParams = new List<object>();
				
				// Add notification receivers if any
				if (notificationReceivers.Count > 0)
				{
					actionParams.Add(new
					{
						Key = "NotificationReceivers",
						Value = System.Text.Json.JsonSerializer.Serialize(notificationReceivers),
						ValueType = "String"
					});
				}

				// Add custom message if email body is provided
				if (!string.IsNullOrEmpty(EmailBody))
				{
					actionParams.Add(new
					{
						Key = "CustomMessage",
						Value = $"'{EmailBody}'", // Wrapped in quotes as shown in sample
						ValueType = "String"
					});
				}

				// Add email subject if provided
				if (!string.IsNullOrEmpty(EmailSubject))
				{
					actionParams.Add(new
					{
						Key = "EmailSubject",
						Value = EmailSubject,
						ValueType = "String"
					});
				}

				// Build the rule object to match the expected API format
				var rule = new
				{
					title = Title,
					condition = Condition ?? "true", // Default to "true" as shown in sample
					triggerType = int.TryParse(TriggerEventType, out int triggerTypeValue) ? triggerTypeValue : 0,
					action = new
					{
						ActionType = int.TryParse(ActionType, out int actionTypeValue) ? actionTypeValue : 0,
						ActionParams = new
						{
							results = actionParams.ToArray()
						}
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
				var createdRule = JsonSerializer.Deserialize<ListRule>(responseContent, new JsonSerializerOptions
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
