using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Threading;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Extensions;

public abstract class PnPArgumentCompleter : IArgumentCompleter
{
    private const int Timeout = 2000;
    public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
    {
        var quoteChar = '"';
        if(wordToComplete.StartsWith('\''))
        {
            quoteChar = '\'';
        }
        wordToComplete = wordToComplete.Trim(['"', '\'']);
        var task = Task.Run(() => GetArguments(commandName, parameterName, wordToComplete, commandAst, fakeBoundParameters, quoteChar));
        return task.TimeoutAfter(TimeSpan.FromMilliseconds(GetTimeOut())).GetAwaiter().GetResult();
    }

    public abstract IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters, char quoteChar);

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