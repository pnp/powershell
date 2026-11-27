using System.IO;
using System.Security.Cryptography;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using PnP.PowerShell.Commands.Properties;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Persists an app-only MSAL cache using secure storage, propagating storage failures to the caller.
    /// </summary>
    internal sealed class AppOnlyTokenCache
    {
        private readonly StorageCreationProperties _properties;
        private readonly Storage _storage;
        private CrossPlatLock _cacheLock;

        internal AppOnlyTokenCache(StorageCreationProperties properties)
        {
            _properties = properties;
            _storage = Storage.Create(properties);
        }

        /// <summary>Verifies that secure storage is available before attaching the cache.</summary>
        internal void VerifyPersistence()
        {
            using var cacheLock = CreateLock();
            _storage.VerifyPersistence();
        }

        /// <summary>Registers synchronous callbacks so token acquisition observes storage failures.</summary>
        internal void RegisterCache(ITokenCache tokenCache)
        {
            tokenCache.SetBeforeAccess(BeforeAccess);
            tokenCache.SetAfterAccess(AfterAccess);
        }

        /// <summary>Clears the isolated app-only cache, failing if its contents cannot be removed.</summary>
        internal void Clear()
        {
            using var cacheLock = CreateLock();
            if (System.OperatingSystem.IsWindows())
            {
                File.Delete(_properties.CacheFilePath);
            }
            else
            {
                _storage.Clear(ignoreExceptions: false);
            }
            if (ReadData().Length != 0)
            {
                throw new IOException(Resources.PersistedLoginCacheNotCleared);
            }
        }

        private CrossPlatLock CreateLock()
        {
            return new CrossPlatLock(_properties.CacheFilePath + ".lockfile", _properties.LockRetryDelay, _properties.LockRetryCount);
        }

        private byte[] ReadData()
        {
            if (!System.OperatingSystem.IsWindows())
            {
                return _storage.ReadData();
            }

            // MSAL's Windows file accessor suppresses I/O failures; use the same DPAPI format without suppressing errors.
            byte[] data;
            try
            {
                data = File.ReadAllBytes(_properties.CacheFilePath);
            }
            catch (FileNotFoundException)
            {
                return [];
            }
            return data.Length == 0 ? data : ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
        }

        private void WriteData(byte[] data)
        {
            if (System.OperatingSystem.IsWindows())
            {
                File.WriteAllBytes(_properties.CacheFilePath, ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser));
            }
            else
            {
                _storage.WriteData(data);
            }
        }

        private void BeforeAccess(TokenCacheNotificationArgs args)
        {
            // Keep MSAL's read/modify/write cycle under the same cross-process lock.
            _cacheLock = CreateLock();
            try
            {
                args.TokenCache.DeserializeMsalV3(ReadData(), shouldClearExistingCache: true);
            }
            catch
            {
                ReleaseLock();
                throw;
            }
        }

        private void AfterAccess(TokenCacheNotificationArgs args)
        {
            try
            {
                if (args.HasStateChanged)
                {
                    WriteData(args.TokenCache.SerializeMsalV3());
                }
            }
            finally
            {
                ReleaseLock();
            }
        }

        private void ReleaseLock()
        {
            _cacheLock?.Dispose();
            _cacheLock = null;
        }
    }
}
