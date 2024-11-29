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
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            List<CompletionResult> arguments = new List<CompletionResult>();
            IEnumerable<List> result = ClientContext.LoadQuery(ClientContext.Web.Lists.Include(list => list.Title, list => list.RootFolder.Name));
            ClientContext.ExecuteQueryRetry();
            foreach (var list in result.Where(l => l.Title.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase) || l.RootFolder.Name.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (list.RootFolder.Name != list.Title)
                {
                    arguments.Add(new CompletionResult(list.RootFolder.Name));
                }
                arguments.Add(new CompletionResult(list.Title));
            }
            return arguments.OrderBy(l => l.CompletionText);
        }
    }
}