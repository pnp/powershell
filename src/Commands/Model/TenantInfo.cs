using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains information regarding a tenant
    /// </summary>
    public class TenantInfo
    {
        /// <summary>
        /// Unique identifier of the tenant
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// The name of the string value shown to users when signing in to Entra ID
        /// </summary>
        public string FederationBrandName { get; set; }

        /// <summary>
        /// The company name shown in places such as the admin portal
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The default domain name set on the tenant
        /// </summary>
        public string DefaultDomainName { get; set; }
    }
}
