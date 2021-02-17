using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
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
        [Obsolete("ExcludeSiteUrl is now the default behaviour. Use IncludeSiteUrl instead to include the site url of the underlying SharePoint site.")]
        public SwitchParameter ExcludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeClassification;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeHasTeam;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;
            List<UnifiedGroupEntity> groups = null;
#pragma warning disable 0618
            var includeSiteUrl = ParameterSpecified(nameof(ExcludeSiteUrl)) ? !ExcludeSiteUrl.ToBool() : IncludeSiteUrl.ToBool();
#pragma warning restore 0618

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken, includeSite: includeSiteUrl);
            }
            else
            {
                groups = UnifiedGroupsUtility.GetUnifiedGroups(AccessToken, includeSite: IncludeSiteUrl, includeClassification: IncludeClassification.IsPresent, includeHasTeam: IncludeHasTeam.IsPresent);
            }

            if (group != null)
            {
                WriteObject(group);
            }
            else if (groups != null)
            {
                WriteObject(groups, true);
            }
        }
    }
}