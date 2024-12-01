
using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Microsoft.PowerShell.Cmdletization.Xml;

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

        public class TraceLogEntry
        {
            public DateTime TimeStamp;
            public string Category;
            // public string Three;
            public string Level;
            public string Message;
            // public string Six;
            // public string Seven;
            public TraceLogEntry(string[] values)
            {
                TimeStamp = DateTime.Parse(values[0].Split(" : ")[1]);
                Category= values[1].Trim('[',']');
                // Three = values[2];
                Level = values[3].Trim('[',']');
                Message = values[4];
                // Six = values[5];
                // Seven = values[6];
            }
        }
    }
}