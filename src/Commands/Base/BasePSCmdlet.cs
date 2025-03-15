using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Cmdlets
    /// </summary>
    public class BasePSCmdlet : PSCmdlet
    {
        /// <summary>
        /// Generate a new correlation id for each cmdlet execution. This is used to correlate log entries in the PnP PowerShell log stream.
        /// </summary>
        internal Guid? CorrelationId { get; } = Guid.NewGuid();

        #region Cmdlet execution

        /// <summary>
        /// Triggered when the cmdlet is started. This is the place to do any initialization work.
        /// </summary>
        protected override void BeginProcessing()
        {
            LogDebug($"Cmdlet execution started for {MyInvocation.Line}");
            base.BeginProcessing();

            CheckForDeprecationAttributes();
        }

        /// <summary>
        /// Executes the cmdlet. This is the place to do the actual work of the cmdlet.
        /// </summary>
        protected virtual void ExecuteCmdlet()
        { }

        /// <summary>
        /// Triggered for the execution of the cmdlet. Use ExecuteCmdlet() to do the actual work of the cmdlet.
        /// </summary>
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

        /// <summary>
        /// Triggered when the cmdlet is done executing. This is the place to do any cleanup or finalization work.
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
            LogDebug($"Cmdlet execution done for {MyInvocation.Line}");
        }

        /// <summary>
        /// Triggered when the cmdlet is stopped
        /// </summary>
        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

        #endregion

        #region Helper methods

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

        /// <summary>
        /// Checks if deprecation attribute is present on the cmdlet and if so, writes a warning message to the console to notify the user to change their script to use the new cmdlet name.
        /// </summary>
        private void CheckForDeprecationAttributes()
        {
            if (MyInvocation.MyCommand.Name.ToLower() != MyInvocation.InvocationName.ToLower())
            {
                var attribute = Attribute.GetCustomAttribute(GetType(), typeof(WriteAliasWarningAttribute));
                if (attribute != null)
                {
                    var warningAttribute = attribute as WriteAliasWarningAttribute;
                    if (!string.IsNullOrEmpty(warningAttribute?.DeprecationMessage))
                    {
                        LogWarning(warningAttribute.DeprecationMessage);
                    }
                }
            }
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

        #endregion

        #region Logging

        /// <summary>
        /// Allows logging an error
        /// </summary>
        /// <param name="exception">The exception to log as an error</param>
        internal void LogError(Exception exception)
        {
            LogError(exception.Message);
        }

        /// <summary>
        /// Allows logging an error
        /// </summary>
        /// <param name="message">The message to log</param>
        internal void LogError(string message)
        {
            Utilities.Logging.LoggingUtility.Error(this, message, correlationId: CorrelationId);
        }

        /// <summary>
        /// Allows logging a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        internal void LogDebug(string message)
        {
            Utilities.Logging.LoggingUtility.Debug(this, message, correlationId: CorrelationId);
        }

        /// <summary>
        /// Allows logging a warning
        /// </summary>
        /// <param name="message">The message to log</param>
        internal void LogWarning(string message)
        {
            Utilities.Logging.LoggingUtility.Warning(this, message, correlationId: CorrelationId);
        }

        /// <summary>
        /// Allows logging an informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        internal void LogInformational(string message)
        {
            Utilities.Logging.LoggingUtility.Info(this, message, correlationId: CorrelationId);
        }
        
        #endregion
    }
}
