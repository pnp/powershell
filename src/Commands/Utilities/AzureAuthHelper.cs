using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class AzureAuthHelper
    {
        private static string CLIENTID = "1950a258-227b-4e31-a9cf-717495945fc2"; // Well-known Azure Management App Id
        internal static async Task<string> AuthenticateAsync(string tenantId, string username, SecureString password, string loginEndPoint = "https://login.microsoftonline.com")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }

            var authority = $"{loginEndPoint}/{tenantId}";
            var scopes = new string[] { "https://graph.microsoft.com/.default" };
            var app = PublicClientApplicationBuilder.Create(CLIENTID).WithAuthority(authority).Build();

            var result = await app.AcquireTokenByUsernamePassword(scopes, username, password).ExecuteAsync();
            return result.AccessToken;
        }

        internal static string AuthenticateDeviceLogin(PSCmdlet cmdlet, string tenantId, ref CancellationToken cancellationToken, string loginEndPoint = "https://login.microsoftonline.com")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }
            var authority = $"{loginEndPoint}/{tenantId}";


            var app = PublicClientApplicationBuilder.Create(CLIENTID).WithAuthority(authority).Build();
            var scopes = new string[] { "https://graph.microsoft.com/.default" };

            var result = app.AcquireTokenWithDeviceCode(scopes, result =>
            {
                cmdlet.Host.UI.Write(result.Message);
                return Task.FromResult(0);
            }).ExecuteAsync(cancellationToken).GetAwaiter().GetResult();
            return result.AccessToken;
        }

        internal static void WriteFormattedWarning(this PSCmdlet cmdlet, string message)
        {

            if (cmdlet.Host.Name == "ConsoleHost")
            {
                var messageLines = new List<string>();
                messageLines.AddRange(message.Split(new[] { '\n' }));
                var wrappedText = new List<string>();
                foreach (var messageLine in messageLines)
                {
                    wrappedText.AddRange(WordWrap(messageLine, 100));
                }

                var notificationColor = "\x1B[7m";
                var resetColor = "\x1B[0m";

                var outMessage = string.Empty;
                foreach (var wrappedLine in wrappedText)
                {
                    var lineToAdd = wrappedLine.PadRight(100);
                    outMessage += $"{notificationColor} {lineToAdd} {resetColor}\n";
                }
                cmdlet.Host.UI.WriteLine($"{notificationColor}\n{outMessage}{resetColor}\n");
            }
            else
            {
                cmdlet.WriteWarning(message);
            }
        }


        private static List<string> WordWrap(string text, int maxLineLength)
        {
            var list = new List<string>();

            int currentIndex;
            var lastWrap = 0;
            var whitespace = new[] { ' ', '\r', '\n', '\t' };
            do
            {
                currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny(new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1);
                if (currentIndex <= lastWrap)
                    currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);
                list.Add(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace));
                lastWrap = currentIndex;
            } while (currentIndex < text.Length);

            return list;
        }
    }
}