using System;
using System.Runtime.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    public sealed class PropertyNotRetrievedException : InvalidOperationException
    {
        public PropertyNotRetrievedException()
            : base("Property has not been retrieved.")
        {
        }

        public PropertyNotRetrievedException(string message)
            : base(message)
        {
        }

        private PropertyNotRetrievedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public PropertyNotRetrievedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}