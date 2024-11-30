using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    [Cmdlet(VerbsCommunications.Send, "PnPMail", DefaultParameterSetName = ParameterSet_SENDTHROUGHSPO)]
    public class SendMail : PnPWebCmdlet
    {
        private const string ParameterSet_SENDTHROUGHGRAPH = "Send through Microsoft Graph with attachments from local file system";
        private const string ParameterSet_SENDTHROUGHSPO = "Send through SharePoint Online";
        private const string ParameterSet_SENDTHROUGHGRAPHWITHFILES = "Send through Microsoft Graph with attachments from SPO";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string From;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string[] To;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string[] Cc;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string[] Bcc;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string Subject;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHSPO)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string Body;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public MessageImportanceType Importance;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public string[] ReplyTo;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public bool? SaveToSentItems;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public MessageBodyContentType? BodyContentType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPH)]
        public string[] Attachments;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SENDTHROUGHGRAPHWITHFILES)]
        public FilePipeBind[] Files;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(From))
            {
                WriteVerbose("Sending e-mail through SharePoint Online");
                WriteWarning("\n The SharePoint SendEmail API will be retired on October 31, 2025, and this method of sending emails will stop working. \n Please update your script to use Microsoft Graph as described here: https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html#send-through-microsoft-graph \n Learn more: https://devblogs.microsoft.com/microsoft365dev/retirement-of-the-sharepoint-sendemail-api");
                MailUtility.SendSharePointEmail(ClientContext, Subject, Body, To, Cc, Bcc);
            }
            else
            {
                WriteVerbose($"Sending e-mail using Microsoft Graph");
                List<MessageAttachmentOptions> messageAttachmentOptions = null;
                if (ParameterSpecified(nameof(Attachments)))
                {
                    messageAttachmentOptions = MailUtility.GetListOfAttachments(Attachments, SessionState.Path.CurrentFileSystemLocation.Path);
                }
                else if (ParameterSpecified(nameof(Files)))
                {
                    messageAttachmentOptions = MailUtility.GetListOfFiles(Files, Connection.PnPContext);
                }

                MailUtility.SendGraphMail(RequestHelper, new Message
                {
                    Subject = Subject,
                    MessageBody = new Body
                    {
                        Content = Body,
                        ContentType = BodyContentType ?? MessageBodyContentType.Text
                    },
                    ToRecipients = To.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t } }).ToList(),
                    CcRecipients = Cc?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t } }).ToList(),
                    BccRecipients = Bcc?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t } }).ToList(),
                    Sender = new Recipient { EmailAddress = new EmailAddress { Address = From } },
                    ReplyTo = ReplyTo?.Select(t => new Recipient { EmailAddress = new EmailAddress { Address = t } }).ToList(),
                    Importance = Importance,
                    Attachments = messageAttachmentOptions
                }, SaveToSentItems ?? true);
            }

            WriteVerbose($"E-mail sent successfully");
        }
    }
}
