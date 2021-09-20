namespace PnP.PowerShell.Commands.Model.PowerAutomate
{
    /// <summary>
    /// Information on a Power Automate environment
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// Internal name of the Power Automate environment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Information on in which region the environment is hosted
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Internal identifier for the type of environment
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Full path to the identifier of this Power Automate environment
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Additional properties on the environment
        /// </summary>
        public EnvironmentProperties Properties { get; set; }
    }
}