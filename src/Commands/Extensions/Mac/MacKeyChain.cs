// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// Namespace modified for PnP PowerShell
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using PnP.PowerShell.Extensions;
using PnP.PowerShell.Extensions.Mac;
using static PnP.PowerShell.Extensions.Mac.CoreFoundation;
using static PnP.PowerShell.Extensions.Mac.SecurityFramework;

namespace Microsoft.Identity.Client.Extensions.Msal
{
    internal class MacOSKeychain
    {
        private readonly string _namespace;

        #region Constructors

        /// <summary>
        /// Open the default keychain (current user's login keychain).
        /// </summary>
        /// <param name="namespace">Optional namespace to scope credential operations.</param>
        /// <returns>Default keychain.</returns>
        public MacOSKeychain(string @namespace = null)
        {
            _namespace = @namespace;
        }

        #endregion

        #region ICredentialStore

        public MacOSKeychainCredential Get(string service, string account)
        {
            IntPtr query = IntPtr.Zero;
            IntPtr resultPtr = IntPtr.Zero;
            IntPtr servicePtr = IntPtr.Zero;
            IntPtr accountPtr = IntPtr.Zero;

            try
            {
                query = CFDictionaryCreateMutable(
                    IntPtr.Zero,
                    0,
                    IntPtr.Zero, IntPtr.Zero);

                CFDictionaryAddValue(query, kSecClass, kSecClassGenericPassword);
                CFDictionaryAddValue(query, kSecMatchLimit, kSecMatchLimitOne);
                CFDictionaryAddValue(query, kSecReturnData, kCFBooleanTrue);
                CFDictionaryAddValue(query, kSecReturnAttributes, kCFBooleanTrue);

                if (!string.IsNullOrWhiteSpace(service))
                {
                    string fullService = CreateServiceName(service);
                    servicePtr = CreateCFStringUtf8(fullService);
                    CFDictionaryAddValue(query, kSecAttrService, servicePtr);
                }

                if (!string.IsNullOrWhiteSpace(account))
                {
                    accountPtr = CreateCFStringUtf8(account);
                    CFDictionaryAddValue(query, kSecAttrAccount, accountPtr);
                }

                int searchResult = SecItemCopyMatching(query, out resultPtr);

                switch (searchResult)
                {
                case OK:
                    int typeId = CFGetTypeID(resultPtr);
                    Debug.Assert(typeId != CFArrayGetTypeID(), "Returned more than one keychain item in search");
                    if (typeId == CFDictionaryGetTypeID())
                    {
                        return CreateCredentialFromAttributes(resultPtr);
                    }

                    throw new InteropException($"Unknown keychain search result type CFTypeID: {typeId}.", -1);

                case ErrorSecItemNotFound:
                    return null;

                default:
                    ThrowIfError(searchResult);
                    return null;
                }
            }
            finally
            {
                if (query != IntPtr.Zero)
                    CFRelease(query);
                if (servicePtr != IntPtr.Zero)
                    CFRelease(servicePtr);
                if (accountPtr != IntPtr.Zero)
                    CFRelease(accountPtr);
                if (resultPtr != IntPtr.Zero)
                    CFRelease(resultPtr);
            }
        }

