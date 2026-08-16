using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.Win32.SafeHandles;
using PnP.Framework.Modernization.Cache;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

[assembly: InternalsVisibleTo("PnP.PowerShell.Tests")]
namespace PnP.PowerShell.Commands.Utilities
{
    internal static class CredentialManager
    {
        private const string LinuxManagedAppIdSchemaName = "pnp.powershell.managedappid";
        private const string LinuxManagedAppIdSecretLabel = "PnP PowerShell managed App Id";
        private const string LinuxManagedAppIdCacheDirectory = ".m365pnppowershell";

        private const string LinuxCredentialSchemaName = "pnp.powershell.credential";
        private const string LinuxCredentialSecretLabel = "PnP PowerShell credential";

        /// <summary>The prefix every credential is stored under in the credential store native to the operating system.</summary>
        private const string CredentialNamePrefix = "PnPPS:";

        /// <summary>The attribute secret-tool prints the name of a stored credential under.</summary>
        private const string LinuxNameAttributePrefix = "attribute.Name";

        /// <summary>How long an external credential store command may take before it is killed.</summary>
        private const int ProcessTimeoutMilliseconds = 30000;

        /// <summary>How many lines of a failing command's error output are kept to explain the failure.</summary>
        private const int MaximumDiagnosticLines = 3;

        /// <summary>The Windows ERROR_NOT_FOUND, which CredEnumerate returns when no entry matches the filter.</summary>
        private const int ErrorNotFound = 1168;

        public static bool AddCredential(string name, string username, SecureString password, bool overwrite)
        {
            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                AddVaultCredential(defaultVault, name, username, password);
                return true;
            }

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
            else if (OperatingSystem.IsLinux())
            {
                WriteLinuxCredentialEntry(name, username, SecureStringToString(password));
            }
            return true;
        }

        public static bool AddAppId(string name, string appid, bool overwrite)
        {
            if (!name.StartsWith("PnPPSAppId:"))
            {
                name = $"PnPPSAppId:{name}";
            }

            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                AddVaultAppId(defaultVault, name, appid);
                return true;
            }

