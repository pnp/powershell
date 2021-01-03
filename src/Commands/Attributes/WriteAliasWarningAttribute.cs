using System;

namespace PnP.PowerShell.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class WriteAliasWarningAttribute : Attribute
    {
        public string DeprecationMessage {get;set;}
        public WriteAliasWarningAttribute(string deprecationMessage)
        {
            DeprecationMessage = deprecationMessage;
        }
    }
}