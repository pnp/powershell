﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Add, "PnPGroupMember")]
    [OutputType(typeof(void))]
    public class AddGroupMember : PnPWebCmdlet
    {
        private const string ParameterSet_INTERNAL = "Internal";
        private const string ParameterSet_EXTERNAL = "External";
        private const string ParameterSet_BATCHED = "Batched";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_INTERNAL)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCHED)]
        public string LoginName;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_INTERNAL)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_EXTERNAL)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BATCHED)]
        [Alias("Identity")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_EXTERNAL)]
        public string EmailAddress;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_EXTERNAL)]
        public SwitchParameter SendEmail;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_EXTERNAL)]
        public string EmailBody = "Site shared with you.";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCHED)]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_EXTERNAL)
            {
                var group = Group.GetGroup(CurrentWeb);
                group.InviteExternalUser(EmailAddress, SendEmail, EmailBody);
            }
            else
            {
                var group = Group.GetGroup(PnPContext);
                var user = PnPContext.Web.EnsureUser(LoginName);

                if (ParameterSetName == ParameterSet_BATCHED)
                {
                    group.AddUserBatch(Batch.Batch, user.LoginName);
                }
                else
                {
                    group.AddUser(user.LoginName);
                }
            }
        }
    }
}
