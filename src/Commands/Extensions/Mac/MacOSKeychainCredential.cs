// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// Namespace modified for PnP PowerShell
namespace PnP.PowerShell.Extensions.Mac
{
    internal class MacOSKeychainCredential 
    {
        internal MacOSKeychainCredential(string service, string account, byte[] password, string label)
        {
            Service = service;
            Account = account;
            Password = password;
            Label = label;
        }

        public string Service { get; }

        public string Account { get; }

        public string Label { get; }

        public byte[] Password { get; }
    }
}