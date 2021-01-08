using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;
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

        protected virtual void ExecuteCmdlet()
        { }

        protected override void ProcessRecord()
        {
            ExecuteCmdlet();
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
        }
    }
}
