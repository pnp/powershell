using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPDiagnostics")]
    [OutputType(typeof(Diagnostics))]
    public class GetDiagnostics : BasePSCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = new Diagnostics();

            FillVersion(result);
            FillModuleInfo(result);
            FillOperatingSystem(result);
            FillConnectionMethod(result);
            FillCurrentSite(result);
            FillNewerVersionAvailable(result);
            FillLastException(result);

            WriteObject(result, true);
        }

        void FillVersion(Diagnostics result)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = ((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version;
            result.Version = version;
        }

        void FillModuleInfo(Diagnostics result)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var escapedLocation = Uri.UnescapeDataString(location);

            var modulePath = System.IO.Path.GetDirectoryName(escapedLocation);
            DirectoryInfo dirInfo = new DirectoryInfo(modulePath);

            result.ModulePath = modulePath;
        }

        void FillOperatingSystem(Diagnostics result)
        {
            result.OperatingSystem = Environment.OSVersion.VersionString;
        }

        void FillConnectionMethod(Diagnostics result)
        {
            result.ConnectionMethod = PnPConnection.Current?.ConnectionMethod;
        }

        void FillCurrentSite(Diagnostics result)
        {
            result.CurrentSite = PnPConnection.Current?.Url;
        }

        void FillNewerVersionAvailable(Diagnostics result)
        {
            var versionAvailable = VersionChecker.GetAvailableVersion();
            if (versionAvailable != null && VersionChecker.IsNewer(versionAvailable.Version))
            {
                result.NewerVersionAvailable = versionAvailable.ToString();
            }
        }

        void FillLastException(Diagnostics result)
        {
            // Most of this code has been copied from GetException cmdlet
            PnPException pnpException = null;
            var exceptions = (ArrayList)this.SessionState.PSVariable.Get("error").Value;
            var exception = (ErrorRecord)(exceptions.ToArray().FirstOrDefault(e => e is ErrorRecord));
            if (exception != null)
            {
                var correlationId = string.Empty;
                if (exception.Exception.Data.Contains("CorrelationId"))
                {
                    correlationId = exception.Exception.Data["CorrelationId"]?.ToString();
                }
                var timeStampUtc = DateTime.MinValue;
                if (exception.Exception.Data.Contains("TimeStampUtc"))
                {
                    timeStampUtc = (DateTime)exception.Exception.Data["TimeStampUtc"];
                }
                pnpException = new PnPException() { CorrelationId = correlationId, TimeStampUtc = timeStampUtc, Message = exception.Exception.Message, Stacktrace = exception.Exception.StackTrace, ScriptLineNumber = exception.InvocationInfo.ScriptLineNumber, InvocationInfo = exception.InvocationInfo, Exception = exception.Exception };

            }

            result.LastCorrelationId = pnpException?.CorrelationId;
            result.LastExceptionTimeStampUtc = pnpException?.TimeStampUtc;
            result.LastExceptionMessage = pnpException?.Message;
            result.LastExceptionStacktrace = pnpException?.Stacktrace;
            result.LastExceptionScriptLineNumber = pnpException?.ScriptLineNumber;
        }
    }
}