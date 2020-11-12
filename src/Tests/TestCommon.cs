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
            SiteUrl = Environment.GetEnvironmentVariable("PnPTests_SiteUrl");
            var credentialManagerEntry = Environment.GetEnvironmentVariable("PnPTests_CredentialManagerLabel");

            if (string.IsNullOrEmpty(SiteUrl))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_SiteUrl environment variable, or run Run-Tests.ps1 in the build root folder");
            }
            if (string.IsNullOrEmpty(credentialManagerEntry))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_CredentialManagerLabel variable, or run Run-Tests.ps1 in the build root folder");
            }

            // Trim trailing slashes
            SiteUrl = SiteUrl.TrimEnd(new[] { '/' });

            Credentials = PnP.PowerShell.Commands.Utilities.CredentialManager.GetCredential(credentialManagerEntry);
        }
        #endregion

        #region Properties
        public static string TenantUrl { get; set; }
        public static string SiteUrl { get; set; }
        static PSCredential Credentials { get; set; }


        #endregion

        #region Methods
        public static ClientContext CreateClientContext()
        {
            return CreateContext(SiteUrl, Credentials);
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
