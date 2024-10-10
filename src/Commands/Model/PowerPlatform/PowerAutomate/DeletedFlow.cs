using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information on one Microsoft Power Automate Flow
    /// </summary>
    public class DeletedFlow
    {
        /// <summary>
        /// Name of the Flow as its Flow GUID
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// The friendly name of the Flow as can be seen through flow.microsoft.com
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Date and time at which this Flow has last been modified
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }

        public DeletedFlow(string name, string displayName, DateTime? lastModifiedTime)
        {
            Name = name;
            DisplayName = displayName;
            LastModifiedTime = lastModifiedTime;
        }
    }
}