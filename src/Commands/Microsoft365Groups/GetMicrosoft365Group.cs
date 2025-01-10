using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365Group")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/GroupMember.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]  
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
                var group = Identity.GetGroup(RequestHelper, includeSiteUrl, IncludeOwners, Detailed.ToBool(), IncludeSensitivityLabels);
                WriteObject(group);
            }
            else
            {
                var groups = Microsoft365GroupsUtility.GetGroups(RequestHelper, includeSiteUrl, IncludeOwners, Filter, IncludeSensitivityLabels);

                WriteObject(groups.OrderBy(p => p.DisplayName), true);
            }
        }
    }
}