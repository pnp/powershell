using System;
using System.ComponentModel;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Extension methods
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Returns the description of an enum value which can be set using [Description("some description")] attribute
        /// </summary>
        /// <param name="enumValue">The enum to try to get the description of</param>
        /// <returns>Friendly name of the enum value</returns>
        static public string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field == null)
            {
                return enumValue.ToString();
            }

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return enumValue.ToString();
        }
    }
}
