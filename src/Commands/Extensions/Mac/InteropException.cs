// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// Namespace modified for PnP PowerShell
using System;

namespace PnP.PowerShell.Extensions
{
    /// <summary>
    /// An unexpected error occurred in interop-code.
    /// </summary>
    internal class InteropException : Exception
    {
        public InteropException()
            : base() { }

        public InteropException(string message, int errorCode)
            : base(message + " .Error code: " + errorCode)
        {
            ErrorCode = errorCode;
        }

        public InteropException(string message, int errorCode, Exception innerException)
            : base(message + ". Error code: " + errorCode, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Native error code.
        /// </summary>
        public int ErrorCode { get; }

        private string DebuggerDisplay => $"{Message} [0x{ErrorCode:x}]";
    }
}