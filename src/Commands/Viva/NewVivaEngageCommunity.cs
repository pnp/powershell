using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.VivaEngage;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.New, "PnPVivaEngageCommunity")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Community.ReadWrite.All")]
    public class NewVivaEngageCommunity : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateLength(1, 255)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        [ValidateLength(1, 1024)]
        public string Description;

        [Parameter(Mandatory = false)]
        public CommunityPrivacy Privacy = CommunityPrivacy.Private;

        [Parameter(Mandatory = false)]
        public string[] Owners;
        protected override void ExecuteCmdlet()
        {
            var postData = new Dictionary<string, object>() {
                    { "description" , string.IsNullOrEmpty(Description) ? null : Description },
                    { "displayName" , DisplayName },
                    {"privacy", Privacy.ToString().ToLower() }
                };

            if (Owners?.Length > 0)
            {
                string[] ownerData = Microsoft365GroupsUtility.GetUsersDataBindValue(RequestHelper, Owners);
                postData.Add("owners@odata.bind", ownerData);
            }

            var data = JsonSerializer.Serialize(postData);
            var stringContent = new StringContent(data);
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var httpResponseMessage = RequestHelper.PostHttpContent("/v1.0/employeeExperience/communities", stringContent);

            var opLocationResponseHeader = httpResponseMessage.Headers.Location;

            VivaEngageProvisioningResult provisioningResult;
            int retryCount = 0;
            do
            {
                provisioningResult = RequestHelper.Get<VivaEngageProvisioningResult>(opLocationResponseHeader.AbsoluteUri);
                if (provisioningResult.Status != "succeeded")
                {
                    // Wait for 5 seconds before retrying
                    System.Threading.Thread.Sleep(5000);
                    retryCount++;
                }
            } while (provisioningResult.Status != "succeeded" && retryCount < 5);

            if (provisioningResult.Status != "succeeded")
            {
                throw new Exception("Provisioning failed after 5 attempts.");
            }

            WriteObject(provisioningResult);
        }
    }
}
