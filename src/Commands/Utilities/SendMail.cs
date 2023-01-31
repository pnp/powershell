using System.Management.Automation;

using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Utilities
{
    [Cmdlet(VerbsCommunications.Send, "PnPMail")]
    public class SendMail : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Server = "smtp.office365.com";

        [Parameter(Mandatory = false)]
        public string From;

        [Parameter(Mandatory = false)]
        public string Password;

        [Parameter(Mandatory = true)]
        public string[] To;

        [Parameter(Mandatory = false)]
        public string[] Cc;

        [Parameter(Mandatory = false)]
        public string[] Bcc;        

        [Parameter(Mandatory = true)]
        public string Subject;

        [Parameter(Mandatory = true)]
        public string Body;
        
        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(Password) && string.IsNullOrWhiteSpace(From))
            {
                MailUtility.SendEmail(ClientContext, To, Cc, Bcc, Subject, Body);
            }
            else
            {
                MailUtility.SendEmail(Server, From, Password, To, Cc, Bcc, Subject, Body);
            }
        }
    }
}
