using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class ListNameCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            wordToComplete = wordToComplete.Trim('"');
          
            IEnumerable<List> result = PnPConnection.Current.Context.LoadQuery(PnPConnection.Current.Context.Web.Lists.Include(list => list.Title));
            PnPConnection.Current.Context.ExecuteQueryRetry();
            foreach (var list in result.Where(l => l.Title.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult($"\"{list.Title.Replace("\"","`\"")}\"");
            }

        }
    }
}