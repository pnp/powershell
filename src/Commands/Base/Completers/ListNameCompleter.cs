using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class ListNameCompleter : PnPArgumentCompleter
    {
        public override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters, char quoteChar)
        {
            IEnumerable<List> result = PnPConnection.Current.Context.LoadQuery(PnPConnection.Current.Context.Web.Lists.Include(list => list.Title));
            PnPConnection.Current.Context.ExecuteQueryRetry();
            foreach (var list in result.Where(l => l.Title.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                var listTitle = list.Title;
                if(quoteChar == '"')
                {
                   listTitle = list.Title.Replace("\"","`\"");
                }
                yield return new CompletionResult($"{quoteChar}{listTitle}{quoteChar}");
            }

        }
    }
}