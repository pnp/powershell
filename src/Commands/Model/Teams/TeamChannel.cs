﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Teams
{
    public partial class TeamChannel
    {
        #region Public Members

        [JsonPropertyName("@odata.type")]
        public string Type { get; set; }
        /// <summary>
        /// Defines a collection of Tabs for a Channel in a Team
        /// </summary>
        public List<TeamTab> Tabs { get; private set; }

        /// <summary>
        /// Defines a collection of Resources for Tabs in a Team Channel
        /// </summary>
        public List<TeamTabResource> TabResources { get; private set; }

        /// <summary>
        /// Defines a collection of Messages for a Team Channe
        /// </summary>
        public List<TeamChannelMessage> Messages { get; private set; }

        /// <summary>
        /// Defines the Display Name of the Channel
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Defines the type of the Channel
        /// </summary>
        public string MembershipType { get; set; }
        /// <summary>
        /// Defines the Description of the Channel
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Created date time for the channel
        /// </summary>  
        public DateTime? CreatedDateTime { get; set; }
        /// <summary>
        /// Email for the channel
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// web url of the channel
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// Defines whether the Channel is Favorite by default for all members of the Team
        /// </summary>
        public bool? IsFavoriteByDefault { get; set; }
        /// <summary>
        /// Declares the ID for the Channel
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Members of a channel
        /// </summary>
        public List<TeamChannelMember> Members { get; set; }

        /// <summary>
        /// Settings for moderating posts in a Teams Channel
        /// </summary>
        public ChannelModerationSettings ModerationSettings { get; set; } = new();

        #endregion
    }
}
