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
	[Cmdlet(VerbsCommon.Get, "PnPListRule")]
	[OutputType(typeof(ListRule))]
	public class GetListRule : PnPWebCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind List { get; set; }

		[Parameter(Mandatory = false)]
		public ListRulePipeBind Identity { get; set; }

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
				// Call the GetAllRules endpoint
				var endpoint = $"web/lists(guid'{list.Id}')/GetAllRules";
				var response = RestHelper.ExecutePostRequest(ClientContext, endpoint, "{}");
				var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				// Parse the response
				var jsonDoc = JsonDocument.Parse(responseContent);
				var rules = new List<ListRule>();

				if (jsonDoc.RootElement.TryGetProperty("value", out var valueElement))
				{
					foreach (var ruleElement in valueElement.EnumerateArray())
					{
						var rule = JsonSerializer.Deserialize<ListRule>(ruleElement.GetRawText(), new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						});
						rules.Add(rule);
					}
				}
				else if (jsonDoc.RootElement.TryGetProperty("d", out var dElement) && dElement.TryGetProperty("GetAllRules", out var rulesElement))
				{
					foreach (var ruleElement in rulesElement.EnumerateArray())
					{
						var rule = JsonSerializer.Deserialize<ListRule>(ruleElement.GetRawText(), new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						});
						rules.Add(rule);
					}
				}

				// Filter by Identity if specified
				if (Identity != null)
				{
					if (Identity.Id != Guid.Empty)
					{
						var rule = rules.FirstOrDefault(r => r.RuleId == Identity.Id);
						if (rule != null)
						{
							WriteObject(rule);
						}
						else
						{
							WriteWarning($"Rule with ID '{Identity.Id}' not found");
						}
					}
					else if (!string.IsNullOrEmpty(Identity.Title))
					{
						var matchingRules = rules.Where(r => r.Title.Equals(Identity.Title, StringComparison.OrdinalIgnoreCase)).ToList();
						WriteObject(matchingRules, true);
					}
				}
				else
				{
					WriteObject(rules, true);
				}
			}
			catch (Exception ex)
			{
				WriteError(new ErrorRecord(
					new Exception($"Failed to retrieve rules: {ex.Message}", ex),
					"FailedToRetrieveRules",
					ErrorCategory.ReadError,
					list));
			}
		}
	}
}
