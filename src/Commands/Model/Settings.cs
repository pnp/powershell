using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Identity.Client.Extensions.Msal;
using PnP.PowerShell.Commands.Properties;

namespace PnP.PowerShell.Commands.Model
{
    public class Settings
    {
        private static Settings _settings;

        [JsonPropertyName("Cache")]
        private List<TokenCacheConfiguration> _cache { get; set; }
        public List<TokenCacheConfiguration> Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new List<TokenCacheConfiguration>();
                }
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }

        //public string LastUsedTenant { get; set; }

        public static Settings Current
        {
            get
            {
                if (_settings == null)
                {
                    var settingsFile = Path.Combine(MsalCacheHelper.UserRootDirectory, ".m365pnppowershell", "settings.json");
                    _settings = Load(settingsFile);
                }
                return _settings;
            }
        }

        /// <summary>Loads settings without treating unreadable or invalid files as empty configuration.</summary>
        internal static Settings Load(string settingsFile)
        {
            string json;
            try
            {
                json = File.ReadAllText(settingsFile);
            }
            catch (FileNotFoundException)
            {
                return new Settings();
            }
            catch (DirectoryNotFoundException)
            {
                return new Settings();
            }

            var settings = JsonSerializer.Deserialize<Settings>(json);
            if (settings == null || settings.Cache.Contains(null))
            {
                throw new JsonException(Resources.PersistedLoginSettingsInvalid);
            }
            return settings;
        }

        public void Save()
        {
            if (_settings != null)
            {
                var settingsFile = Path.Combine(MsalCacheHelper.UserRootDirectory, ".m365pnppowershell", "settings.json");

                if (!System.IO.Directory.Exists(Path.Combine(MsalCacheHelper.UserRootDirectory, ".m365pnppowershell")))
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(MsalCacheHelper.UserRootDirectory, ".m365pnppowershell"));
                }
                var json = JsonSerializer.Serialize(_settings);
                System.IO.File.WriteAllText(settingsFile, json);
            }
        }
    }
}
