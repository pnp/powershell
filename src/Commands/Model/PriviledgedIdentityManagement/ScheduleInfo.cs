using System;

namespace PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement
{
    /// <summary>
    /// Information about a schedule used within role assignment requests in Privileged Identity Management
    /// </summary>
    public class ScheduleInfo
    {
        /// <summary>
        /// The date and time at which the role activation should become active
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// The expiration when the role activation should end
        /// </summary>
        public Expiration Expiration { get; set; }
    }
}
