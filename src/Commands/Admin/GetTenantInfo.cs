using PnP.Core.Transformation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInfo")]
    public class GetTenantInfo : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public string DomainName;

        [Parameter(Mandatory = false)]
        public string TenantId;

        protected override void ExecuteCmdlet()
        {
            EnsureValidInput();

            WriteVerbose("Acquiring access token for Microsoft Graph to look up Tenant");
            var graphAccessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
            var requestUrl = BuildRequestUrl();

            var results = RestHelper.GetAsync<Model.TenantInfo>(Connection.HttpClient, requestUrl, graphAccessToken).GetAwaiter().GetResult();
            WriteObject(results, true);
        }

        private void EnsureValidInput()
        {
            if (string.IsNullOrEmpty(TenantId) && string.IsNullOrEmpty(DomainName))
            {
                throw new PSArgumentException("Specify atleast one, either DomainName or TenantId, but not both");
            }

            if (TenantId != null && DomainName != null)
            {
                throw new PSArgumentException("Please specify either DomainName or TenantId, but not both");
            }
        }

        private string BuildRequestUrl()
        {
            var baseUrl = $"https://{Connection.GraphEndPoint}/v1.0/tenantRelationships/";
            var query = TenantId != null
                ? $"microsoft.graph.findTenantInformationByTenantId(tenantId='{TenantId}')"
                : $"microsoft.graph.findTenantInformationByDomainName(domainName='{DomainName}')";

            return baseUrl + query;
        }
    }
}
