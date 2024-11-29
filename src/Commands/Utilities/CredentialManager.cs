using System;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.SharePoint.Client;
using Microsoft.Win32.SafeHandles;
using PnP.Framework.Extensions;
using PnP.Framework.Modernization.Cache;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

[assembly: InternalsVisibleTo("PnP.PowerShell.Tests")]
namespace PnP.PowerShell.Commands.Utilities
{
    internal static class CredentialManager
    {


        public static bool AddCredential(string name, string username, SecureString password, bool overwrite)
        {
            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    AddVaultCredential(defaultVault, name, username, password);
                }
            }
            else
            {
                if (!name.StartsWith("PnPPS:"))
                {
                    name = $"PnPPS:{name}";
                }
                if (OperatingSystem.IsWindows())
                {
                    WriteWindowsCredentialManagerEntry(name, username, password);
                }
                else if (OperatingSystem.IsMacOS())
                {
                    WriteMacOSKeyChainEntry(name, SecureStringToString(password));
                }
            }
            return true;
        }

        public static bool AddAppId(string name, string appid, bool overwrite)
        {
            if (!name.StartsWith("PnPPSAppId:"))
            {
                name = $"PnPPSAppId:{name}";
            }
            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    AddVaultAppId(defaultVault, name, appid);
                }
            }
            else
            {
                var secureAppId = new NetworkCredential(null, appid).SecurePassword;
                if (OperatingSystem.IsWindows())
                {

                    WriteWindowsCredentialManagerEntry(name, null, secureAppId);
                }
                else if (OperatingSystem.IsMacOS())
                {
                    WriteMacOSKeyChainEntry(name, appid);
                }
            }
            return true;
        }

        public static PSCredential GetCredential(string name)
        {
            // check if Microsoft.PowerShell.SecretManagement is available
            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    return GetVaultCredential(defaultVault, name);
                }
            }
            else
            {
                if (OperatingSystem.IsWindows())
                {
                    var cred = ReadWindowsCredentialManagerEntry(name);
                    if (cred == null)
                    {
                        cred = ReadWindowsCredentialManagerEntry($"PnPPS:{name}");
                    }
                    return cred;
                }
                if (OperatingSystem.IsMacOS())
                {
                    var cred = ReadMacOSKeyChainEntry(name);
                    if (cred == null)
                    {
                        cred = ReadMacOSKeyChainEntry($"PnPPS:{name}");
                    }
                    return cred;
                }
            }
            return null;
        }

        public static string GetAppId(string name)
        {
            if (!name.StartsWith("PnPPSAppId:"))
            {
                name = $"PnPPSAppId:{name}";
            }
            // check if Microsoft.PowerShell.SecretManagement is available
            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    return GetVaultAppId(defaultVault, name);
                }
            }
            else
            {
                if (OperatingSystem.IsWindows())
                {
                    var cred = ReadWindowsCredentialManagerEntry(name);
                    if (cred != null)
                    {
                        return SecureStringToString(cred.Password);
                    }
                }
                if (OperatingSystem.IsMacOS())
                {
                    var cred = ReadMacOSKeyChainEntry(name);
                    if (cred != null)
                    {
                        return SecureStringToString(cred.Password).Trim('"');
                    }
                }
            }
            return null;
        }

        public static bool RemoveCredential(string name)
        {
            bool success = false;

            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    RemoveVaultCredential(defaultVault, name);
                    return true;
                }
            }
            else
            {
                if (OperatingSystem.IsWindows())
                {
                    success = DeleteWindowsCredentialManagerEntry(name);
                    if (!success)
                    {
                        success = DeleteWindowsCredentialManagerEntry($"PnPPS:{name}");
                    }
                }
                if (OperatingSystem.IsMacOS())
                {
                    success = DeleteMacOSKeyChainEntry(name);
                    if (!success)
                    {
                        success = DeleteMacOSKeyChainEntry($"PnPPS:{name}");
                    }
                    return success;
                }
            }
            return success;
        }

        public static bool RemoveAppid(string name)
        {
            if (!name.StartsWith("PnPPSAppId:"))
            {
                name = $"PnPPSAppId:{name}";
            }
            bool success = false;

            if (HasSecretManagement())
            {
                var defaultVault = GetDefaultVault();

                if (!string.IsNullOrEmpty(defaultVault))
                {
                    RemoveVaultCredential(defaultVault, name);
                    return true;
                }
            }
            else
            {
                if (OperatingSystem.IsWindows())
                {
                    success = DeleteWindowsCredentialManagerEntry(name);
                }
                if (OperatingSystem.IsMacOS())
                {
                    success = DeleteMacOSKeyChainEntry(name);
                    return success;
                }
            }
            return success;
        }


        #region PRIVATE

        private static bool HasSecretManagement()
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();
            using (var rs = RunspaceFactory.CreateRunspace(iss))
            {
                rs.Open();
                using (var ps = System.Management.Automation.PowerShell.Create())
                {
                    ps.Runspace = rs;
                    ps.AddCommand("get-module")
                    .AddParameter("Name", "Microsoft.PowerShell.SecretManagement")
                    .AddParameter("ListAvailable");

                    var results = ps.Invoke();
                    if (results.Any())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static string GetDefaultVault()
        {
            var defaultVaultName = "";
            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("get-secretvault");

                    foreach (var result in powershell.Invoke())
                    {
                        var isDefaultProp = result.Properties.FirstOrDefault(p => p.Name == "IsDefault");
                        if (isDefaultProp != null)
                        {
                            if (Convert.ToBoolean(isDefaultProp.Value))
                            {
                                try
                                {
                                    defaultVaultName = result.Properties["Name"].Value.ToString();
                                }
                                catch
                                {
                                    defaultVaultName = result.Properties["VaultName"].Value.ToString();
                                }
                            }
                        }
                    }

                }
                myRunSpace.Close();
            }
            return defaultVaultName;
        }

        private static PSCredential GetVaultCredential(string vaultName, string name)
        {
            PSCredential creds = null;

            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("get-secret")
                    .AddParameter("Vault", vaultName)
                    .AddParameter("Name", name);

                    foreach (var result in powershell.Invoke())
                    {
                        var username = result.Properties["Username"].Value.ToString();
                        var password = result.Properties["Password"].Value;
                        creds = new PSCredential(username, (SecureString)password);
                    }

                }
                myRunSpace.Close();
            }
            return creds;
        }

        private static string GetVaultAppId(string vaultName, string name)
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("get-secret")
                    .AddParameter("Vault", vaultName)
                    .AddParameter("Name", name);

                    foreach (var result in powershell.Invoke())
                    {
                        var secureAppId = (SecureString)result.BaseObject;
                        return SecureStringToString(secureAppId);
                    }

                }
                myRunSpace.Close();
            }
            return null;
        }


        private static void AddVaultCredential(string vaultName, string name, string username, SecureString password)
        {
            PSCredential creds = new PSCredential(username, password);

            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("set-secret")
                    .AddParameter("Vault", vaultName)
                    .AddParameter("Name", name)
                    .AddParameter("Secret", creds);

                    powershell.Invoke();
                }
                myRunSpace.Close();
            }
        }

        private static void AddVaultAppId(string vaultName, string name, string appId)
        {
            // PSCredential creds = new PSCredential(username, password);

            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("set-secret")
                    .AddParameter("Vault", vaultName)
                    .AddParameter("Name", name)
                    .AddParameter("Secret", appId);

                    powershell.Invoke();
                }
                myRunSpace.Close();
            }
        }

        private static void RemoveVaultCredential(string vaultName, string name)
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();

            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;

                    // Create a pipeline with the Get-Command command.
                    powershell.AddCommand("remove-secret")
                    .AddParameter("Vault", vaultName)
                    .AddParameter("Name", name);

                    powershell.Invoke();
                }
                myRunSpace.Close();
            }
        }

        private static PSCredential ReadWindowsCredentialManagerEntry(string applicationName)
        {
            IntPtr credPtr;

            bool success = CredRead(applicationName, CRED_TYPE.GENERIC, 0, out credPtr);
            if (success)
            {
                var critCred = new CriticalCredentialHandle(credPtr);
                var cred = critCred.GetCredential();
                var username = cred.UserName;
                var securePassword = StringToSecureString(cred.CredentialBlob);
                return new PSCredential(username, securePassword);
            }
            return null;
        }

        private static bool DeleteWindowsCredentialManagerEntry(string applicationName)
        {
            bool success = CredDelete(applicationName, CRED_TYPE.GENERIC, 0);
            return success;
        }


        private static void WriteWindowsCredentialManagerEntry(string applicationName, string userName, SecureString securePassword)
        {
            var password = SecureStringToString(securePassword);

            byte[] byteArray = password == null ? null : Encoding.Unicode.GetBytes(password);
            if (Environment.OSVersion.Version < new Version(6, 1))
            {
                if (byteArray != null && byteArray.Length > 512)
                    throw new ArgumentOutOfRangeException("password", "The password has exceeded 512 bytes.");
            }
            else
            {
                if (byteArray != null && byteArray.Length > 512 * 5)
                    throw new ArgumentOutOfRangeException("password", "The password has exceeded 2560 bytes.");
            }

            NativeCredential credential = new NativeCredential();
            credential.AttributeCount = 0;
            credential.Attributes = IntPtr.Zero;
            credential.Comment = IntPtr.Zero;
            credential.TargetAlias = IntPtr.Zero;
            credential.Type = CRED_TYPE.GENERIC;
            credential.Persist = (uint)3;
            credential.CredentialBlobSize = (uint)(byteArray == null ? 0 : byteArray.Length);
            credential.TargetName = Marshal.StringToCoTaskMemUni(applicationName);
            credential.CredentialBlob = Marshal.StringToCoTaskMemUni(password);
            credential.UserName = Marshal.StringToCoTaskMemUni(userName ?? Environment.UserName);

            bool written = CredWrite(ref credential, 0);
            Marshal.FreeCoTaskMem(credential.TargetName);
            Marshal.FreeCoTaskMem(credential.CredentialBlob);
            Marshal.FreeCoTaskMem(credential.UserName);

            if (!written)
            {
                int lastError = Marshal.GetLastWin32Error();
                throw new Exception($"CredWrite failed with the error code {lastError}");
            }
        }

        private static PSCredential ReadMacOSKeyChainEntry(string applicationName)
        {
            var keychain = new MacOSKeychain();
            var credential = keychain.Get(applicationName, applicationName);
            if (credential != null)
            {
                SecureString pw = new SecureString();
                foreach (char c in credential.Password)
                {
                    pw.AppendChar(c);
                }
                return new PSCredential(credential.Account, pw);
            }
            return null;
        }
        private static void WriteMacOSKeyChainEntry(string applicationName,string password)
        {
            var keychain = new MacOSKeychain();
            keychain.AddOrUpdate(applicationName, applicationName, password.ToByteArray());
        }

        private static bool DeleteMacOSKeyChainEntry(string name)
        {
            var keychain = new MacOSKeychain();
            return keychain.Remove(name,name);
            // var cmd = $"/usr/bin/security delete-generic-password -s '{name}'";
            // var output = Shell.Bash(cmd);
            // var success = output.Count > 1 && !output[0].StartsWith("security:");
            // return success;
        }

        private static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        private static SecureString StringToSecureString(string inputString)
        {
            var securityString = new SecureString();
            char[] chars = inputString.ToCharArray();
            foreach (var c in chars)
            {
                securityString.AppendChar(c);
            }
            return securityString;
        }
        #endregion

        #region UNMANAGED
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NativeCredential
        {
            public UInt32 Flags;
            public CRED_TYPE Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public FILETIME LastWritten;
            public UInt32 CredentialBlobSize;
            public IntPtr CredentialBlob;
            public UInt32 Persist;
            public UInt32 AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;

            internal static NativeCredential GetNativeCredential(Credential cred)
            {
                NativeCredential ncred = new NativeCredential();
                ncred.AttributeCount = 0;
                ncred.Attributes = IntPtr.Zero;
                ncred.Comment = IntPtr.Zero;
                ncred.TargetAlias = IntPtr.Zero;
                ncred.Type = CRED_TYPE.GENERIC;
                ncred.Persist = (UInt32)1;
                ncred.CredentialBlobSize = (UInt32)cred.CredentialBlobSize;
                ncred.TargetName = Marshal.StringToCoTaskMemUni(cred.TargetName);
                ncred.CredentialBlob = Marshal.StringToCoTaskMemUni(cred.CredentialBlob);
                ncred.UserName = Marshal.StringToCoTaskMemUni(Environment.UserName);
                return ncred;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Credential
        {
            public UInt32 Flags;
            public CRED_TYPE Type;
            public string TargetName;
            public string Comment;
            public FILETIME LastWritten;
            public UInt32 CredentialBlobSize;
            public string CredentialBlob;
            public UInt32 Persist;
            public UInt32 AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        public enum CRED_PERSIST : uint
        {
#pragma warning disable CA1712 // Do not prefix enum values with type name
            CRED_PERSIST_SESSION = 1,

            CRED_PERSIST_LOCAL_MACHINE = 2,

            CRED_PERSIST_ENTERPRISE = 3
#pragma warning restore CA1712 // Do not prefix enum values with type name
        }
        public enum CRED_TYPE : uint
        {
            GENERIC = 1,
            DOMAIN_PASSWORD = 2,
            DOMAIN_CERTIFICATE = 3,
            DOMAIN_VISIBLE_PASSWORD = 4,
            GENERIC_CERTIFICATE = 5,
            DOMAIN_EXTENDED = 6,
            MAXIMUM = 7,      // Maximum supported cred type
            MAXIMUM_EX = (MAXIMUM + 1000),  // Allow new applications to run on old OSes
        }

        public class CriticalCredentialHandle : CriticalHandleZeroOrMinusOneIsInvalid
        {
            public CriticalCredentialHandle(IntPtr preexistingHandle)
            {
                SetHandle(preexistingHandle);
            }

            public Credential GetCredential()
            {
                if (!IsInvalid)
                {
                    NativeCredential ncred = (NativeCredential)Marshal.PtrToStructure(handle,
                          typeof(NativeCredential));
                    Credential cred = new Credential();
                    cred.CredentialBlobSize = ncred.CredentialBlobSize;
                    cred.CredentialBlob = Marshal.PtrToStringUni(ncred.CredentialBlob,
                          (int)ncred.CredentialBlobSize / 2);
                    cred.UserName = Marshal.PtrToStringUni(ncred.UserName);
                    cred.TargetName = Marshal.PtrToStringUni(ncred.TargetName);
                    cred.TargetAlias = Marshal.PtrToStringUni(ncred.TargetAlias);
                    cred.Type = ncred.Type;
                    cred.Flags = ncred.Flags;
                    cred.Persist = ncred.Persist;
                    return cred;
                }
                else
                {
                    throw new InvalidOperationException("Invalid CriticalHandle!");
                }
            }

            override protected bool ReleaseHandle()
            {
                if (!IsInvalid)
                {
                    CredFree(handle);
                    SetHandleAsInvalid();
                    return true;
                }
                return false;
            }
        }



        [DllImport("Advapi32.dll", SetLastError = true, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode)]
        private static extern bool CredWrite([In] ref NativeCredential userCredential, [In] UInt32 flags);

        [DllImport("Advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CredRead(string target, CRED_TYPE type, int reservedFlag, out IntPtr CredentialPtr);

        [DllImport("Advapi32.dll", EntryPoint = "CredFree", SetLastError = true)]
        private static extern bool CredFree([In] IntPtr cred);

        [DllImport("Advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool CredDelete(string target, CRED_TYPE type, int reservedFlag);
        #endregion
    }
}
