using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class PageCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            List<CompletionResult> results = new List<CompletionResult>();
            wordToComplete = wordToComplete.Replace('\\', '/');
            var pages = PnPConnection.Current.PnPContext.Web.GetPages(wordToComplete.TrimStart('/'));
            foreach (var page in pages.OrderBy(p => p.Name))
            {
                var result = string.IsNullOrEmpty(page.Folder) ? page.Name : page.Folder + "/" + page.Name;
                results.Add(new CompletionResult(result));
            }
            return results;
        }
    }

}