using System;

namespace PnP.PowerShell.Commands.Attributes
{
    public enum TokenType : short
    {
        All = 0,
        Application = 1,
        Delegate = 2
    }

    /// <summary>
    /// Declares the type of token a cmdlet works with.
    /// </summary>
    /// <remarks>
    /// Nothing reads this attribute, so it neither restricts nor validates anything at runtime. Use <see cref="ApiNotAvailableUnderApplicationPermissions"/>
    /// or <see cref="ApiNotAvailableUnderDelegatedPermissions"/> to state that a cmdlet cannot be used with a token type, as those are the markers
    /// <see cref="Utilities.CommandPermissionHelper"/> acts on.
    /// </remarks>
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
