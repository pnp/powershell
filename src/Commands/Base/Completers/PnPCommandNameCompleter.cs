using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PnP.PowerShell.Commands.Base.Completers
{
	public sealed class PnPCommandNameCompleter : PnPArgumentCompleter
	{
		protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
		{
			return typeof(BasePSCmdlet).Assembly.GetTypes()
				.Select(type => type.GetCustomAttributes(typeof(CmdletAttribute), false).FirstOrDefault() as CmdletAttribute)
				.Where(attribute => attribute != null)
				.Select(attribute => $"{attribute.VerbName}-{attribute.NounName}")
				.Where(name => name.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase))
				.OrderBy(name => name)
				.Select(name => new CompletionResult(name));
		}
	}
}