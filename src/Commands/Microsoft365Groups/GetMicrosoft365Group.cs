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
        [Obsolete("The site url is now excluded by default. Use IncludeSiteUrl instead to include the site url of the underlying SharePoint site.")]
        public SwitchParameter ExcludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeOwners;

        [Parameter(Mandatory = false)]
        [Obsolete("Classification is always included")]
        public SwitchParameter IncludeClassification;

        [Parameter(Mandatory = false)]
        [Obsolete("HasTeam is always included")]
        public SwitchParameter IncludeHasTeam;

        [Parameter(Mandatory = false)]        
        public string Filter;

        protected override void ExecuteCmdlet()
        {
#pragma warning disable 0618
            var includeSiteUrl = ParameterSpecified(nameof(ExcludeSiteUrl)) ? !ExcludeSiteUrl.ToBool() : IncludeSiteUrl.ToBool();
#pragma warning restore 0618

            if (Identity != null)
            {
                var group = Identity.GetGroup(Connection, AccessToken, includeSiteUrl, IncludeOwners);
                WriteObject(group);
            }
            else
            {
                var groups = Microsoft365GroupsUtility.GetGroupsAsync(Connection, AccessToken, includeSiteUrl, IncludeOwners, Filter).GetAwaiter().GetResult();

                WriteObject(groups.OrderBy(p => p.DisplayName), true);
            }
        }
    }
}