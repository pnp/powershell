using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365Group")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeOwners;

        [Parameter(Mandatory = false)]
        public SwitchParameter Detailed;

        [Parameter(Mandatory = false)]
        public string Filter;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSensitivityLabels;

        protected override void ExecuteCmdlet()
        {
            var includeSiteUrl = IncludeSiteUrl.ToBool();

            if (Identity != null)
            {
                var group = Identity.GetGroup(this, Connection, AccessToken, includeSiteUrl, IncludeOwners, Detailed.ToBool(), IncludeSensitivityLabels);
                WriteObject(group);
            }
            else
            {
                var groups = Microsoft365GroupsUtility.GetGroupsAsync(this, Connection, AccessToken, includeSiteUrl, IncludeOwners, Filter, IncludeSensitivityLabels).GetAwaiter().GetResult();

                WriteObject(groups.OrderBy(p => p.DisplayName), true);
            }
        }
    }
}