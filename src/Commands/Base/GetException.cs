
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPException")]
    [OutputType(typeof(PnPException))]
    public class GetException : BasePSCmdlet
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
                    for (var x = 0; x < exceptions.Count; x++)
                    {
                        var exception = exceptions[x] as ErrorRecord;
                        if (exception == null) continue;

                        var correlationId = string.Empty;
                        if (exception.Exception.Data.Contains("CorrelationId"))
                        {
                            correlationId = exception.Exception.Data["CorrelationId"]?.ToString();
                        }
                        var timeStampUtc = DateTime.MinValue;
                        if (exception.Exception.Data.Contains("TimeStampUtc") && exception.Exception.Data["TimeStampUtc"] != null)
                        {
                            timeStampUtc = (DateTime)exception.Exception.Data["TimeStampUtc"];
                        }
                        output.Add(new PnPException() { CorrelationId = correlationId, TimeStampUtc = timeStampUtc, Message = exception.Exception.Message, Stacktrace = exception.Exception.StackTrace, ScriptLineNumber = exception.InvocationInfo.ScriptLineNumber, InvocationInfo = exception.InvocationInfo, Exception = exception.Exception });
                    }
                }
                else
                {
                    var exceptionObject = exceptions.ToArray().FirstOrDefault(e => e is ErrorRecord);
                    if(exceptionObject == null) return;

                    var exception = (ErrorRecord) exceptionObject;

                    var correlationId = string.Empty;
                    if (exception.Exception.Data.Contains("CorrelationId") && exception.Exception.Data["CorrelationId"] != null)
                    {
                        correlationId = exception.Exception.Data["CorrelationId"]?.ToString();
                    }
                    var timeStampUtc = DateTime.MinValue;
                    if (exception.Exception.Data.Contains("TimeStampUtc") && exception.Exception.Data["TimeStampUtc"] != null)
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
