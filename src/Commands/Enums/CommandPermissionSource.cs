using System.ComponentModel;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Indicates where the permission information returned for a cmdlet originates from
    /// </summary>
    public enum CommandPermissionSource : short
    {
        /// <summary>
        /// The permissions could not be determined for this cmdlet
        /// </summary>
        [Description("Unknown")]
        Unknown = 0,

        /// <summary>
        /// The permissions have been declared on the cmdlet through its permission attributes and are therefore authoritative
        /// </summary>
        [Description("Declared")]
        Declared = 1,

        /// <summary>
        /// The permissions have been derived from the type of cmdlet and the operation it performs. They are a least privilege estimate and may need to be raised for specific operations.
        /// </summary>
        [Description("Inferred")]
        Inferred = 2,

        /// <summary>
        /// Part of the permissions is declared on the cmdlet through its permission attributes and part of it has been derived. This applies to cmdlets which call into another API next to SharePoint CSOM.
        /// </summary>
        [Description("Declared and inferred")]
        DeclaredAndInferred = 5,

        /// <summary>
        /// The cmdlet requires no API permissions on the Entra ID application registration used to connect with PnP PowerShell. That covers cmdlets which call no API at all,
        /// as well as cmdlets which authenticate separately, such as Register-PnPEntraIDApp, which can still require rights of their own. The guidance states which.
        /// </summary>
        [Description("Not applicable")]
        NotApplicable = 3,

        /// <summary>
        /// The permissions required follow from the resource the cmdlet is pointed at at runtime and can therefore not be stated up front
        /// </summary>
        [Description("Depends on the resource")]
        ResourceDependent = 4
    }
}
