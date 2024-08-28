﻿using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;

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
                        TokenHandler.EnsureRequiredPermissionsAvailableInAccessToken(GetType(), gex.AccessToken);
                    }
                }
                if(string.IsNullOrWhiteSpace(errorMessage) && gex.HttpResponse != null && gex.HttpResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
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
            this.WriteError(new ErrorRecord(exception, string.Empty, errorCategory, target));
        }
    }
}
