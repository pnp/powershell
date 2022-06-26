using System;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Describes the information returned in a sensitivitylabel of a site
    /// </summary>
    public class SensitivityLabel
    {
        /// <summary>
        /// Unique identifier of the sensitivity label
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The name of the sensitivity label
        /// </summary>
        public string DisplayName { get; set; }
    }
}