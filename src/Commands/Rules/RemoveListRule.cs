using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Rules
{
	[Cmdlet(VerbsCommon.Remove, "PnPListRule")]
	[OutputType(typeof(void))]
	public class RemoveListRule : PnPWebCmdlet
	{
		[Parameter(Mandatory = false, ValueFromPipeline = false, Position = 1)]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind List { get; set; }

		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		public ListRulePipeBind Identity { get; set; }

		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

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

			if (!Force && !ShouldContinue($"Remove rule '{Identity.Id}' from list '{list.Title}'?", Properties.Resources.Confirm))
			{
				return;
			}

			try
			{
				// Build the request body
				var deleteData = new
				{
					ruleId = Identity.Id
				};

				var jsonContent = JsonSerializer.Serialize(deleteData, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				// Call the DeleteRule endpoint
				var endpoint = $"web/lists(guid'{list.Id}')/DeleteRule";
				RestHelper.ExecutePostRequest(ClientContext, endpoint, jsonContent);

				WriteVerbose($"Rule '{Identity.Id}' successfully removed from list '{list.Title}'");
			}
			catch (Exception ex)
			{
				WriteError(new ErrorRecord(
					new Exception($"Failed to remove rule: {ex.Message}", ex),
					"FailedToRemoveRule",
					ErrorCategory.WriteError,
					list));
			}
		}
	}
}
