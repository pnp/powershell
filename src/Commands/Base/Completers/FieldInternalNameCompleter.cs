using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class FieldInternalNameCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            IEnumerable<Field> result = ClientContext.LoadQuery(PnPConnection.Current.Context.Web.AvailableFields.Include(f => f.InternalName));
            ClientContext.ExecuteQueryRetry();
            foreach (var field in result.Where(l => l.InternalName.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult(field.InternalName);
            }
        }
    }
}