﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPStorageEntity")]
    public class SetPnPStorageEntity : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Key;

        [Parameter(Mandatory = true)]
        public string Value;

        [Parameter(Mandatory = false)]
        [AllowNull]
        public string Comment;

        [Parameter(Mandatory = false)]
        [AllowNull]
        public string Description;

        [Parameter(Mandatory = false)]
        public StorageEntityScope Scope = StorageEntityScope.Tenant;

        protected override void ExecuteCmdlet()
        {
            if (Scope == StorageEntityScope.Tenant)
            {
                var appCatalogUri = ClientContext.Web.GetAppCatalog();
                if(appCatalogUri != null)
                {
                    using (var clonedContext = ClientContext.Clone(appCatalogUri))
                    {
                        clonedContext.Web.SetStorageEntity(Key, Value, Description, Comment);
                        clonedContext.ExecuteQueryRetry();
                    }
                }
                else
                {
                    WriteWarning("Tenant app catalog is not available on this tenant.");
                }
            }
            else
            {
                var appcatalog = ClientContext.Site.RootWeb.SiteCollectionAppCatalog;
                ClientContext.Load(appcatalog);
                ClientContext.ExecuteQueryRetry();
                if (appcatalog.ServerObjectIsNull == false)
                {
                    ClientContext.Site.RootWeb.SetStorageEntity(Key, Value, Description, Comment);
                    ClientContext.ExecuteQueryRetry();
                }
                else
                {
                    WriteWarning("Site Collection App Catalog is not available on this site.");
                }
            }
        }
    }
}