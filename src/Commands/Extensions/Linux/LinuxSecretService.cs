using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PnP.PowerShell.Extensions.Linux
{
    /// <summary>
    /// Reads item metadata from the Linux Secret Service through libsecret, the same library MSAL stores the credentials with.
    /// Only attributes are ever requested, so listing never decrypts or transfers a stored secret into this process.
    /// </summary>
    internal static class LinuxSecretService
    {
        private const string LibSecret = "libsecret-1.so.0";
        private const string LibGlib = "libglib-2.0.so.0";
        private const string LibGObject = "libgobject-2.0.so.0";

        private const int SecretServiceNone = 0;

        /// <summary>SECRET_SEARCH_ALL. Deliberately without SECRET_SEARCH_LOAD_SECRETS, which is what secret-tool passes.</summary>
        private const int SecretSearchAll = 1 << 1;

        /// <summary>Offset of the message pointer within a GError: a 4 byte GQuark and a 4 byte code precede it.</summary>
        private const int GErrorMessageOffset = 8;

        /// <summary>
        /// Returns the <paramref name="wantedKey"/> attribute of every stored item whose <paramref name="filterKey"/> attribute
        /// equals <paramref name="filterValue"/>.
        /// </summary>
        public static List<string> GetItemAttributeValues(string filterKey, string filterValue, string wantedKey)
        {
            var values = new List<string>();

            IntPtr service = secret_service_get_sync(SecretServiceNone, IntPtr.Zero, out IntPtr error);
            ThrowOnError(error, "connect to the Secret Service");
            if (service == IntPtr.Zero)
            {
                throw new InvalidOperationException("No Secret Service is available.");
            }

            IntPtr query = IntPtr.Zero;
            IntPtr items = IntPtr.Zero;
            try
            {
                // An empty attribute table matches every item, which avoids having to hand glib the string hash and equality
                // function pointers that a populated query table would need. The filtering happens below instead
                query = g_hash_table_new_full(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

                items = secret_service_search_sync(service, IntPtr.Zero, query, SecretSearchAll, IntPtr.Zero, out error);
                ThrowOnError(error, "search the Secret Service");

                uint count = g_list_length(items);
                for (uint index = 0; index < count; index++)
                {
                    IntPtr item = g_list_nth_data(items, index);
                    if (item == IntPtr.Zero)
                    {
                        continue;
                    }

                    IntPtr attributes = secret_item_get_attributes(item);
                    if (attributes == IntPtr.Zero)
                    {
                        continue;
                    }

                    try
                    {
                        if (!string.Equals(LookupAttribute(attributes, filterKey), filterValue, StringComparison.Ordinal))
                        {
                            continue;
                        }

                        var value = LookupAttribute(attributes, wantedKey);
                        if (!string.IsNullOrEmpty(value))
                        {
                            values.Add(value);
                        }
                    }
                    finally
                    {
                        g_hash_table_unref(attributes);
                    }
                }
            }
            finally
            {
                if (items != IntPtr.Zero)
                {
                    uint count = g_list_length(items);
                    for (uint index = 0; index < count; index++)
                    {
                        IntPtr item = g_list_nth_data(items, index);
                        if (item != IntPtr.Zero)
                        {
                            g_object_unref(item);
                        }
                    }
                    g_list_free(items);
                }

                if (query != IntPtr.Zero)
                {
                    g_hash_table_unref(query);
                }

                g_object_unref(service);
            }

            return values;
        }

        private static string LookupAttribute(IntPtr attributes, string key)
        {
            IntPtr keyPtr = Marshal.StringToCoTaskMemUTF8(key);
            try
            {
                // The returned pointer is owned by the hash table and must not be freed
                return Marshal.PtrToStringUTF8(g_hash_table_lookup(attributes, keyPtr));
            }
            finally
            {
                Marshal.FreeCoTaskMem(keyPtr);
            }
        }

        private static void ThrowOnError(IntPtr error, string action)
        {
            if (error == IntPtr.Zero)
            {
                return;
            }

            var message = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(error, GErrorMessageOffset));
            g_error_free(error);
            throw new InvalidOperationException($"Unable to {action}: {message}");
        }

        #region UNMANAGED

        [DllImport(LibSecret, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr secret_service_get_sync(int flags, IntPtr cancellable, out IntPtr error);

        [DllImport(LibSecret, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr secret_service_search_sync(IntPtr service, IntPtr schema, IntPtr attributes, int flags, IntPtr cancellable, out IntPtr error);

        [DllImport(LibSecret, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr secret_item_get_attributes(IntPtr item);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr g_hash_table_new_full(IntPtr hashFunc, IntPtr keyEqualFunc, IntPtr keyDestroyFunc, IntPtr valueDestroyFunc);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr g_hash_table_lookup(IntPtr hashTable, IntPtr key);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_hash_table_unref(IntPtr hashTable);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint g_list_length(IntPtr list);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr g_list_nth_data(IntPtr list, uint n);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_list_free(IntPtr list);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_error_free(IntPtr error);

        [DllImport(LibGObject, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_object_unref(IntPtr obj);

        #endregion
    }
}
