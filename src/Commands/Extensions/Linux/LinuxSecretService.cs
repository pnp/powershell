using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PnP.PowerShell.Extensions.Linux
{
    /// <summary>Reads item metadata from the Linux Secret Service through libsecret. Only attributes are ever requested, so
    /// listing never decrypts or transfers a stored secret into this process.</summary>
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

        /// <summary>Offsets within a GList, which is { gpointer data; GList *next; GList *prev; }.</summary>
        private const int GListDataOffset = 0;
        private static readonly int GListNextOffset = IntPtr.Size;

        private static IntPtr _stringHashFunc;
        private static IntPtr _stringEqualFunc;

        /// <summary>Returns the <paramref name="wantedKey"/> attribute of every item whose <paramref name="filterKey"/> is <paramref name="filterValue"/>.</summary>
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
            IntPtr filterKeyPtr = IntPtr.Zero;
            IntPtr filterValuePtr = IntPtr.Zero;
            try
            {
                // Filtering in the query means the service returns only matching items, not every secret in the keyring
                EnsureStringHashFunctions();
                query = g_hash_table_new_full(_stringHashFunc, _stringEqualFunc, IntPtr.Zero, IntPtr.Zero);
                filterKeyPtr = Marshal.StringToCoTaskMemUTF8(filterKey);
                filterValuePtr = Marshal.StringToCoTaskMemUTF8(filterValue);
                g_hash_table_insert(query, filterKeyPtr, filterValuePtr);

                items = secret_service_search_sync(service, IntPtr.Zero, query, SecretSearchAll, IntPtr.Zero, out error);
                ThrowOnError(error, "search the Secret Service");

                // Walked by next pointer: indexing a GList restarts at the head each time, which is quadratic
                for (IntPtr node = items; node != IntPtr.Zero; node = Marshal.ReadIntPtr(node, GListNextOffset))
                {
                    IntPtr item = Marshal.ReadIntPtr(node, GListDataOffset);
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
                for (IntPtr node = items; node != IntPtr.Zero; node = Marshal.ReadIntPtr(node, GListNextOffset))
                {
                    IntPtr item = Marshal.ReadIntPtr(node, GListDataOffset);
                    if (item != IntPtr.Zero)
                    {
                        g_object_unref(item);
                    }
                }

                if (items != IntPtr.Zero)
                {
                    g_list_free(items);
                }

                if (query != IntPtr.Zero)
                {
                    g_hash_table_unref(query);
                }

                // The table has no destroy functions, so the key and value are ours to free
                Marshal.FreeCoTaskMem(filterKeyPtr);
                Marshal.FreeCoTaskMem(filterValuePtr);

                g_object_unref(service);
            }

            return values;
        }

        /// <summary>Resolves glib string hash and equality, which a query table needs to match on key text.</summary>
        private static void EnsureStringHashFunctions()
        {
            if (_stringHashFunc != IntPtr.Zero)
            {
                return;
            }

            IntPtr glib = NativeLibrary.Load(LibGlib);
            _stringHashFunc = NativeLibrary.GetExport(glib, "g_str_hash");
            _stringEqualFunc = NativeLibrary.GetExport(glib, "g_str_equal");
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
        private static extern void g_hash_table_insert(IntPtr hashTable, IntPtr key, IntPtr value);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr g_hash_table_lookup(IntPtr hashTable, IntPtr key);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_hash_table_unref(IntPtr hashTable);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_list_free(IntPtr list);

        [DllImport(LibGlib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_error_free(IntPtr error);

        [DllImport(LibGObject, CallingConvention = CallingConvention.Cdecl)]
        private static extern void g_object_unref(IntPtr obj);

        #endregion
    }
}
