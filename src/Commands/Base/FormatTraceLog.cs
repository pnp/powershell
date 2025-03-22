using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Format, "PnPTraceLog")]
    public class FormatTraceLog : BasePSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public string LogLine;

        protected override void ProcessRecord()
        {
            LogLine = LogLine.Replace(System.Environment.NewLine," ");
            var items = LogLine.Split('\t');
            WriteObject(new TraceLogEntry(items));
        }
    }
}