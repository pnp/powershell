using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to indicate that the API permissions required by a cmdlet are not fixed, but follow from the resource the cmdlet is pointed at at runtime.
    /// Use this on cmdlets for which declaring a fixed set of permissions through <see cref="RequiredApiPermissionsBase"/> would be inaccurate, i.e. the Microsoft Graph change notification cmdlets.
    /// This attribute is informational only. It is surfaced through Get-PnPCommandPermission and is deliberately not taken into account when validating an access token, so it can never cause a false warning.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiPermissionsDependOnResource : Attribute
    {
        /// <summary>
        /// Name of the parameter of which the value determines the permissions required, if the resource is provided through a parameter of the cmdlet
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Url of the documentation which lists the permissions required per resource
        /// </summary>
        public string DocumentationUrl { get; set; }

        /// <summary>
        /// Remarks explaining which permissions are needed on the resource
        /// </summary>
        public string Remarks { get; set; }
    }
}
