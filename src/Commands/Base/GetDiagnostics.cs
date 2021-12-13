using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPDiagnostics")]
    public class GetDiagnostics : BasePSCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = new PSObject();

            FillVersion(result);
            FillModuleInfo(result);
            FillOperatingSystem(result);
            FillConnectionMethod(result);
            FillCurrentSite(result);
            FillNewerVersionAvailable(result);
            FillLastException(result);

            WriteObject(result, true);
        }

        void FillVersion(PSObject result)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = ((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version;
            AddProperty(result, "Version", version);
        }

        void FillModuleInfo(PSObject result)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var escapedLocation = Uri.UnescapeDataString(location);

            var modulePath = System.IO.Path.GetDirectoryName(escapedLocation);
            DirectoryInfo dirInfo = new DirectoryInfo(modulePath);

            AddProperty(result, "ModulePath", modulePath);
        }

        void FillOperatingSystem(PSObject result)
        {
            AddProperty(result, "OperatingSystem", Environment.OSVersion.VersionString);
        }

        void FillConnectionMethod(PSObject result)
        {
            AddProperty(result, "ConnectionMethod", PnPConnection.Current?.ConnectionMethod);
        }

        void FillCurrentSite(PSObject result)
        {
            AddProperty(result, "CurrentSite", PnPConnection.Current?.Url);
        }

        void FillNewerVersionAvailable(PSObject result)
        {
            var versionAvailable = VersionChecker.GetAvailableVersion();
            if (versionAvailable != null && VersionChecker.IsNewer(versionAvailable))
            {
                AddProperty(result, "NewerVersionAvailable", versionAvailable.ToString());
            }
        }

        void FillLastException(PSObject result)
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
                    correlationId = exception.Exception.Data["CorrelationId"].ToString();
                }
                var timeStampUtc = DateTime.MinValue;
                if (exception.Exception.Data.Contains("TimeStampUtc"))
                {
                    timeStampUtc = (DateTime)exception.Exception.Data["TimeStampUtc"];
                }
                pnpException = new PnPException() { CorrelationId = correlationId, TimeStampUtc = timeStampUtc, Message = exception.Exception.Message, Stacktrace = exception.Exception.StackTrace, ScriptLineNumber = exception.InvocationInfo.ScriptLineNumber, InvocationInfo = exception.InvocationInfo, Exception = exception.Exception };

            }

            AddProperty(result, "LastCorrelationId", pnpException?.CorrelationId);
            AddProperty(result, "LastExceptionTimeStampUtc", pnpException?.TimeStampUtc);
            AddProperty(result, "LastExceptionMessage", pnpException?.Message);
            AddProperty(result, "LastExceptionStacktrace", pnpException?.Stacktrace);
            AddProperty(result, "LastExceptionScriptLineNumber", pnpException?.ScriptLineNumber);
        }

        void AddProperty(PSObject pso, string name, object value)
        {
            pso.Properties.Add(new PSVariableProperty(new PSVariable(name, value)));
        }
    }
}