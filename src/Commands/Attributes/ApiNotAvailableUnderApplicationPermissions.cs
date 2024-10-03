using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to specify that the API is not available under application permissions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiNotAvailableUnderApplicationPermissions : Attribute
    {       
    }
}
