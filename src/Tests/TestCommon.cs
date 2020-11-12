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
                throw new ConfigurationErrorsException("AppSettings is empty");
            }
            // Read configuration data
            DevSiteUrl = configuration.AppSettings.Settings["SiteUrl"].Value;
            var credentialManagerEntry = configuration.AppSettings.Settings["CredentialManagerLabel"].Value;
            if (string.IsNullOrEmpty(DevSiteUrl))
            {
                throw new ConfigurationErrorsException("site url in App.config are not set up.");
            }

            // Trim trailing slashes
            DevSiteUrl = DevSiteUrl.TrimEnd(new[] { '/' });

            Credentials = PnP.PowerShell.Commands.Utilities.CredentialManager.GetCredential(credentialManagerEntry);
        }
        #endregion

        #region Properties
        public static string TenantUrl { get; set; }
        public static string DevSiteUrl { get; set; }
        public static string Dev2SiteUrl { get; set; }
        static string UserName { get; set; }
        static SecureString Password { get; set; }
        static PSCredential Credentials { get; set; }
        static string Realm { get; set; }
        static string AppId { get; set; }
        static string AppSecret { get; set; }

        public static String AzureStorageKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AzureStorageKey"];
            }
        }

        public static string WebHookTestUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WebHookTestUrl"];
            }
        }
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

        public static bool AppOnlyTesting()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppId"]) &&
                !String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOCredentialManagerLabel"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOUserName"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOPassword"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremUserName"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremDomain"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremPassword"]))
            {
                return true;
            }
            else
            {
                return false;
            }
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
