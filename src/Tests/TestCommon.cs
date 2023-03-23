using Microsoft.SharePoint.Client;
using System;
using System.Configuration;
using System.Security;
using System.Net;
using Core = PnP.Framework;
using System.Threading;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Tests
{
    static class TestCommon
    {
        #region Constructor
        static TestCommon()
        {
            Configuration = new Configuration();
        }
        #endregion

        public static Configuration Configuration { get; set; }

        public static string WebHookTestUrl { get; set; }

        private static PSTestScope testScope;
        public static PSTestScope GetTestScope()
        {
            if (testScope == null)
            {
                testScope = new PSTestScope();
            }
            return testScope;
        }

        #region Methods
        public static ClientContext CreateClientContext()
        {
            return CreateContext(Configuration.SiteUrl, Configuration.Credentials);
        }

        public static ClientContext CreateTenantClientContext()
        {
            var tenantUrl = UrlUtilities.GetTenantAdministrationUrl(Configuration.SiteUrl);
            return CreateContext(tenantUrl, Configuration.Credentials);
        }

        public static ClientContext CreateClientContext(string siteUrl)
        {
            return CreateContext(siteUrl, Configuration.Credentials);
        }

        private static ClientContext CreateContext(string contextUrl, PSCredential credentials)
        {
            ClientContext context = null;
            using (var am = PnP.Framework.AuthenticationManager.CreateWithCredentials(credentials.UserName, credentials.Password))
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

    internal class Configuration
    {
        public string SiteUrl { get; set; }
        public PSCredential Credentials { get; set; }

        public Configuration()
        {
            SiteUrl = Environment.GetEnvironmentVariable("PnPTests_SiteUrl");
            if (string.IsNullOrEmpty(SiteUrl))
            {
                throw new ConfigurationErrorsException("Please set PnPTests_SiteUrl environment variable, or run Run-Tests.ps1 in the build root folder");
            }
            else
            {
                SiteUrl = SiteUrl.TrimEnd(new[] { '/' });

            }
            var credLabel = Environment.GetEnvironmentVariable("PnPTests_CredentialManagerLabel");
            if (string.IsNullOrEmpty(credLabel))
            {
                var username = Environment.GetEnvironmentVariable("PnPTests_Username");
                var password = Environment.GetEnvironmentVariable("PnPTests_Password");
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    Credentials = new PSCredential(username, ConvertFromBase64String(password));
                }
            }
            else
            {
                Credentials = PnP.PowerShell.Commands.Utilities.CredentialManager.GetCredential(credLabel);
            }
            if (Credentials == null)
            {
                throw new ConfigurationErrorsException("Please set PnPTests_CredentialManagerLabel or PnPTests_Username and PnPTests_Password, or run Run-Tests.ps1 in the build root folder");
            }
        }

        private SecureString ConvertToSecureString(string input)
        {
            var secureString = new SecureString();

            foreach (char c in input)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();
            return secureString;
        }

        private SecureString ConvertFromBase64String(string input)
        {
            var iss = InitialSessionState.CreateDefault();

            using (var rs = RunspaceFactory.CreateRunspace(iss))
            {

                rs.Open();

                var pipeLine = rs.CreatePipeline();

                var cmd = new Command("ConvertTo-SecureString");
                cmd.Parameters.Add("String", input);
                pipeLine.Commands.Add(cmd);
                var results = pipeLine.Invoke();
                if (results.Count > 0)
                {
                    return results[0].BaseObject as SecureString;
                }
            }
            return null;
        }
    }
}

