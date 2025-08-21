using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInfo")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/CrossTenantInformation.ReadBasic.All")]
    [OutputType(typeof(Model.TenantInfo))]
    public class GetTenantInfo : PnPSharePointOnlineAdminCmdlet
    {
        private const string GETINFOBYTDOMAINNAME = "By Domain Name";
        private const string GETINFOBYTENANTID = "By TenantId";
        private const string GETINFOOFCURRENTTENANT = "Current Tenant";

        [Parameter(Mandatory = false, ParameterSetName = GETINFOOFCURRENTTENANT)]
        public SwitchParameter CurrentTenant;

        [Parameter(Mandatory = true, ParameterSetName = GETINFOBYTDOMAINNAME)]
        public string DomainName;

        [Parameter(Mandatory = true, ParameterSetName = GETINFOBYTENANTID)]
        public string TenantId;

        protected override void ExecuteCmdlet()
        {
            if ((ParameterSetName == GETINFOBYTDOMAINNAME && TenantId != null) || (ParameterSetName == GETINFOBYTENANTID && DomainName != null))
            {
                throw new PSArgumentException("Specify either DomainName or TenantId, but not both.");
            }

            LogDebug("Acquiring access token for Microsoft Graph to look up Tenant");
            //var graphAccessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
            var requestUrl = BuildRequestUrl();

            LogDebug($"Making call to {requestUrl} to request tenant information");
            var results = GraphRequestHelper.Get<Model.TenantInfo>(requestUrl);
            WriteObject(results, true);
        }

        private string BuildRequestUrl()
        {
            var baseUrl = $"/v1.0/tenantRelationships/";
            var query = string.Empty;
            switch (ParameterSetName)
            {
                case GETINFOBYTDOMAINNAME:
                    query = $"microsoft.graph.findTenantInformationByDomainName(domainName='{DomainName}')";
                    break;
                case GETINFOBYTENANTID:
                    query = $"microsoft.graph.findTenantInformationByTenantId(tenantId='{TenantId}')";
                    break;
                case GETINFOOFCURRENTTENANT:
                    if (Connection != null)
                    {
                        string tenantId = TenantExtensions.GetTenantIdByUrl(Connection.Url, Connection.AzureEnvironment).ToString();
                        query = $"microsoft.graph.findTenantInformationByTenantId(tenantId='{tenantId}')";
                    }
                    else
                    {
                        throw new InvalidOperationException($"You are not signed in. Please use Connect-PnPOnline to connect.");
                    }
                    break;
            }
            return baseUrl + query;
        }
    }
}
