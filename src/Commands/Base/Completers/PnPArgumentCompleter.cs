using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Extensions;

public abstract class PnPArgumentCompleter : IArgumentCompleter
{
    public static Microsoft.SharePoint.Client.ClientContext ClientContext => PnPConnection.Current.Context;

    public static PnP.Core.Services.PnPContext PnPContext => PnPConnection.Current.PnPContext;

    private const int Timeout = 2000;
    
    public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
    {
        var quoteChar = '"';
        if (wordToComplete.StartsWith('\''))
        {
            quoteChar = '\'';
        }
        wordToComplete = wordToComplete.Trim(['"', '\'']);
        var task = Task.Run(() => GetArguments(commandName, parameterName, wordToComplete, commandAst, fakeBoundParameters));

        var results = task.TimeoutAfter(TimeSpan.FromMilliseconds(GetTimeOut())).GetAwaiter().GetResult();
        foreach (var result in results)
        {
            var completionText = result.CompletionText;
            if (quoteChar == '"')
            {
                completionText = completionText.Replace("\"", "`\"");
            }
            else if (quoteChar == '\'')
            {
                completionText = completionText.Replace("'", "`'");
            }
            yield return new CompletionResult($"{quoteChar}{completionText}{quoteChar}", result.ListItemText, result.ResultType, result.ToolTip);
        }
    }

    protected virtual IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
    {
        return null;
    }

    private Int32 GetTimeOut()
    {
        var timeOutFromEnv = Environment.GetEnvironmentVariable("PNPPSCOMPLETERTIMEOUT");
        if (!string.IsNullOrEmpty(timeOutFromEnv))
        {
            try
            {
                return Convert.ToInt32(timeOutFromEnv);
            }
            catch (FormatException)
            {
                return Timeout;
            }
        }
        else
        {
            return Timeout;
        }
    }
}