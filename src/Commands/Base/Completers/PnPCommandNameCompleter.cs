using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class PnPCommandNameCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            return CommandPermissionHelper.GetCommandNames()
                .Where(name => name.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase))
                .Select(name => new CompletionResult(name));
        }
    }
}
