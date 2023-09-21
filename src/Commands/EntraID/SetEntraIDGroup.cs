using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Set, "PnPEntraIDGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    [Alias("Set-PnPAzureADGroup")]
    public class SetEntraIDGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EntraIDGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string[] Owners;

        [Parameter(Mandatory = false)]
        public string[] Members;

        [Parameter(Mandatory = false)]
        public bool? SecurityEnabled;

        [Parameter(Mandatory = false)]
        public bool? MailEnabled;

        [Parameter(Mandatory = false)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false)]
        public bool? HideFromOutlookClients;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(Connection, AccessToken);
            }

            if (group != null)
            {
                try
                {
                    bool changed = false;
                    if (ParameterSpecified(nameof(DisplayName)))
                    {
                        group.DisplayName = DisplayName;
                        changed = true;
                    }
                    if (ParameterSpecified(nameof(Description)))
                    {
                        group.Description = Description;
                        changed = true;
                    }
                    if (ParameterSpecified(nameof(SecurityEnabled)) && SecurityEnabled.HasValue)
                    {
                        group.SecurityEnabled = SecurityEnabled.Value;
                        changed = true;
                    }
                    if (ParameterSpecified(nameof(MailEnabled)) && MailEnabled.HasValue)
                    {
                        group.MailEnabled = MailEnabled.Value;
                        changed = true;
                    }

                    if (changed)
                    {
                        EntraIDGroupsUtility.UpdateAsync(Connection, AccessToken, group).GetAwaiter().GetResult();
                    }

                    if (ParameterSpecified(nameof(Owners)))
                    {
                        Microsoft365GroupsUtility.UpdateOwnersAsync(Connection, new Guid(group.Id), AccessToken, Owners).GetAwaiter().GetResult();
                    }
                    if (ParameterSpecified(nameof(Members)))
                    {
                        Microsoft365GroupsUtility.UpdateMembersAsync(Connection, new Guid(group.Id), AccessToken, Members).GetAwaiter().GetResult();
                    }

                    if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                    {
                        // For this scenario a separate call needs to be made
                        Utilities.Microsoft365GroupsUtility.SetVisibilityAsync(Connection, AccessToken, new Guid(group.Id), HideFromAddressLists, HideFromOutlookClients).GetAwaiter().GetResult();
                    }
                }
                catch (Exception e)
                {
                    while (e.InnerException != null) e = e.InnerException;
                    WriteError(new ErrorRecord(e, "GROUPUPDATEFAILED", ErrorCategory.InvalidOperation, this));
                }
            }
            else
            {
                WriteError(new ErrorRecord(new Exception("Group not found"), "GROUPNOTFOUND", ErrorCategory.ObjectNotFound, this));
            }
        }
    }
}