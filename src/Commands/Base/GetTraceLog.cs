using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.PowerShell.Cmdletization.Xml;
using PnP.PowerShell.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPTraceLog")]
    public class GetTraceLog : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "On")]
        public string Path;

        protected override void ProcessRecord()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (File.Exists(Path))
            {
                var lines = System.IO.File.ReadAllLines(Path);
                foreach (var line in lines)
                {
                    var items = line.Split('\t');
                    WriteObject(new TraceLogEntry(items), true);
                }
            } else {
                throw new PSArgumentException($"File {Path} does not exist");
            }
        }

       
    }
}