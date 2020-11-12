using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Tests
{
    public class PSTestScope : IDisposable
    {
        private Runspace _runSpace;

        public string SiteUrl { get; set; }
        public string CredentialManagerEntry { get; set; }

        public PSTestScope(bool connect = true)
        {
            SiteUrl = Environment.GetEnvironmentVariable("PnPTests_SiteUrl");
            CredentialManagerEntry = Environment.GetEnvironmentVariable("PnPTests_CredentialManagerLabel");

            if(string.IsNullOrEmpty(SiteUrl))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_SiteUrl environment variable, or run Run-Tests.ps1 in the build root folder");
            }
            if(string.IsNullOrEmpty(CredentialManagerEntry))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_CredentialManagerLabel variable, or run Run-Tests.ps1 in the build root folder");
            }
            var iss = InitialSessionState.CreateDefault();
            if (connect)
            {
                SessionStateCmdletEntry ssce = new SessionStateCmdletEntry("Connect-PnPOnline", typeof(ConnectOnline), null);

                iss.Commands.Add(ssce);
            }
            _runSpace = RunspaceFactory.CreateRunspace(iss);

            _runSpace.Open();

            var pipeLine = _runSpace.CreatePipeline();

            if (connect)
            {
                var cmd = new Command("Connect-PnPOnline");
                cmd.Parameters.Add("Url", SiteUrl);
                // Use the configured Credential Manager to authenticate
                cmd.Parameters.Add("Credentials", CredentialManagerEntry);
                pipeLine.Commands.Add(cmd);
                pipeLine.Invoke();
            }
        }

        public PSTestScope(string siteUrl, bool connect = true)
        {
            SiteUrl = siteUrl;
            CredentialManagerEntry = Environment.GetEnvironmentVariable("PnPTests_CredentialManagerLabel");

            if(string.IsNullOrEmpty(CredentialManagerEntry))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_CredentialManagerLabel variable, or run Run-Tests.ps1 in the build root folder");
            }
            var iss = InitialSessionState.CreateDefault();
            if (connect)
            {
                SessionStateCmdletEntry ssce = new SessionStateCmdletEntry("Connect-PnPOnline", typeof(ConnectOnline), null);

                iss.Commands.Add(ssce);
            }
            _runSpace = RunspaceFactory.CreateRunspace(iss);

            _runSpace.Open();

            var pipeLine = _runSpace.CreatePipeline();

            if (connect)
            {
                var cmd = new Command("Connect-PnPOnline");
                cmd.Parameters.Add("Url", SiteUrl);
                cmd.Parameters.Add("Credentials", CredentialManagerEntry);
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
            //if (_powerShell != null)
            //{
            //    _powerShell.Dispose();
            //}
            if (_runSpace != null)
            {
                _runSpace.Dispose();
            }
        }
    }
}
