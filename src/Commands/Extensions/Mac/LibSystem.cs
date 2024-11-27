// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// Namespace modified for PnP PowerShell
using System;
using System.Runtime.InteropServices;

namespace PnP.PowerShell.Extensions.Mac
{
    internal static class LibSystem
    {
        private const string LibSystemLib = "/usr/lib/libSystem.dylib";

        [DllImport(LibSystemLib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dlopen(string name, int flags);

        [DllImport(LibSystemLib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dlsym(IntPtr handle, string symbol);

        public static IntPtr GetGlobal(IntPtr handle, string symbol)
        {
            IntPtr ptr = dlsym(handle, symbol);            
            var structure = Marshal.PtrToStructure(ptr, typeof(IntPtr));

            return (IntPtr)structure;
        }
    }
}