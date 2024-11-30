using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Mail;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public static void SendGraphMail(GraphHelper requestHelper, Message message, bool saveToSentItems = true)
        {
            var jsonSerializer = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, };
            jsonSerializer.Converters.Add(new JsonStringEnumConverter());

            var stringContent = new StringContent(JsonSerializer.Serialize(new SendMailMessage { Message = message, SaveToSentItems = saveToSentItems }, jsonSerializer));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = requestHelper.PostHttpContent($"v1.0/users/{message.Sender.EmailAddress.Address}/sendMail", stringContent);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new System.Exception($"Error sending e-mail message: {response.ReasonPhrase}. {(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())}");
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
        public static void SendSharePointEmail(ClientContext context, string subject, string body, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
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

            Utility.SendEmail(context, properties);
            context.ExecuteQueryRetry();
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
        public static void SendSmtpEmail(string subject, string body, string fromAddress, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null, MessageImportanceType? importance = null, string servername = "smtp.office365.com", short? serverPort = null, bool? enableSsl = null, string username = null, string password = null, MessageBodyContentType contentType = MessageBodyContentType.Html)
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

            client.SendMailAsync(mail).GetAwaiter().GetResult();
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

        public static List<MessageAttachmentOptions> GetListOfFiles(FilePipeBind[] files, PnPContext context)
        {
            if (files == null || files?.Length == 0)
            {
                return null;
            }
            List<MessageAttachmentOptions> messageAttachmentOptions = new List<MessageAttachmentOptions>();
            foreach (var file in files)
            {
                MessageAttachmentOptions item = new MessageAttachmentOptions();
                var attachmentFile = file.GetCoreFile(context);
                item.Type = "#microsoft.graph.fileAttachment";
                item.Name = attachmentFile.Name;
                var fileByteArray = attachmentFile.GetContentBytesAsync().GetAwaiter().GetResult();
                item.ContentBytes = Convert.ToBase64String(fileByteArray);
                MimeTypeMap.TryGetMimeType(attachmentFile.Name, out var mimeType);
                item.ContentType = mimeType ?? "text/plain";
                messageAttachmentOptions.Add(item);
            }
            return messageAttachmentOptions;
        }
    }
}