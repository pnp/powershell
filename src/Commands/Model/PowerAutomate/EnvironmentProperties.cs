using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.PowerAutomate
{
    /// <summary>
    /// Information on a Power Automate environment
    /// </summary>
    public class EnvironmentProperties
    {
        /// <summary>
        /// The friendly displayname of a Power Automate environment
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Date and time at which this environment has been created
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Information on the endpoints through which this environment is available
        /// </summary>
        public Dictionary<string, string> RuntimeEndpoints { get; set; }
    }
}