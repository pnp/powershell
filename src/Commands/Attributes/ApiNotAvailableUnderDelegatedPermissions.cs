using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to specify that the API is not available under delegated permissions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiNotAvailableUnderDelegatedPermissions : Attribute
    {       
    }
}
