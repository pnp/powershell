﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.New, "PnPAzureADGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    [Alias("New-PnPEntraIDGroup")]
    public class NewAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = true)]
        public string Description;

        [Parameter(Mandatory = true)]
        public string MailNickname;

        [Parameter(Mandatory = false)]
        public string[] Owners;

        [Parameter(Mandatory = false)]
        public string[] Members;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsSecurityEnabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsMailEnabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (MailNickname.Contains(" "))
            {
                throw new ArgumentException("MailNickname cannot contain spaces.");
            }
            bool forceCreation;

            if (!Force)
            {
                var existingGroup = AzureADGroupsUtility.GetGroupAsync(Connection, MailNickname, AccessToken).GetAwaiter().GetResult();

                forceCreation = existingGroup == null || ShouldContinue(string.Format(Resources.ForceCreationOfExistingGroup0, MailNickname), Resources.Confirm);
            }
            else
            {
                forceCreation = true;
            }

            if (forceCreation)
            {
                string[] ownerData = null;
                string[] memberData = null;

                var postData = new Dictionary<string, object>() {
                    { "description" , string.IsNullOrEmpty(Description) ? null : Description },
                    { "displayName" , DisplayName },
                    { "groupTypes", new List<string>(){} },
                    { "mailEnabled", IsMailEnabled.ToBool() },
                    { "mailNickname" , MailNickname },
                    { "securityEnabled", IsSecurityEnabled.ToBool() }                    
                };

                if (Owners?.Length > 0)
                {
                    ownerData = Microsoft365GroupsUtility.GetUsersDataBindValueAsync(Connection, AccessToken, Owners).GetAwaiter().GetResult();
                    postData.Add("owners@odata.bind", ownerData);
                }
                if (Members?.Length > 0)
                {
                    memberData = Microsoft365GroupsUtility.GetUsersDataBindValueAsync(Connection, AccessToken, Members).GetAwaiter().GetResult();
                    postData.Add("members@odata.bind", memberData);
                }               

                var data = JsonSerializer.Serialize(postData);
                var stringContent = new StringContent(data);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var groupResult = GraphHelper.PostAsync<Group>(Connection, $"v1.0/groups", stringContent, AccessToken).GetAwaiter().GetResult();

                WriteObject(groupResult);
            }
        }
    }
}