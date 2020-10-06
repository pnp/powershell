
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "Exception")]
    public class GetException : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter All;

        protected override void ProcessRecord()
        {
            var exceptions = (ArrayList)this.SessionState.PSVariable.Get("error").Value;
            if (exceptions.Count > 0)
            {
                var output = new List<PnPException>();
                if (All.IsPresent)
                {
                    foreach (ErrorRecord exception in exceptions)
                    {

                        var correlationId = string.Empty;
                        if (exception.Exception.Data.Contains("CorrelationId"))
                        {
                            correlationId = exception.Exception.Data["CorrelationId"].ToString();
                        }
                        var timeStampUtc = DateTime.MinValue;
                        if (exception.Exception.Data.Contains("TimeStampUtc"))
                        {
                            timeStampUtc = (DateTime)exception.Exception.Data["TimeStampUtc"];
                        }
                        output.Add(new PnPException() { CorrelationId = correlationId, TimeStampUtc = timeStampUtc, Message = exception.Exception.Message, Stacktrace = exception.Exception.StackTrace, ScriptLineNumber = exception.InvocationInfo.ScriptLineNumber, InvocationInfo = exception.InvocationInfo, Exception = exception.Exception });
                    }
                }
                else
                {
                    var exception = (ErrorRecord)exceptions[0];
                    var correlationId = string.Empty;
                    if (exception.Exception.Data.Contains("CorrelationId"))
                    {
                        correlationId = exception.Exception.Data["CorrelationId"].ToString();
                    }
                    var timeStampUtc = DateTime.MinValue;
                    if (exception.Exception.Data.Contains("TimeStampUtc"))
                    {
                        timeStampUtc = (DateTime)exception.Exception.Data["TimeStampUtc"];
                    }
                    output.Add(new PnPException() { CorrelationId = correlationId, TimeStampUtc = timeStampUtc, Message = exception.Exception.Message, Stacktrace = exception.Exception.StackTrace, ScriptLineNumber = exception.InvocationInfo.ScriptLineNumber, InvocationInfo = exception.InvocationInfo, Exception = exception.Exception });
                }
                WriteObject(output, true);
            }
        }
    }
}
