using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Enums;
using System;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.New, "PnPGroup")]
    [OutputType(typeof(Group))]
    public class NewGroup : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title = string.Empty;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public SwitchParameter AutoAcceptRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowMembersEditMembership;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisallowMembersViewMembership;

        [Parameter(Mandatory = false)]
        public string RequestToJoinEmail;

        protected override void ExecuteCmdlet()
        {
            var web = CurrentWeb;

            var groupCI = new GroupCreationInformation { Title = Title, Description = Description };

            var group = web.SiteGroups.Add(groupCI);

            ClientContext.Load(group);
            ClientContext.Load(group.Users);
            ClientContext.ExecuteQueryRetry();
            var dirty = false;
            if (AllowRequestToJoinLeave)
            {
                group.AllowRequestToJoinLeave = true;
                dirty = true;
            }

            if (AutoAcceptRequestToJoinLeave)
            {
                group.AutoAcceptRequestToJoinLeave = true;
                dirty = true;
            }
            if (AllowMembersEditMembership)
            {
                group.AllowMembersEditMembership = true;
                dirty = true;
            }
            if (DisallowMembersViewMembership)
            {
                group.OnlyAllowMembersViewMembership = false;
                dirty = true;
            }
            if (!string.IsNullOrEmpty(RequestToJoinEmail))
            {
                group.RequestToJoinLeaveEmailSetting = RequestToJoinEmail;
                dirty = true;
            }

            if (!string.IsNullOrEmpty(Description))
            {
                var groupItem = CurrentWeb.SiteUserInfoList.GetItemById(group.Id);
                CurrentWeb.Context.Load(groupItem, g => g["Notes"]);
                CurrentWeb.Context.ExecuteQueryRetry();

                var groupDescription = groupItem["Notes"]?.ToString();

                if (groupDescription != Description)
                {
                    groupItem["Notes"] = Description;
                    groupItem.Update();
                    dirty = true;
                }

                var plainTextDescription = Framework.Utilities.PnPHttpUtility.ConvertSimpleHtmlToText(Description, int.MaxValue);
                if (group.Description != plainTextDescription)
                {
                    //If the description is more than 512 characters long a server exception will be thrown.
                    group.Description = plainTextDescription;                    
                    dirty = true;
                }
            }

            if (dirty)
            {
                group.Update();
                ClientContext.ExecuteQueryRetry();
            }

            if (!string.IsNullOrEmpty(Owner))
            {
                Principal groupOwner;

                try
                {
                    groupOwner = web.EnsureUser(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                catch
                {
                    groupOwner = web.SiteGroups.GetByName(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }

            ClientContext.ExecuteQueryRetry();
            WriteObject(group);
        }
    }
}
