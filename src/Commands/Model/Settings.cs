using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Graph;
using Microsoft.Identity.Client.Extensions.Msal;

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
                    // try to load settings
                    var settingsFile = Path.Combine(MsalCacheHelper.UserRootDirectory, ".m365pnppowershell", "settings.json");
                    if (System.IO.File.Exists(settingsFile))
                    {
                        _settings = JsonSerializer.Deserialize<Settings>(System.IO.File.ReadAllText(settingsFile)); ;
                    }
                    else
                    {
                        _settings = new Settings();
                    }
                }
                return _settings;
            }
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