        /// <summary>Returns the service name of every generic password item. Only attributes are requested, never the data, so this
        /// does not prompt the user for access to the secrets it enumerates.</summary>
        public List<string> EnumerateServiceNames()
        {
            IntPtr query = IntPtr.Zero;
            IntPtr resultPtr = IntPtr.Zero;

            var serviceNames = new List<string>();

            try
            {
                query = CFDictionaryCreateMutable(
                    IntPtr.Zero,
                    0,
                    IntPtr.Zero, IntPtr.Zero);

                CFDictionaryAddValue(query, kSecClass, kSecClassGenericPassword);
                CFDictionaryAddValue(query, kSecMatchLimit, GetMatchLimitAll());
                CFDictionaryAddValue(query, kSecReturnAttributes, kCFBooleanTrue);

                int searchResult = SecItemCopyMatching(query, out resultPtr);

                switch (searchResult)
                {
                case OK:
                    break;

                case ErrorSecItemNotFound:
                    return serviceNames;

                default:
                    ThrowIfError(searchResult);
                    return serviceNames;
                }

                if (resultPtr == IntPtr.Zero)
                {
                    return serviceNames;
                }

                int typeId = CFGetTypeID(resultPtr);
                if (typeId == CFArrayGetTypeID())
                {
                    long count = CFArrayGetCount(resultPtr);
                    for (long index = 0; index < count; index++)
                    {
                        IntPtr item = CFArrayGetValueAtIndex(resultPtr, index);
                        if (item != IntPtr.Zero)
                        {
                            AddServiceName(serviceNames, item);
                        }
                    }
                }
                else if (typeId == CFDictionaryGetTypeID())
                {
                    // A keychain holding a single generic password hands back that item rather than an array of one
                    AddServiceName(serviceNames, resultPtr);
                }
                else
                {
                    // Anything else means the query did not return what was asked for. Say so rather than report an empty keychain
                    throw new InteropException($"Unexpected keychain search result type CFTypeID: {typeId}.", -1);
                }

                return serviceNames;
            }
            finally
            {
                if (query != IntPtr.Zero)
                    CFRelease(query);
                if (resultPtr != IntPtr.Zero)
                    CFRelease(resultPtr);
            }
        }

        private void AddServiceName(List<string> serviceNames, IntPtr attributes)
        {
            string service = GetStringAttribute(attributes, kSecAttrService);
            if (string.IsNullOrWhiteSpace(service))
            {
                return;
            }

            // Undo the prefix CreateServiceName applies when this keychain is scoped to a namespace
            if (!string.IsNullOrWhiteSpace(_namespace))
            {
                string prefix = $"{_namespace}:";
                if (!service.StartsWith(prefix, StringComparison.Ordinal))
                {
                    return;
                }
                service = service.Substring(prefix.Length);
            }

            serviceNames.Add(service);
        }

        public void AddOrUpdate(string service, string account, byte[] secretBytes)
        {
            IntPtr passwordData = IntPtr.Zero;
            IntPtr itemRef = IntPtr.Zero;

            string serviceName = CreateServiceName(service);


            uint serviceNameLength = (uint)serviceName.Length;
            uint accountLength = (uint)(account?.Length ?? 0);

            try
            {
                // Check if an entry already exists in the keychain
                int findResult = SecKeychainFindGenericPassword(
                    IntPtr.Zero, serviceNameLength, serviceName, accountLength, account,
                    out uint _, out passwordData, out itemRef);

                switch (findResult)
                {
                // Update existing entry
                case OK:
                    ThrowIfError(
                        SecKeychainItemModifyAttributesAndData(itemRef, IntPtr.Zero, (uint)secretBytes.Length, secretBytes),
                        "Could not update existing item"
                    );
                    break;

                // Create new entry
                case ErrorSecItemNotFound:
                    ThrowIfError(
                        SecKeychainAddGenericPassword(IntPtr.Zero, serviceNameLength, serviceName, accountLength,
                            account, (uint)secretBytes.Length, secretBytes, out itemRef),
                        "Could not create new item"
                    );
                    break;

                default:
                    ThrowIfError(findResult);
                    break;
                }
            }
            finally
            {
                if (passwordData != IntPtr.Zero)
                {
                    SecKeychainItemFreeContent(IntPtr.Zero, passwordData);
                }

                if (itemRef != IntPtr.Zero)
                {
                    CFRelease(itemRef);
                }
            }
        }

