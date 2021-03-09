using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.New, "PnPAzureADGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class NewAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public String DisplayName;

        [Parameter(Mandatory = true)]
        public String Description;

        [Parameter(Mandatory = true)]
        public String MailNickname;

        [Parameter(Mandatory = false)]
        public String[] Owners;

        [Parameter(Mandatory = false)]
        public String[] Members;

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
                var existingGroup = GroupsUtility.GetGroups(AccessToken,
                    mailNickname: MailNickname,
                    endIndex: 1).Any();

                forceCreation = !existingGroup || ShouldContinue(string.Format(Resources.ForceCreationOfExistingGroup0, MailNickname), Resources.Confirm);
            }
            else
            {
                forceCreation = true;
            }

            if (forceCreation)
            {
                var group = GroupsUtility.CreateGroup(
                    displayName: DisplayName,
                    description: Description,
                    mailNickname: MailNickname,
                    accessToken: AccessToken,
                    owners: Owners,
                    members: Members,
                    securityEnabled: IsSecurityEnabled,
                    mailEnabled: IsMailEnabled);

                WriteObject(group);
            }
        }
    }
}