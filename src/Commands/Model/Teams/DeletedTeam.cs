using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities.JSON;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Teams
{
    /// <summary>
    /// Defines Deleted Team
    /// </summary>
    public class DeletedTeam
    {
        #region Public Members
        /// <summary>
        /// The Id of the deleted team
        /// </summary>
        public Guid Id { get; set; }
        #endregion
    }
}
