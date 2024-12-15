using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "PnPAzureADGroup")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Set-PnPEntraIDGroup")]
    public class SetAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

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
                group = Identity.GetGroup(RequestHelper);
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
                        AzureADGroupsUtility.Update(RequestHelper, group);
                    }

                    if (ParameterSpecified(nameof(Owners)))
                    {
                        ClearOwners.UpdateOwners(RequestHelper, new Guid(group.Id), Owners);
                    }
                    if (ParameterSpecified(nameof(Members)))
                    {
                        ClearOwners.UpdateMembersAsync(RequestHelper, new Guid(group.Id), Members);
                    }

                    if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                    {
                        // For this scenario a separate call needs to be made
                        Utilities.ClearOwners.SetVisibility(RequestHelper, new Guid(group.Id), HideFromAddressLists, HideFromOutlookClients);
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