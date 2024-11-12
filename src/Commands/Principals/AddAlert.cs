using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Add, "PnPAlert")]
    [OutputType(typeof(AlertCreationInformation))]
    public class AddAlert : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public string Title = "Alert";

        [Parameter(Mandatory = false)]
        public UserPipeBind User;

        [Parameter(Mandatory = false)]
        public AlertDeliveryChannel DeliveryMethod = AlertDeliveryChannel.Email;

        [Parameter(Mandatory = false)]
        public AlertEventType ChangeType = AlertEventType.All;

        [Parameter(Mandatory = false)]
        public AlertFrequency Frequency = AlertFrequency.Immediate;

        [Parameter(Mandatory = false)]
        public AlertFilter Filter = AlertFilter.AnythingChanges;

        [Parameter(Mandatory = false)]
        public DateTime Time = DateTime.MinValue;

        [Parameter(Mandatory = false)]
        public string AlertTemplateName;

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }
            if (list != null)
            {
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

                alert.AlertFrequency = Frequency;
                alert.AlertType = AlertType.List;
                alert.AlwaysNotify = false;
                alert.DeliveryChannels = DeliveryMethod;
                var filterValue = Convert.ChangeType(Filter, Filter.GetTypeCode()).ToString();
                alert.Filter = filterValue;                

                // setting the value of Filter sometimes does not work (CSOM < Jan 2017, ...?), so we use a known workaround
                // reference: http://toddbaginski.com/blog/how-to-create-office-365-sharepoint-alerts-with-the-client-side-object-model-csom/
                var properties = new Dictionary<string, string>()
                {
                    { "FilterIndex", filterValue }
                    //Send Me an alert when:
                    // 0 = Anything Changes
                    // 1 = Someone else changes a document
                    // 2 = Someone else changes a document created by me
                    // 3 = Someone else changes a document modified by me
                };
                alert.Properties = properties;

                alert.List = list;
                alert.Status = AlertStatus.On;
                alert.Title = Title;
                alert.User = user;
                alert.EventType = ChangeType;
                if (Time != DateTime.MinValue)
                {
                    alert.AlertTime = Time;
                }

                if (!string.IsNullOrEmpty(AlertTemplateName))
                {
                    alert.AlertTemplateName = AlertTemplateName;
                }
                
                user.Alerts.Add(alert);
                ClientContext.ExecuteQueryRetry();
                WriteObject(alert);
            }
            else
            {
                throw new ArgumentException("Unable to find list", "List");
            }
        }
    }
}