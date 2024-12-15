using System;

namespace PnP.PowerShell.Commands.Model
{
    public sealed class Diagnostics
    {
        public string Version { get; set; }
        public string ModulePath { get; set; }
        public string OperatingSystem { get; set; }
        public ConnectionMethod? ConnectionMethod { get; set; }
        public string CurrentSite { get; set; }
        public string NewerVersionAvailable { get; set; }
        public string/*?*/ LastCorrelationId { get; set; }
        public DateTime? LastExceptionTimeStampUtc { get; set; }
        public string/*?*/ LastExceptionMessage { get; set; }
        public string/*?*/ LastExceptionStacktrace { get; set; }
        public int? LastExceptionScriptLineNumber { get; set; }

    }
}
