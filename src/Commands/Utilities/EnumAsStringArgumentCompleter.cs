using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Allow argument completion of enums without prohibiting undefined values. 
    /// ie. to allow for future extension
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    internal sealed class EnumAsStringArgumentCompleter<TEnum> : IArgumentCompleter where TEnum : struct, Enum
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            return Enum.GetNames(typeof(TEnum))
                .Where(x => x.StartsWith(wordToComplete, StringComparison.OrdinalIgnoreCase))
                .Select(x => new CompletionResult(x, x, CompletionResultType.ParameterValue, x));
        }
    }
}