using System;

namespace PnP.PowerShell.Commands.Model.Syntex
{
    /// <summary>
    /// Syntex model
    /// </summary>
    public class SyntexModel
    {
        /// <summary>
        /// Id of a model (= id of the list item)
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Unique id of the model (= unique id of the model file)
        /// </summary>
        public Guid UniqueId { get; internal set; }

        /// <summary>
        /// Name of the model
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Model description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Date when the model was trained for the last time
        /// </summary>
        public DateTime ModelLastTrained { get; internal set; }
    }
}
