using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.Office.Client.TranslationServices;
using PnP.Framework.Provisioning.Model;

namespace PnP.PowerShell.Commands.Model
{
    public class Settings
    {
        private static Settings _settings;

        public List<TokenCacheConfiguration> Cache { get; set; }

        public string LastUserTenant { get; set; }

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
                var json = JsonSerializer.Serialize(_settings);
                System.IO.File.WriteAllText(settingsFile, json);
            }
        }
    }
}