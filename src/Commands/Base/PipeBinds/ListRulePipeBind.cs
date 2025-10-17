using System;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
	public sealed class ListRulePipeBind
	{
		public ListRule ListRuleInstance { get; private set; }
		private readonly Guid _id;
		private readonly string _title;

		public ListRulePipeBind(Guid guid)
		{
			_id = guid;
		}

		public ListRulePipeBind(ListRule listRule)
		{
			ListRuleInstance = listRule ?? throw new ArgumentNullException(nameof(listRule));
			_id = listRule.RuleId;
			_title = listRule.Title;
		}

		public ListRulePipeBind(string input)
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

		public Guid Id => _id;
		public string Title => _title;

		public ListRulePipeBind()
		{
			_id = Guid.Empty;
		}
	}
}
