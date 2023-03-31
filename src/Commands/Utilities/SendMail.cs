using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Mail;
using System.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    [Cmdlet(VerbsCommunications.Send, "PnPMail", DefaultParameterSetName = ParameterSet_SENDTHROUGHSPO)]
    public class SendMail : PnPWebCmdlet
    {
        private const string ParameterSet_SENDTHROUGHGRAPH = "Send through Microsoft Graph";
        private const string ParameterSet_SENDTHROUGHSPO = "Send through SharePoint Online";
        private const string ParameterSet_SENDTHROUGHSMTP = "Send through SMTP";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public string Server;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public short? ServerPort;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public bool? EnableSsl;             

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public string From;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public string Username;        

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public string Password;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        public string[] To;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        public string[] Cc;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        public string[] Bcc;        

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        public string Subject;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        public string Body;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public MessageImportanceType Importance { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        public string[] ReplyTo { get; set; }         

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        public bool? SaveToSentItems { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSMTP)]
        public MessageBodyContentType? BodyContentType { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(Password) && string.IsNullOrWhiteSpace(From))
            {
                WriteVerbose("Sending e-mail through SharePoint Online");
                MailUtility.SendSharePointEmail(ClientContext, Subject, Body, To, Cc, Bcc).GetAwaiter().GetResult();
            }
            else
            {
                if(ParameterSpecified(nameof(Server)) && !string.IsNullOrWhiteSpace(Server))
                {
                    WriteVerbose($"Sending e-mail directly through SMTP server {Server}");
                    MailUtility.SendSmtpEmail(Subject, Body, From, To, Cc, Bcc, Importance, Server, ServerPort, EnableSsl, Username, Password, BodyContentType ?? MessageBodyContentType.Html).GetAwaiter().GetResult();
                }
                else
                {
                    WriteVerbose($"Sending e-mail using Microsoft Graph");
                    MailUtility.SendGraphMail(Connection, GraphAccessToken, new Message
                    {
                        Subject = Subject,
                        MessageBody = new Body
                        {
                            Content = Body,
                            ContentType = BodyContentType ?? MessageBodyContentType.Text
                        },
                        ToRecipients = To.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t }}).ToList(),
                        CcRecipients = Cc?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t }}).ToList(),
                        BccRecipients = Bcc?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t }}).ToList(),
                        Sender = new Recipient { EmailAddress = new EmailAddress { Address = From } },
                        ReplyTo = ReplyTo?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t }}).ToList(),
                        Importance = Importance
                    }, SaveToSentItems ?? true).GetAwaiter().GetResult();                    
                }
            }

            WriteVerbose($"E-mail sent successfully");
        }
    }
}