        public bool Remove(string service, string account)
        {
            IntPtr query = IntPtr.Zero;
            IntPtr itemRefPtr = IntPtr.Zero;
            IntPtr servicePtr = IntPtr.Zero;
            IntPtr accountPtr = IntPtr.Zero;

            try
            {
                query = CFDictionaryCreateMutable(
                    IntPtr.Zero,
                    0,
                    IntPtr.Zero, IntPtr.Zero);

                CFDictionaryAddValue(query, kSecClass, kSecClassGenericPassword);
                CFDictionaryAddValue(query, kSecMatchLimit, kSecMatchLimitOne);
                CFDictionaryAddValue(query, kSecReturnRef, kCFBooleanTrue);

                if (!string.IsNullOrWhiteSpace(service))
                {
                    string fullService = CreateServiceName(service);
                    servicePtr = CreateCFStringUtf8(fullService);
                    CFDictionaryAddValue(query, kSecAttrService, servicePtr);
                }

                if (!string.IsNullOrWhiteSpace(account))
                {
                    accountPtr = CreateCFStringUtf8(account);
                    CFDictionaryAddValue(query, kSecAttrAccount, accountPtr);
                }

                // Search for the credential to delete and get the SecKeychainItem ref.
                int searchResult = SecItemCopyMatching(query, out itemRefPtr);
                switch (searchResult)
                {
                case OK:
                    // Delete the item
                    ThrowIfError(
                        SecKeychainItemDelete(itemRefPtr)
                    );
                    return true;

                case ErrorSecItemNotFound:
                    return false;

                default:
                    ThrowIfError(searchResult);
                    return false;
                }
            }
            finally
            {
                if (query != IntPtr.Zero)
                    CFRelease(query);
                if (itemRefPtr != IntPtr.Zero)
                    CFRelease(itemRefPtr);
                if (servicePtr != IntPtr.Zero)
                    CFRelease(servicePtr);
                if (accountPtr != IntPtr.Zero)
                    CFRelease(accountPtr);
            }
        }

        #endregion

        private static IntPtr CreateCFStringUtf8(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return CFStringCreateWithBytes(IntPtr.Zero,
                bytes, bytes.Length, CFStringEncoding.kCFStringEncodingUTF8, false);
        }

        private static MacOSKeychainCredential CreateCredentialFromAttributes(IntPtr attributes)
        {
            string service = GetStringAttribute(attributes, kSecAttrService);
            string account = GetStringAttribute(attributes, kSecAttrAccount);
            byte[] password = GetByteArrayAtrribute(attributes, kSecValueData);
            string label = GetStringAttribute(attributes, kSecAttrLabel);
            return new MacOSKeychainCredential(service, account, password, label);
        }

        private static byte[] GetByteArrayAtrribute(IntPtr dict, IntPtr key)
        {
            if (dict == IntPtr.Zero)
            {
                return null;
            }

            if (CFDictionaryGetValueIfPresent(dict, key, out IntPtr value) && value != IntPtr.Zero)
            {
                if (CFGetTypeID(value) == CFDataGetTypeID())
                {
                    int length = CFDataGetLength(value);
                    if (length > 0)
                    {
                        IntPtr ptr = CFDataGetBytePtr(value);
                        byte[] managedArray = new byte[length]; // last byte is the string terminator!
                        Marshal.Copy(ptr, managedArray, 0, length);

                        return managedArray;
                    }
                }
            }

            return null;
        }

        private static string GetStringAttribute(IntPtr dict, IntPtr key)
        {
            if (dict == IntPtr.Zero)
            {
                return null;
            }

            IntPtr buffer = IntPtr.Zero;
            try
            {
                if (CFDictionaryGetValueIfPresent(dict, key, out IntPtr value) && value != IntPtr.Zero)
                {
                    if (CFGetTypeID(value) == CFStringGetTypeID())
                    {
                        int stringLength = (int)CFStringGetLength(value);
                        int bufferSize = stringLength + 1;
                        buffer = Marshal.AllocHGlobal(bufferSize);
                        if (CFStringGetCString(value, buffer, bufferSize, CFStringEncoding.kCFStringEncodingUTF8))
                        {
                            return Marshal.PtrToStringAuto(buffer, stringLength);
                        }
                    }

                    if (CFGetTypeID(value) == CFDataGetTypeID())
                    {
                        int length = CFDataGetLength(value);
                        if (length > 0)
                        {
                            IntPtr ptr = CFDataGetBytePtr(value);
                            return Marshal.PtrToStringAuto(ptr, length);
                        }
                    }
                }
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }

            return null;
        }

        private string CreateServiceName(string service)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(_namespace))
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0}:", _namespace);
            }

            sb.Append(service);
            return sb.ToString();
        }
    }
}