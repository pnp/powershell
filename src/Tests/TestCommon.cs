using Microsoft.SharePoint.Client;
using System;
using System.Configuration;
using System.Security;
using System.Net;
using Core = PnP.Framework;
using System.Threading;
using System.Management.Automation;

namespace PnP.PowerShell.Tests
{
    static class TestCommon
    {
        #region Constructor
        static TestCommon()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration("PnP.PowerShell.Tests.dll");
            if (configuration.AppSettings.Settings.Count == 0)
            {
                throw new ConfigurationErrorsException("AppSettings is empty. Did you copy App.config.sample to App.config and changed the values?");
            }
            // Read configuration data
            DevSiteUrl = configuration.AppSettings.Settings["SiteUrl"].Value;
            if (string.IsNullOrEmpty(DevSiteUrl))
            {
                throw new ConfigurationErrorsException("site url in App.config are not set up.");
            }

            var credentialManagerEntry = configuration.AppSettings.Settings["CredentialManagerLabel"].Value;
            if (string.IsNullOrEmpty(credentialManagerEntry))
            {
                throw new ConfigurationErrorsException("CredentialmanagerLabel value is not set up in App.config");
            }

            // Trim trailing slashes
            DevSiteUrl = DevSiteUrl.TrimEnd(new[] { '/' });

            Credentials = PnP.PowerShell.Commands.Utilities.CredentialManager.GetCredential(credentialManagerEntry);
        }
        #endregion

        #region Properties
        public static string TenantUrl { get; set; }
        public static string DevSiteUrl { get; set; }
        static PSCredential Credentials { get; set; }


        #endregion

        #region Methods
        public static ClientContext CreateClientContext()
        {
            return CreateContext(DevSiteUrl, Credentials);
        }

        public static ClientContext CreateClientContext(string siteUrl)
        {
            return CreateContext(siteUrl, Credentials);
        }

        public static ClientContext CreateTenantClientContext()
        {
            return CreateContext(TenantUrl, Credentials);
        }

        private static ClientContext CreateContext(string contextUrl, PSCredential credentials)
        {
            ClientContext context = null;
            using (var am = new PnP.Framework.AuthenticationManager(credentials.UserName, credentials.Password))
            {
                context = am.GetContext(contextUrl);
            }

            context.RequestTimeout = Timeout.Infinite;
            return context;
        }

        public static string GetTenantRootUrl(ClientContext ctx)
        {
            var uri = new Uri(ctx.Url);
            return $"https://{uri.DnsSafeHost}";
        }
        #endregion
    }
}
