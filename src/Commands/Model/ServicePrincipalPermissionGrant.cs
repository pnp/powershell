﻿using Microsoft.Online.SharePoint.TenantAdministration.Internal;

namespace PnP.PowerShell.Commands.Model
{
    public class TenantServicePrincipalPermissionGrant
    {
        public string ClientId { get; set; }
        public string ConsentType { get; set; }
        public string ObjectId { get; set; }
        public string Resource { get; set; }
        public string ResourceId { get; set; }
        public string Scope { get; set; }

        public TenantServicePrincipalPermissionGrant(SPOWebAppServicePrincipalPermissionGrant grant)
        {
            ClientId = grant.ClientId;
            ConsentType = grant.ConsentType;
            ObjectId = grant.ObjectId;
            Resource = grant.Resource;
            ResourceId = grant.ResourceId;
            Scope = grant.Scope;
        }
    }
}