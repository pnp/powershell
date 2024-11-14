using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPAlert", DefaultParameterSetName = ParameterSet_SPECIFICUSER)]
    [OutputType(typeof(Alert))]
    public class GetAlert : PnPWebCmdlet
    {
        private const string ParameterSet_SPECIFICUSER = "Alerts for a specific user";
        private const string ParameterSet_ALLUSERS = "Alerts for all users";

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_SPECIFICUSER)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ALLUSERS)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICUSER)]
        public UserPipeBind User;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICUSER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLUSERS)]
        public string Title;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLUSERS)]
        public SwitchParameter AllUsers;

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }

            if (!ParameterSpecified(nameof(AllUsers)))
            {
                // Return alerts for a specific user
                User user;
                if (null != User)
                {
                    user = User.GetUser(ClientContext);
                    if (user == null)
                    {
                        throw new ArgumentException("Unable to find user", nameof(User));
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
            else
            {
                // Return alerts for all users
                ClientContext.Load(CurrentWeb.Alerts);
                if (list != null)
                {
                    ClientContext.Load(CurrentWeb.Alerts, a => a.Include(b => b.ListID));
                }
                ClientContext.ExecuteQueryRetry();

                if(list != null)
                {
                    // Return all alerts on the specified list for all users
                    if (string.IsNullOrWhiteSpace(Title))
                    {
                        WriteObject(CurrentWeb.Alerts.Where(a => a.ListID == list.Id), true);
                    }
                    else
                    {
                        WriteObject(CurrentWeb.Alerts.Where(a => a.ListID == list.Id && a.Title == Title), true);
                    }
                }
                else
                {
                    // Return all alerts for all users
                    WriteObject(CurrentWeb.Alerts, true);
                }
            }
        }
    }
}