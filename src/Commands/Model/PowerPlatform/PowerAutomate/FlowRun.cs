namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information of one Microsoft Power Automate Flow run
    /// </summary>
    public class FlowRun
    {
        /// <summary>
        /// Name of the Flow as its Flow GUID
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique identifier of this Flow run.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of object, typically Microsoft.ProcessSimple/environments/flows/runs
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Additional information of the Flow run.
        /// </summary>
        public FlowRunProperties Properties { get; set; }
    }
}
