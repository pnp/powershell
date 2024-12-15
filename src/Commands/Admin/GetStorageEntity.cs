using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using System.Text.Json;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPStorageEntity")]
    public class GetPnPStorageEntity : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Key;

        [Parameter(Mandatory = false)]
        public StorageEntityScope Scope = StorageEntityScope.Tenant;

        protected override void ExecuteCmdlet()
        {
            string storageEntitiesIndex = string.Empty;

            if (Scope == StorageEntityScope.Site)
            {
                var appcatalog = ClientContext.Site.RootWeb.SiteCollectionAppCatalog;
                ClientContext.Load(appcatalog);
                ClientContext.ExecuteQueryRetry();
                if (appcatalog.ServerObjectIsNull == false)
                {
                    if (ParameterSpecified(nameof(Key)))
                    {
                        var storageEntity = ClientContext.Site.RootWeb.GetStorageEntity(Key);
                        ClientContext.Load(storageEntity);
                        ClientContext.ExecuteQueryRetry();

                        var storageEntityValue = new StorageEntity
                        {
                            Key = Key,
                            Value = storageEntity.Value,
                            Comment = storageEntity.Comment,
                            Description = storageEntity.Description
                        };
                        WriteObject(storageEntityValue);
                    }
                    else
                    {
                        storageEntitiesIndex = ClientContext.Site.RootWeb.GetPropertyBagValueString("storageentitiesindex", "");
                    }
                }
                else
                {
                    WriteWarning("Site Collection App Catalog is not available on this site.");
                }
            }
            else
            {
                var appCatalogUri = ClientContext.Web.GetAppCatalog();
                if (appCatalogUri != null)
                {
                    using (var clonedContext = ClientContext.Clone(appCatalogUri))
                    {
                        if (ParameterSpecified(nameof(Key)))
                        {
                            var storageEntity = clonedContext.Site.RootWeb.GetStorageEntity(Key);
                            clonedContext.Load(storageEntity);
                            clonedContext.ExecuteQueryRetry();

                            var storageEntityValue = new StorageEntity
                            {
                                Key = Key,
                                Value = storageEntity.Value,
                                Comment = storageEntity.Comment,
                                Description = storageEntity.Description
                            };

                            WriteObject(storageEntityValue);
                        }
                        else
                        {
                            storageEntitiesIndex = clonedContext.Web.GetPropertyBagValueString("storageentitiesindex", "");
                        }
                    }
                }
                else
                {
                    WriteWarning("Tenant app catalog is not available on this tenant.");
                }
            }

            if (!string.IsNullOrEmpty(storageEntitiesIndex))
            {
                var storageEntitiesDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(storageEntitiesIndex);

                var storageEntities = new List<StorageEntity>();
                foreach (var key in storageEntitiesDict.Keys)
                {
                    var storageEntity = new StorageEntity
                    {
                        Key = key,
                        Value = storageEntitiesDict[key]["Value"],
                        Comment = storageEntitiesDict[key]["Comment"],
                        Description = storageEntitiesDict[key]["Description"]
                    };
                    storageEntities.Add(storageEntity);
                }
                WriteObject(storageEntities, true);
            }
        }
    }

    public class StorageEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
    }
}