﻿using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInfo")]
    [OutputType(typeof(Model.TenantInfo))]
    public class GetTenantInfo : PnPAdminCmdlet
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

            WriteVerbose("Acquiring access token for Microsoft Graph to look up Tenant");
            var graphAccessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
            var requestUrl = BuildRequestUrl();

            WriteVerbose($"Making call to {requestUrl} to request tenant information");
            var results = RestHelper.GetAsync<Model.TenantInfo>(Connection.HttpClient, requestUrl, graphAccessToken).GetAwaiter().GetResult();
            WriteObject(results, true);
        }

        private string BuildRequestUrl()
        {
            var baseUrl = $"https://{Connection.GraphEndPoint}/v1.0/tenantRelationships/";
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
                        throw new InvalidOperationException($"The current connection holds no SharePoint context. Please use one of the Connect-PnPOnline commands which uses the -Url argument to connect.");
                    }
                    break;
            }
            return baseUrl + query;
        }
    }
}
