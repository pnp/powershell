using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities;
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
        [Alias("GroupLogoPath")]
        public string LogoPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter CreateTeam;

        [Parameter(Mandatory = false)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false)]
        public bool? HideFromOutlookClients;

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
                var candidate = Microsoft365GroupsUtility.GetGroupAsync(HttpClient, MailNickname, AccessToken, false).GetAwaiter().GetResult();
                forceCreation = candidate == null || ShouldContinue($"The Microsoft 365 Group '{MailNickname} already exists. Do you want to create a new one?", Resources.Confirm);
            }
            else
            {
                forceCreation = true;
            }

            if (forceCreation)
            {
                if (ParameterSpecified(nameof(LogoPath)))
                {
                    if (System.IO.Path.IsPathRooted(LogoPath))
                    {
                        LogoPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LogoPath);
                    }
                    if (!System.IO.File.Exists(LogoPath))
                    {
                        throw new PSArgumentException("File specified for logo does not exist.");
                    }
                }
                var newGroup = new Microsoft365Group()
                {
                    DisplayName = DisplayName,
                    Description = Description,
                    MailNickname = MailNickname,
                    Visibility = IsPrivate ? "Private" : "Public",
                    MailEnabled = true,
                    SecurityEnabled = false,
                    GroupTypes = new string[] { "Unified" }
                };
                var group = Microsoft365GroupsUtility.CreateAsync(HttpClient, AccessToken, newGroup, CreateTeam, LogoPath, Owners, Members, HideFromAddressLists, HideFromOutlookClients).GetAwaiter().GetResult();

                if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                {
                    Microsoft365GroupsUtility.SetVisibilityAsync(HttpClient, AccessToken, group.Id.Value, HideFromAddressLists, HideFromOutlookClients).GetAwaiter().GetResult();
                }

                WriteObject(group);
            }
        }
    }
}