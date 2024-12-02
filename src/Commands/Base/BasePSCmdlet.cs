using System;
using System.Management.Automation;
using System.Threading;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Cmdlets
    /// </summary>
    public class BasePSCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            PnP.Framework.Diagnostics.Log.Info("PnP.PowerShell", $"Executing {this.MyInvocation.InvocationName}");
            if (MyInvocation.MyCommand.Name.ToLower() != MyInvocation.InvocationName.ToLower())
            {
                var attribute = Attribute.GetCustomAttribute(this.GetType(), typeof(WriteAliasWarningAttribute));
                if (attribute != null)
                {
                    var warningAttribute = attribute as WriteAliasWarningAttribute;
                    if (!string.IsNullOrEmpty(warningAttribute?.DeprecationMessage))
                    {
                        WriteWarning(warningAttribute.DeprecationMessage);
                    }
                }
            }
            if (PnPConnection.Current == null)
            {
                if (Settings.Current.LastUserTenant != null)
                {
                    var clientid = PnPConnection.GetCacheClientId(Settings.Current.LastUserTenant);
                    if (clientid != null)
                    {
                        var  cancellationTokenSource = new CancellationTokenSource();
                        PnPConnection.Current = PnPConnection.CreateWithInteractiveLogin(new Uri(Settings.Current.LastUserTenant.ToLower()), clientid, null, Framework.AzureEnvironment.Production, cancellationTokenSource, false, null, false, false, Host);
                    }

                }
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        /// <summary>
        /// Checks if a parameter with the provided name has been provided in the execution command
        /// </summary>
        /// <param name="parameterName">Name of the parameter to validate if it has been provided in the execution command</param>
        /// <returns>True if a parameter with the provided name is present, false if it is not</returns>
        public bool ParameterSpecified(string parameterName)
        {
            return MyInvocation.BoundParameters.ContainsKey(parameterName);
        }

        protected string ErrorActionSetting
        {
            get
            {
                if (MyInvocation.BoundParameters.TryGetValue("ErrorAction", out object result))
                    return result.ToString() ?? "";
                else
                    return SessionState.PSVariable.GetValue("ErrorActionPreference")?.ToString() ?? "";
            }
        }

        protected virtual void ExecuteCmdlet()
        { }

        protected override void ProcessRecord()
        {
            try
            {
                ExecuteCmdlet();
            }
            catch (Model.Graph.GraphException gex)
            {
                var errorMessage = gex.Error.Message;

                if (gex.Error.Code == "Authorization_RequestDenied")
                {
                    if (!string.IsNullOrEmpty(gex.AccessToken))
                    {
                        TokenHandler.EnsureRequiredPermissionsAvailableInAccessTokenAudience(GetType(), gex.AccessToken);
                    }
                }
                if (string.IsNullOrWhiteSpace(errorMessage) && gex.HttpResponse != null && gex.HttpResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    errorMessage = "Access denied. Check for the required permissions.";
                }
                throw new PSInvalidOperationException(errorMessage);
            }
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

        internal void WriteError(Exception exception, ErrorCategory errorCategory, object target = null)
        {
            WriteError(new ErrorRecord(exception, string.Empty, errorCategory, target));
        }
    }
}
