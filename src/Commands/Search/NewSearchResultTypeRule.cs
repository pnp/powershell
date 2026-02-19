using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.New, "PnPSearchResultTypeRule")]
	[OutputType(typeof(SearchResultTypeRule))]
	public class NewSearchResultTypeRule : PSCmdlet
	{
		[Parameter(Mandatory = true, Position = 0)]
		public string PropertyName;

		[Parameter(Mandatory = true, Position = 1)]
		public SearchResultTypeRuleOperatorType Operator;

		[Parameter(Mandatory = true, Position = 2)]
		public string[] Values;

		protected override void ProcessRecord()
		{
			var rule = new SearchResultTypeRule
			{
				PropertyName = PropertyName,
				Operator = new SearchResultTypeRuleOperator
				{
					N = (int)Operator,
					JBO = true
				},
				Values = new List<string>(Values)
			};

			WriteObject(rule);
		}
	}
}
