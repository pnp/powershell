﻿using System;
using System.Linq;
using System.Reflection;

namespace PnP.PowerShell.Commands.Utilities
{
    internal class PSVersionUtility
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
    }
}
