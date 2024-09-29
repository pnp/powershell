using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Defines one required API permission
    /// </summary>
    public class RequiredApiPermission
    {
        /// <summary>
        /// The type of resource for which the permission is required
        /// </summary>
        public ResourceTypeName ResourceType { get; set; }

        /// <summary>
        /// The scope of the permission that is required on the resource
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Instantiates a new combination of the required resource type and scope
        /// </summary>
        /// <param name="resourceTypeName">Type of resource for which the permission is required</param>
        /// <param name="scope">The permission scope required on the resource</param>
        public RequiredApiPermission(ResourceTypeName resourceTypeName, string scope)
        {
            ResourceType = resourceTypeName;
            Scope = scope;
        }
    }
}
