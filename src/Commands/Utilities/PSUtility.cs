using System;
using System.Linq;
using System.Reflection;

namespace PnP.PowerShell.Commands.Utilities
{
    internal class PSUtility
    {
        public static string PSVersion => (PSVersionLazy.Value);

        public static readonly Lazy<string> PSVersionLazy = new Lazy<string>(
            () =>

            {
                var caller = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "System.Management.Automation");
                //var caller = Assembly.GetCallingAssembly();
                var psVersionType = caller.GetType("System.Management.Automation.PSVersionInfo");
                if (null != psVersionType)
                {
                    PropertyInfo propInfo = psVersionType.GetProperty("PSVersion");
                    if (null == propInfo)
                    {
                        propInfo = psVersionType.GetProperty("PSVersion", BindingFlags.NonPublic | BindingFlags.Static);
                    }
                    var getter = propInfo.GetGetMethod(true);
                    var version = getter.Invoke(null, new object[] { });

                    if (null != version)
                    {
                        var versionType = version.GetType();
                        var versionProperty = versionType.GetProperty("Major");
                        var minorVersionProperty = versionType.GetProperty("Minor");
                        return ((int)versionProperty.GetValue(version)).ToString() + "." + ((int)minorVersionProperty.GetValue(version)).ToString();
                    }
                }
                return "";
            });

#pragma warning disable CA1416 // Validate platform compatibility
        public static bool IsUserLocalAdmin()
        {
            if (OperatingSystem.IsWindows() && PSVersion == "7.5")
            {
                using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                var isAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

                return isAdmin;
            }
            return true;
        }
#pragma warning restore CA1416 // Validate platform compatibility

        public static bool IsAzureCloudShell()
        {
            string psDistChannel = Environment.GetEnvironmentVariable("POWERSHELL_DISTRIBUTION_CHANNEL");
            if (string.IsNullOrWhiteSpace(psDistChannel))
            {
                return false;
            }

            return psDistChannel == "CloudShell";
        }
    }
}
