using PnP.PowerShell.Commands.ToDo;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PnP.PowerShell.Commands.Base.Completers
{
    /// <summary>
    /// Provides tab completion for Microsoft To Do list display names.
    /// </summary>
    public sealed class TodoListCompleter : PnPArgumentCompleter
    {
        /// <summary>
        /// Retrieves matching Microsoft To Do list display names for the current connection.
        /// </summary>
        /// <param name="commandName">Name of the command requesting completion.</param>
        /// <param name="parameterName">Name of the parameter requesting completion.</param>
        /// <param name="wordToComplete">Current word to complete.</param>
        /// <param name="commandAst">PowerShell command abstract syntax tree.</param>
        /// <param name="fakeBoundParameters">Parameters already bound in the command line.</param>
        /// <returns>Matching Microsoft To Do list completion results.</returns>
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            try
            {
                var requestHelper = new ApiRequestHelper(typeof(GetTodoList), PnPConnection.Current);
                var todoLists = requestHelper.GetResultCollection<Model.ToDo.ToDoList>("/v1.0/me/todo/lists");
                return todoLists
                    .Where(l => l.DisplayName.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase))
                    .Select(l => new CompletionResult(l.DisplayName))
                    .OrderBy(l => l.CompletionText);
            }
            catch
            {
                return [];
            }
        }
    }
}
