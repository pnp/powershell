using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Tests
{
    public class PSTestScope : IDisposable
    {
        private Runspace _runSpace;

        private Configuration configuration;

        public PSTestScope(bool connect = true)
        {

            configuration = new Configuration();

            var iss = InitialSessionState.CreateDefault();
            if (connect)
            {
                SessionStateCmdletEntry ssce = new SessionStateCmdletEntry("Connect-PnPOnline", typeof(ConnectOnline), null);

                iss.Commands.Add(ssce);
            }
            _runSpace = RunspaceFactory.CreateRunspace(iss);

            _runSpace.Open();

            var pipeLine = _runSpace.CreatePipeline();

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // This is only works / is needed on Windows
                var executionPolicyCmd = new Command("Set-ExecutionPolicy");
                executionPolicyCmd.Parameters.Add("ExecutionPolicy", "Unrestricted");
                pipeLine.Commands.Add(executionPolicyCmd);
            }

            if (connect)
            {
                var cmd = new Command("Connect-PnPOnline");
                cmd.Parameters.Add("Url", configuration.SiteUrl);
                // Use the configured Credential Manager to authenticate
                cmd.Parameters.Add("Credentials", configuration.Credentials);
                pipeLine.Commands.Add(cmd);
                pipeLine.Invoke();
            }
        }

        public PSTestScope(string siteUrl, bool connect = true)
        {
            configuration = new Configuration();
            var iss = InitialSessionState.CreateDefault();
            if (connect)
            {
                SessionStateCmdletEntry ssce = new SessionStateCmdletEntry("Connect-PnPOnline", typeof(ConnectOnline), null);

                iss.Commands.Add(ssce);
            }
            _runSpace = RunspaceFactory.CreateRunspace(iss);

            _runSpace.Open();

            var pipeLine = _runSpace.CreatePipeline();

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // This is only works / is needed on Windows
                var executionPolicyCmd = new Command("Set-ExecutionPolicy");
                executionPolicyCmd.Parameters.Add("ExecutionPolicy", "Unrestricted");
                pipeLine.Commands.Add(executionPolicyCmd);
            }

            if (connect)
            {
                var cmd = new Command("Connect-PnPOnline");
                cmd.Parameters.Add("Url", configuration.SiteUrl);
                cmd.Parameters.Add("Credentials", configuration.Credentials);
                pipeLine.Commands.Add(cmd);
                pipeLine.Invoke();
            }
        }

        public Collection<PSObject> ExecuteCommand(string cmdletString)
        {
            return ExecuteCommand(cmdletString, null);
        }

        public Collection<PSObject> ExecuteCommand(string cmdletString, params CommandParameter[] parameters)
        {
            var pipeLine = _runSpace.CreatePipeline();
            Command cmd = new Command(cmdletString);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            pipeLine.Commands.Add(cmd);
            return pipeLine.Invoke();

        }

        public Collection<PSObject> ExecuteScript(string script)
        {
            var pipeLine = _runSpace.CreatePipeline();

            pipeLine.Commands.AddScript(script);

            return pipeLine.Invoke();
        }

        public void Dispose()
        {
            if (_runSpace != null)
            {
                _runSpace.Dispose();
            }
        }
    }
}