            var secureAppId = new NetworkCredential(null, appid).SecurePassword;
            if (OperatingSystem.IsWindows())
            {
                WriteWindowsCredentialManagerEntry(name, null, secureAppId);
            }
            else if (OperatingSystem.IsMacOS())
            {
                WriteMacOSKeyChainEntry(name, appid);
            }
            else if (OperatingSystem.IsLinux())
            {
                WriteLinuxAppIdEntry(name, appid);
            }
            return true;
        }

        public static PSCredential GetCredential(string name)
        {
            // check if Microsoft.PowerShell.SecretManagement is available and has a default vault configured
            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                return GetVaultCredential(defaultVault, name);
            }

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
            if (OperatingSystem.IsLinux())
            {
                var cred = ReadLinuxCredentialEntry(name);
                if (cred == null)
                {
                    cred = ReadLinuxCredentialEntry($"PnPPS:{name}");
                }
                return cred;
            }
            return null;
        }

        /// <summary>Enumerates the names credentials are stored under. An empty result only means "nothing is stored" when
        /// <see cref="StoredCredentialList.Warning"/> is not set.</summary>
        public static StoredCredentialList ListCredentials()
        {
            var result = new StoredCredentialList();

            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                result.Source = $"the Microsoft.PowerShell.SecretManagement default vault '{defaultVault}'";
                AddVaultCredentialNames(defaultVault, result);
            }
            else if (OperatingSystem.IsWindows())
            {
                result.Source = "the Windows Credential Manager";
                AddWindowsCredentialManagerEntries(result);
            }
            else if (OperatingSystem.IsMacOS())
            {
                result.Source = "the macOS Keychain";
                AddMacOSKeyChainEntries(result);
            }
            else if (OperatingSystem.IsLinux())
            {
                result.Source = "the Linux Secret Service";
                AddLinuxCredentialEntries(result);
            }
            else
            {
                result.Warning = "Listing stored credentials is not supported on this operating system. Register a default vault through Microsoft.PowerShell.SecretManagement to be able to list stored credentials.";
            }

            // Ordinal, because the Secret Service and the Keychain both hold names differing only in case as separate credentials.
            // Stores that instead treat them as one, such as the Credential Manager, never hand back the pair to begin with
            var names = result.Names
                .Distinct(StringComparer.Ordinal)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            result.Names.Clear();
            result.Names.AddRange(names);

            return result;
        }

        public static string GetAppId(string name)
        {
            if (!name.StartsWith("PnPPSAppId:"))
            {
                name = $"PnPPSAppId:{name}";
            }
            // check if Microsoft.PowerShell.SecretManagement is available
            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                return GetVaultAppId(defaultVault, name);
            }

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
            if (OperatingSystem.IsLinux())
            {
                return ReadLinuxAppIdEntry(name);
            }
            return null;
        }

        public static bool RemoveCredential(string name)
        {
            bool success = false;

            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                RemoveVaultCredential(defaultVault, name);
                return true;
            }

            if (OperatingSystem.IsWindows())
            {
                success = DeleteWindowsCredentialManagerEntry(name);
                if (!success)
                {
                    success = DeleteWindowsCredentialManagerEntry($"PnPPS:{name}");
                }
                return success;
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
            if (OperatingSystem.IsLinux())
            {
                success = DeleteLinuxCredentialEntry(name);
                if (!success)
                {
                    success = DeleteLinuxCredentialEntry($"PnPPS:{name}");
                }
                return success;
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

            var defaultVault = GetDefaultVaultIfAvailable();
            if (!string.IsNullOrEmpty(defaultVault))
            {
                RemoveVaultCredential(defaultVault, name);
                return true;
            }

            if (OperatingSystem.IsWindows())
            {
                success = DeleteWindowsCredentialManagerEntry(name);
            }
            if (OperatingSystem.IsMacOS())
            {
                success = DeleteMacOSKeyChainEntry(name);
                return success;
            }
            if (OperatingSystem.IsLinux())
            {
                success = DeleteLinuxAppIdEntry(name);
                return success;
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

        private static string GetDefaultVaultIfAvailable()
        {
            if (HasSecretManagement())
            {
                return GetDefaultVault();
            }
            return null;
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

        private static void AddVaultCredentialNames(string vaultName, StoredCredentialList result)
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();
                using (var powershell = System.Management.Automation.PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;
                    powershell.AddCommand("get-secretinfo")
                    .AddParameter("Vault", vaultName);

                    try
                    {
                        var skippedByType = 0;

                        foreach (var secretInfo in powershell.Invoke())
                        {
                            var name = secretInfo.Properties["Name"]?.Value?.ToString();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                continue;
                            }

                            var secretType = secretInfo.Properties["Type"]?.Value?.ToString();
                            var isCredential = "PSCredential".Equals(secretType, StringComparison.OrdinalIgnoreCase);

                            // Managed app ids are stored as plain strings. A vault keeps the name given verbatim, so a real
                            // credential may legitimately carry this prefix - only skip it when the secret is not a credential
                            if (!isCredential && name.StartsWith("PnPPSAppId:", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            // Nothing marks which vault secrets PnP wrote, so leave out types this cmdlet could never hand back.
                            // An unreported type is kept, as dropping those would hide usable credentials
                            if (!string.IsNullOrEmpty(secretType) && !isCredential &&
                                !secretType.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
                            {
                                skippedByType++;
                                continue;
                            }

                            result.Names.Add(name);
                        }

                        if (powershell.Streams.Error.Count > 0)
                        {
                            result.Warning = $"The default vault '{vaultName}' reported an error while listing secrets, so the list may be incomplete: {powershell.Streams.Error[0]}";
                        }
                        else if (result.Names.Count == 0 && skippedByType > 0)
                        {
                            // Never let the type filter present "the vault holds nothing of ours" as "the vault is empty"
                            result.Warning = $"The default vault '{vaultName}' holds {skippedByType} secret(s), none of them stored as a PSCredential, which is the only type this cmdlet can return. A credential stored there by another tool can still be retrieved with -Name.";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Warning = $"The default vault '{vaultName}' could not be enumerated: {ex.Message}";
                    }
                }
                myRunSpace.Close();
            }
        }

        private static void AddWindowsCredentialManagerEntries(StoredCredentialList result)
        {
            // Filtering on the prefix in the API rather than afterwards keeps the credentials of other applications - and the secrets
            // CredEnumerate hands back with them - out of this process altogether
            if (!CredEnumerate($"{CredentialNamePrefix}*", 0, out int count, out IntPtr credentials))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (lastError != ErrorNotFound)
                {
                    result.Warning = $"The Windows Credential Manager could not be enumerated. CredEnumerate failed with error code {lastError}.";
                }
                return;
            }

            try
            {
                IntPtr[] credentialPointers = new IntPtr[count];
                Marshal.Copy(credentials, credentialPointers, 0, count);

                foreach (var credentialPointer in credentialPointers)
                {
                    if (credentialPointer == IntPtr.Zero)
                    {
                        continue;
                    }

                    var credential = (NativeCredential)Marshal.PtrToStructure(credentialPointer, typeof(NativeCredential));
                    if (credential.Type != CRED_TYPE.GENERIC)
                    {
                        continue;
                    }

                    var targetName = Marshal.PtrToStringUni(credential.TargetName);
                    if (!string.IsNullOrWhiteSpace(targetName) && targetName.StartsWith(CredentialNamePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Names.Add(targetName.Substring(CredentialNamePrefix.Length));
                    }
                }
            }
            finally
            {
                if (credentials != IntPtr.Zero)
                {
                    CredFree(credentials);
                }
            }
        }

        private static void AddMacOSKeyChainEntries(StoredCredentialList result)
        {
            try
            {
                foreach (var serviceName in new MacOSKeychain().EnumerateServiceNames())
                {
                    if (serviceName.StartsWith(CredentialNamePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Names.Add(serviceName.Substring(CredentialNamePrefix.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                result.Warning = $"The macOS Keychain could not be enumerated: {ex.Message}";
            }
        }

        private static void AddLinuxCredentialEntries(StoredCredentialList result)
        {
            // secret-tool splits its output: measured on libsecret 0.21.4 the attributes go to stderr and the secrets to stdout.
            // Both streams are filtered down to the attribute lines as they arrive, so the names are found and no secret is retained
            if (!TryRunProcess("secret-tool", "search --all Product PnPPowerShell",
                               line => line.TrimStart().StartsWith(LinuxNameAttributePrefix, StringComparison.Ordinal),
                               out var lines, out var error))
            {
                result.Warning = $"The Linux Secret Service could not be enumerated: {error} Ensure that secret-tool (libsecret-tools) is installed and that a Secret Service provider such as GNOME Keyring or KWallet is installed and unlocked.";
                return;
            }

            foreach (var line in lines)
            {
                // secret-tool prints attributes as "attribute.<key> = <value>"
                var separatorIndex = line.IndexOf('=');
                if (separatorIndex < 0)
                {
                    continue;
                }

                var name = line.Substring(separatorIndex + 1).Trim();
                if (name.StartsWith(CredentialNamePrefix, StringComparison.OrdinalIgnoreCase))
                {
                    result.Names.Add(name.Substring(CredentialNamePrefix.Length));
                }
            }
        }

        /// <summary>Runs a command and returns the lines of either stream passing <paramref name="lineFilter"/>, discarding the rest as
        /// they arrive so secrets are never accumulated. Both streams are read concurrently to avoid deadlock, and it is killed on timeout.</summary>
        private static bool TryRunProcess(string fileName, string arguments, Func<string, bool> lineFilter, out List<string> lines, out string error)
        {
            lines = new List<string>();
            error = null;

            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process;
            try
            {
                process = Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                error = $"the command '{fileName}' could not be started: {ex.Message}.";
                return false;
            }

            if (process == null)
            {
                error = $"the command '{fileName}' could not be started.";
                return false;
            }

            using (process)
            {
                var collectedLines = new List<string>();
                var diagnosticLines = new List<string>();
                var collected = new object();

                void ReadStream(StreamReader reader, bool isErrorStream)
                {
                    try
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var isWanted = lineFilter == null || lineFilter(line);
                            lock (collected)
                            {
                                if (isWanted)
                                {
                                    collectedLines.Add(line);
                                }
                                else if (isErrorStream && diagnosticLines.Count < MaximumDiagnosticLines && !line.TrimStart().StartsWith("secret", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Kept only to explain a non-zero exit code. Anything that looks like a secret is left out
                                    diagnosticLines.Add(line.Trim());
                                }
                            }
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // Killing the process on a timeout tears the stream down underneath this loop, which is the intent
                    }
                }

                var readers = new[]
                {
                    Task.Run(() => ReadStream(process.StandardOutput, false)),
                    Task.Run(() => ReadStream(process.StandardError, true))
                };

                try
                {
                    if (!Task.WaitAll(readers, ProcessTimeoutMilliseconds) || !process.WaitForExit(ProcessTimeoutMilliseconds))
                    {
                        KillProcess(process);
                        error = $"the command '{fileName}' did not complete in time.";
                        return false;
                    }
                }
                catch (AggregateException ex)
                {
                    KillProcess(process);
                    error = $"the output of the command '{fileName}' could not be read: {ex.GetBaseException().Message}.";
                    return false;
                }

                if (process.ExitCode != 0)
                {
                    var reason = diagnosticLines.Count > 0 ? $": {string.Join(" ", diagnosticLines)}" : ".";
                    error = $"the command '{fileName}' exited with code {process.ExitCode}{reason}";
                    return false;
                }

                lines = collectedLines;
                return true;
            }
        }

        private static void KillProcess(Process process)
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill(entireProcessTree: true);
                }
            }
            catch
            {
                // The process may have exited on its own in the meantime, which is the outcome this is after anyway
            }
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
        private static void WriteMacOSKeyChainEntry(string applicationName, string password)
        {
            var keychain = new MacOSKeychain();
            keychain.AddOrUpdate(applicationName, applicationName, password.ToByteArray());
        }

        private static bool DeleteMacOSKeyChainEntry(string name)
        {
            var keychain = new MacOSKeychain();
            return keychain.Remove(name, name);
            // var cmd = $"/usr/bin/security delete-generic-password -s '{name}'";
            // var output = Shell.Bash(cmd);
            // var success = output.Count > 1 && !output[0].StartsWith("security:");
            // return success;
        }

        private static Storage CreateLinuxManagedAppIdStorage(string name) =>
            CreateLinuxStorage(name, "pnp.managedappid", LinuxManagedAppIdSchemaName, LinuxManagedAppIdSecretLabel);

        private static Storage CreateLinuxCredentialStorage(string name) =>
            CreateLinuxStorage(name, "pnp.credential", LinuxCredentialSchemaName, LinuxCredentialSecretLabel);

        private static Storage CreateLinuxStorage(string name, string filePrefix, string schemaName, string secretLabel)
        {
            var cacheDir = Path.Combine(MsalCacheHelper.UserRootDirectory, LinuxManagedAppIdCacheDirectory);
            var cacheFileName = $"{filePrefix}.{GetSha256Hash(name)}.cache";

            var properties = new StorageCreationPropertiesBuilder(cacheFileName, cacheDir)
                .WithLinuxKeyring(
                    schemaName: schemaName,
                    collection: MsalCacheHelper.LinuxKeyRingDefaultCollection,
                    secretLabel: secretLabel,
                    attribute1: new KeyValuePair<string, string>("Product", "PnPPowerShell"),
                    attribute2: new KeyValuePair<string, string>("Name", name))
                .Build();

            return Storage.Create(properties);
        }

        /// <summary>
        /// Stores a username and password pair in the Linux Secret Service. Both are needed to be able to hand back a complete
        /// PSCredential later on, so the payload is serialized rather than storing the password on its own.
        /// </summary>
        private static void WriteLinuxCredentialEntry(string name, string username, string password)
        {
            try
            {
                var payload = JsonSerializer.Serialize(new LinuxStoredCredential { Username = username, Password = password });

                var storage = CreateLinuxCredentialStorage(name);
                storage.VerifyPersistence();
                storage.WriteData(Encoding.UTF8.GetBytes(payload));
            }
            catch (MsalCachePersistenceException ex)
            {
                throw new InvalidOperationException("Unable to store the credential in Linux Secret Service. Ensure a Secret Service provider such as GNOME Keyring or KWallet is installed and unlocked, or configure a default vault through Microsoft.PowerShell.SecretManagement.", ex);
            }
        }

        private static PSCredential ReadLinuxCredentialEntry(string name)
        {
            byte[] data;
            try
            {
                data = CreateLinuxCredentialStorage(name).ReadData();
            }
            catch (MsalCachePersistenceException)
            {
                return null;
            }

            if (data == null || data.Length == 0)
            {
                return null;
            }

            var raw = Encoding.UTF8.GetString(data);

            string username = null;
            string password;
            try
            {
                var stored = JsonSerializer.Deserialize<LinuxStoredCredential>(raw);
                username = stored?.Username;
                password = stored?.Password;
            }
            catch (JsonException)
            {
                // Entries written before the username was stored alongside the password hold the raw password only. Reading them back
                // as a credential without a username is still better than discarding a credential the user did successfully store.
                password = raw;
            }

            if (password == null)
            {
                return null;
            }

            var securePassword = new SecureString();
            foreach (var character in password)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();

            return new PSCredential(string.IsNullOrEmpty(username) ? name : username, securePassword);
        }

        private static bool DeleteLinuxCredentialEntry(string name)
        {
            try
            {
                var storage = CreateLinuxCredentialStorage(name);
                if (storage.ReadData() is not { Length: > 0 })
                {
                    return false;
                }

                storage.Clear();
                return true;
            }
            catch (MsalCachePersistenceException)
            {
                return false;
            }
        }

        private sealed class LinuxStoredCredential
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private static void WriteLinuxAppIdEntry(string name, string appId)
        {
            try
            {
                var storage = CreateLinuxManagedAppIdStorage(name);
                storage.VerifyPersistence();
                storage.WriteData(Encoding.UTF8.GetBytes(appId));
            }
            catch (MsalCachePersistenceException ex)
            {
                throw new InvalidOperationException("Unable to store the managed App Id in Linux Secret Service. Ensure a Secret Service provider such as GNOME Keyring or KWallet is installed and unlocked, or configure a default vault through Microsoft.PowerShell.SecretManagement.", ex);
            }
        }

        private static string ReadLinuxAppIdEntry(string name)
        {
            try
            {
                var data = CreateLinuxManagedAppIdStorage(name).ReadData();
                return data == null || data.Length == 0 ? null : Encoding.UTF8.GetString(data);
            }
            catch (MsalCachePersistenceException)
            {
                return null;
            }
        }

        private static bool DeleteLinuxAppIdEntry(string name)
        {
            try
            {
                var storage = CreateLinuxManagedAppIdStorage(name);
                var data = storage.ReadData();
                if (data == null || data.Length == 0)
                {
                    return false;
                }

                storage.Clear(false);
                return true;
            }
            catch (MsalCachePersistenceException)
            {
                return false;
            }
        }

        private static string GetSha256Hash(string value)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();
        }

        public static string SecureStringToString(SecureString value)
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

        [DllImport("Advapi32.dll", EntryPoint = "CredEnumerateW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CredEnumerate(string filter, int flags, out int count, out IntPtr credentials);

        [DllImport("Advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool CredDelete(string target, CRED_TYPE type, int reservedFlag);
        #endregion
    }
}
