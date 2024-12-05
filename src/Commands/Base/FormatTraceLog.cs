using System.Management.Automation;
using PnP.PowerShell.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Format, "PnPTraceLog")]
    public class FormatTraceLog : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "On")]
        public string LogLine;

        protected override void ProcessRecord()
        {
            LogLine = LogLine.Replace(System.Environment.NewLine," ");
            var items = LogLine.Split('\t');
            WriteObject(new TraceLogEntry(items));
        }
    }
}