using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to specify the name of the property in the model that should be used to convert the value from the CSOM object to the model object
    /// </summary>
    /// <param name="propertyName">Name of the property on the CSOM object</param>
    /// <param name="skip">If set to true, the property will be skipped</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsomToModelConverter(string propertyName = null, bool skip = false) : Attribute
    {
        /// <summary>
        /// Name of the property on the CSOM object
        /// </summary>
        public string PropertyName { get; set; } = propertyName;

        /// <summary>
        /// If set to true, the property will be skipped
        /// </summary>
        public bool Skip { get; set; } = skip;
    }
}
