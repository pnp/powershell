using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to indicate that a cmdlet requires no API permissions on the Entra ID application registration used to connect with PnP PowerShell.
    /// Use this on cmdlets which only read or change local state, such as Get-PnPContext, and on cmdlets which authenticate separately instead of using the PnP connection, such as Register-PnPEntraIDApp.
    /// Without this attribute such cmdlets would be reported as requiring the permissions that follow from the base class they derive from, which would be a false positive.
    /// This attribute is informational only and is not taken into account when validating an access token.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiPermissionsNotRequired : Attribute
    {
        /// <summary>
        /// Remarks explaining why no permissions are needed and, if applicable, which rights are needed instead
        /// </summary>
        public string Remarks { get; set; }
    }
}
