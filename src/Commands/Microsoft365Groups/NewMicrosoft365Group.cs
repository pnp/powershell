using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.New, "PnPMicrosoft365Group")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class NewPnPMicrosoft365Group : PnPGraphCmdlet
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
        public SwitchParameter IsPrivate;

        [Parameter(Mandatory = false)]
        public string GroupLogoPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter CreateTeam;

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
                var candidates = UnifiedGroupsUtility.GetUnifiedGroups(AccessToken,
                    mailNickname: MailNickname,
                    endIndex: 1, azureEnvironment: PnPConnection.Current.AzureEnvironment);
                // ListUnifiedGroups retrieves groups with starts-with, so need another check
                var existingGroup = candidates.Any(g => g.MailNickname.Equals(MailNickname, StringComparison.CurrentCultureIgnoreCase));

                forceCreation = !existingGroup || ShouldContinue(string.Format(Resources.ForceCreationOfExistingGroup0, MailNickname), Resources.Confirm);
            }
            else
            {
                forceCreation = true;
            }

            if (forceCreation)
            {
                var group = UnifiedGroupsUtility.CreateUnifiedGroup(
                    displayName: DisplayName,
                    description: Description,
                    mailNickname: MailNickname,
                    accessToken: AccessToken,
                    owners: Owners,
                    members: Members,
                    groupLogoPath: GroupLogoPath,
                    isPrivate: IsPrivate,
                    createTeam: CreateTeam, azureEnvironment: PnPConnection.Current.AzureEnvironment);

                WriteObject(group);
            }
        }
    }
}