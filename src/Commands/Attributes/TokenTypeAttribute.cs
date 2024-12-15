using System;

namespace PnP.PowerShell.Commands.Attributes
{
    public enum TokenType : short
    {
        All = 0,
        Application = 1,
        Delegate = 2
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TokenTypeAttribute : Attribute
    {
        public TokenType TokenType { get; set; }

        public TokenTypeAttribute(TokenType tokenType = TokenType.All)
        {
            TokenType = tokenType;
        }
    }
}
