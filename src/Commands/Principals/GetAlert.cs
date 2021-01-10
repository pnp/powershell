using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPAlert")]
    public class GetAlert : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public UserPipeBind User;

        [Parameter(Mandatory = false)]
        public string Title;

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }

            var alert = new AlertCreationInformation();

            User user;
            if (null != User)
            {
                user = User.GetUser(ClientContext);
                if (user == null)
                {
                    throw new ArgumentException("Unable to find user", "Identity");
                }
            }
            else
            {
                user = CurrentWeb.CurrentUser;
            }

            user.EnsureProperty(u => u.Alerts.IncludeWithDefaultProperties(a => a.ListID));
            if (list != null && !string.IsNullOrWhiteSpace(Title))
            {
                WriteObject(user.Alerts.Where(l => l.ListID == list.Id && l.Title == Title), true);
            }
            else if (list != null)
            {
                WriteObject(user.Alerts.Where(l => l.ListID == list.Id), true);
            }
            else if (!string.IsNullOrWhiteSpace(Title))
            {
                WriteObject(user.Alerts.Where(l => l.Title == Title), true);
            }
            else
            {
                WriteObject(user.Alerts, true);
            }
        }
    }
}