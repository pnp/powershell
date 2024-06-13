using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Model.Mail;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using Microsoft.SharePoint.Client.Utilities;
using System.Net.Mail;
using System.Net;
using PnP.PowerShell.Commands.Enums;
using System.IO;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utility class that allows working with e-mail messages
    /// </summary>
    internal static class MailUtility
    {
        /// <summary>
        /// Sends an e-mail message using Microsoft Graph
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accessToken">Microsoft Graph access token having the Mail.Send permission</param>
        /// <param name="message">The message to send</param>
        /// <param name="saveToSentItems">Boolean indicating if the sent message should be added to the sent items of the sender. Optional. Defaults to true.</param>
        /// <exception cref="System.Exception">Thrown if sending the e-mail failed</exception>
        public static async Task SendGraphMail(Cmdlet cmdlet, PnPConnection connection, string accessToken, Message message, bool saveToSentItems = true)
        {
            var jsonSerializer = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, };
            jsonSerializer.Converters.Add(new JsonStringEnumConverter());

            var stringContent = new StringContent(JsonSerializer.Serialize(new SendMailMessage { Message = message, SaveToSentItems = saveToSentItems }, jsonSerializer));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await GraphHelper.PostAsync(cmdlet, connection, $"v1.0/users/{message.Sender.EmailAddress.Address}/sendMail", accessToken, stringContent);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new System.Exception($"Error sending e-mail message: {response.ReasonPhrase}. {(await response.Content.ReadAsStringAsync())}");
            }
        }

        /// <summary>
        /// Sends an email using the SharePoint SendEmail method
        /// </summary>
        /// <param name="context">Context for SharePoint objects and operations</param>
        /// <param name="subject">Subject of the mail.</param>
        /// <param name="body">HTML body of the mail.</param>
        /// <param name="to">List of TO addresses.</param>
        /// <param name="cc">List of CC addresses. Optional.</param>
        /// <param name="bcc">List of BCC addresses. Optional.</param>
        public static async Task SendSharePointEmail(ClientContext context, string subject, string body, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
        {
            EmailProperties properties = new EmailProperties
            {
                To = to
            };

            if (cc != null)
            {
                properties.CC = cc;
            }

            if (bcc != null)
            {
                properties.BCC = bcc;
            }

            properties.Subject = subject;
            properties.Body = body;

            Microsoft.SharePoint.Client.Utilities.Utility.SendEmail(context, properties);
            await context.ExecuteQueryRetryAsync();
        }

        /// <summary>
        /// Sends an email via SMTP
        /// </summary>
        /// <param name="subject">Subject of the mail.</param>
        /// <param name="body">HTML body of the mail.</param>
        /// <param name="fromAddress">The address to use as the sender address</param>
        /// <param name="to">List of TO addresses.</param>
        /// <param name="cc">List of CC addresses. Optional.</param>
        /// <param name="bcc">List of BCC addresses. Optional.</param>
        /// <param name="servername">SMTP server address. By default this is smtp.office365.com.</param>
        /// <param name="serverPort">SMTP server port. By default this is 25.</param>
        /// <param name="enableSsl">Boolean indicating if SSL should be used. By default this is false.</param>
        /// <param name="username">Username to authenticate to the SMTP server with. Leave NULL to not authenticate.</param>
        /// <param name="password">Password to authenticate to the SMTP server with. Leave NULL to not authenticate.</param>
        /// <param name="contentType">Content type of the body. By default this is HTML.</param>
        public static async Task SendSmtpEmail(string subject, string body, string fromAddress, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null, MessageImportanceType? importance = null, string servername = "smtp.office365.com", short? serverPort = null, bool? enableSsl = null, string username = null, string password = null, MessageBodyContentType contentType = MessageBodyContentType.Html)
        {
            using SmtpClient client = new SmtpClient(servername)
            {
                Port = (int)serverPort.GetValueOrDefault(25),
                EnableSsl = enableSsl.GetValueOrDefault(false),
                Credentials = new NetworkCredential(username, password)
            };

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                client.Credentials = new NetworkCredential(username, password);
            }

            using MailMessage mail = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = contentType == MessageBodyContentType.Html
            };

            if (importance.HasValue)
            {
                switch (importance.Value)
                {
                    case MessageImportanceType.High:
                        mail.Priority = MailPriority.High;
                        break;
                    case MessageImportanceType.Low:
                        mail.Priority = MailPriority.Low;
                        break;
                    case MessageImportanceType.Normal:
                        mail.Priority = MailPriority.Normal;
                        break;
                }
            }

            foreach (string user in to)
            {
                mail.To.Add(user);
            }

            if (cc != null)
            {
                foreach (string user in cc)
                {
                    mail.CC.Add(user);
                }
            }

            if (bcc != null)
            {
                foreach (string user in bcc)
                {
                    mail.Bcc.Add(user);
                }
            }

            await client.SendMailAsync(mail);
        }

        /// <summary>
        /// Gets the list of attachments to be sent via email
        /// </summary>
        /// <param name="attachments">The list of attachments to be sent</param>
        /// <param name="currentPath">The current path of the directory</param>
        /// <returns></returns>
        public static List<MessageAttachmentOptions> GetListOfAttachments(string[] attachments, string currentPath)
        {
            if (attachments == null || attachments?.Length == 0)
            {
                return null;
            }

            List<MessageAttachmentOptions> messageAttachmentOptions = new List<MessageAttachmentOptions>();
            foreach (var attachment in attachments)
            {
                MessageAttachmentOptions item = new MessageAttachmentOptions();
                var file = attachment;
                if (!System.IO.Path.IsPathRooted(attachment))
                {
                    file = System.IO.Path.Combine(currentPath, attachment);
                }

                FileInfo attachmentFile = new FileInfo(file);
                item.Type = "#microsoft.graph.fileAttachment";
                item.Name = attachmentFile.Name;
                var fileByteArray = System.IO.File.ReadAllBytes(file);
                item.ContentBytes = Convert.ToBase64String(fileByteArray);

                MimeTypeMap.TryGetMimeType(attachmentFile.Name, out var mimeType);
                item.ContentType = mimeType ?? "text/plain";
                messageAttachmentOptions.Add(item);
            }

            return messageAttachmentOptions;
        }
    }
}