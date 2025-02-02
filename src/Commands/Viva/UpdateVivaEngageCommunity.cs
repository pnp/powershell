using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.VivaEngage;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Set, "PnPVivaEngageCommunity")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Community.ReadWrite.All")]
    public class UpdateVivaEngageCommunity : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public CommunityPrivacy Privacy;
        protected override void ExecuteCmdlet()
        {
            string endpointUrl = $"/v1.0/employeeExperience/communities/{Identity}";

            var postData = new Dictionary<string, string>();

            if (ParameterSpecified(nameof(DisplayName)))
            {
                postData.Add("displayName", DisplayName);
            }
            if (ParameterSpecified(nameof(Description)))
            {
                postData.Add("description", Description);
            }
            if (ParameterSpecified(nameof(Privacy)))
            {
                postData.Add("privacy", Privacy.ToString().ToLower());
            }
            GraphRequestHelper.Patch(endpointUrl, postData);
        }
    }
}
