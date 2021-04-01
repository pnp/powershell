using PnP.Core.Model.SharePoint;
using System;

namespace PnP.PowerShell.Commands.Model.Syntex
{
    /// <summary>
    /// Defines a model publication
    /// </summary>
    public class SyntexModelPublication
    {
        /// <summary>
        /// Unique id of the SharePoint Syntex model
        /// </summary>
        public Guid ModelUniqueId { get; internal set; }

        /// <summary>
        /// Server relative url of the library registered with the model
        /// </summary>
        public string TargetLibraryServerRelativeUrl { get; internal set; }

        /// <summary>
        /// Fully qualified URL of the site collection holding the library registered with the model
        /// </summary>
        public string TargetSiteUrl { get; internal set; }

        /// <summary>
        /// Server relative url of the web holding the library registered with the model
        /// </summary>
        public string TargetWebServerRelativeUrl { get; internal set; }

        /// <summary>
        /// The view option specified when registering the model with the library
        /// </summary>
        public MachineLearningPublicationViewOption ViewOption { get; internal set; }
    }
}
