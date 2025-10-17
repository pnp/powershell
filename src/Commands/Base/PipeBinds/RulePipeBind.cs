using System;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
	public sealed class RulePipeBind
	{
		private readonly Guid _id;
		private readonly string _title;

		public RulePipeBind(Guid guid)
		{
			_id = guid;
		}

		public RulePipeBind(string input)
		{
			if (Guid.TryParse(input, out Guid guid))
			{
				_id = guid;
			}
			else
			{
				_title = input;
			}
		}

		public RulePipeBind(Rule rule)
		{
			_id = rule.RuleId;
			_title = rule.Title;
		}

		public Guid Id => _id;
		public string Title => _title;

		public RulePipeBind()
		{
			_id = Guid.Empty;
		}
	}
}
