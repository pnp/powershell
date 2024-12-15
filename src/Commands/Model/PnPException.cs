using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Model
{
    public class PnPException
    {
        public string Message;
        public string Stacktrace;
        public int ScriptLineNumber;
        public InvocationInfo InvocationInfo;
        public Exception Exception;
        public string CorrelationId;
        public DateTime TimeStampUtc;
    }
}
