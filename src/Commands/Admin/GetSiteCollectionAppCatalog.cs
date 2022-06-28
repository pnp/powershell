using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteCollectionAppCatalog")]
    [Alias("Get-PnPSiteCollectionAppCatalogs")]
    [WriteAliasWarning("Please use 'Get-PnPSiteCollectionAppCatalog' (singular). The alias 'Get-PnPSiteCollectionAppCatalogs' (plural) will be removed in a future release.")]
    [OutputType(typeof(IEnumerable<SiteCollectionAppCatalog>))]
    public class GetSiteCollectionAppCatalog : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter ExcludeDeletedSites;     

        [Parameter(Mandatory = false)]
        public SwitchParameter CurrentSite;  

        protected override void ExecuteCmdlet()
        {
            WriteVerbose("Retrieving all site collection App Catalogs from SharePoint Online");

            var appCatalogsCsom = ClientContext.Web.TenantAppCatalog.SiteCollectionAppCatalogsSites;
            ClientContext.Load(appCatalogsCsom);
            ClientContext.ExecuteQueryRetry();            

            var appCatalogsLocalModel = appCatalogsCsom.Select(ac =>
                new SiteCollectionAppCatalog {
                    AbsoluteUrl = ac.AbsoluteUrl,
                    ErrorMessage = ac.ErrorMessage,
                    SiteID = ac.SiteID
                }
            ).ToList();

            WriteVerbose($"{appCatalogsLocalModel.Count} site collection App Catalog{(appCatalogsLocalModel.Count != 1 ? "s have" : " has")} been retrieved");

            if(CurrentSite.ToBool())
            {
                SiteContext.Site.EnsureProperties(s => s.Id);

                WriteVerbose($"Filtering down to only the current site at {Connection.Url} with ID {SiteContext.Site.Id}");
                var currentSite = appCatalogsLocalModel.FirstOrDefault(a => a.SiteID.HasValue && a.SiteID.Value == SiteContext.Site.Id);

                appCatalogsLocalModel.Clear();

                if(currentSite == null)
                {
                    WriteVerbose($"Current site at {Connection.Url} with ID {SiteContext.Site.Id} does not have a site collection App Catalog on it");
                    return;
                }

                appCatalogsLocalModel.Add(currentSite);
            }

            var results = new List<SiteCollectionAppCatalog>(appCatalogsLocalModel.Count);
            foreach (var appCatalogLocalModel in appCatalogsLocalModel)
            {
                if (appCatalogLocalModel.SiteID.HasValue)
                {
                    try
                    {
                        WriteVerbose($"Validating site collection App Catalog at {appCatalogLocalModel.AbsoluteUrl}");

                        appCatalogLocalModel.AbsoluteUrl = Tenant.GetSitePropertiesById(appCatalogLocalModel.SiteID.Value, false).Url;
                        results.Add(appCatalogLocalModel);
                    }
                    catch (Microsoft.SharePoint.Client.ServerException e) when (e.ServerErrorTypeName.Equals("Microsoft.Online.SharePoint.Common.SpoNoSiteException", StringComparison.InvariantCultureIgnoreCase))
                    {                        
                        if(!ExcludeDeletedSites.ToBool())
                        {
                            WriteVerbose($"Site collection App Catalog at {appCatalogLocalModel.AbsoluteUrl} regards a site that has been deleted");
                            results.Add(appCatalogLocalModel);
                        }
                        else
                        {
                            WriteVerbose($"Site collection App Catalog at {appCatalogLocalModel.AbsoluteUrl} regards a site that has been deleted. Since the {nameof(ExcludeDeletedSites)} flag has been provided, it will not be included in the results.");
                        }
                    }                    
                }
            }

            WriteObject(results, true);
        }
    }
}