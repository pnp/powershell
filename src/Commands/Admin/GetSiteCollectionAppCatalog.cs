using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

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

            var appCatalogsCsom = AdminContext.Web.TenantAppCatalog.SiteCollectionAppCatalogsSites;
            AdminContext.Load(appCatalogsCsom);
            AdminContext.ExecuteQueryRetry();

            var appCatalogsLocalModel = appCatalogsCsom.Select(ac =>
                new SiteCollectionAppCatalog
                {
                    AbsoluteUrl = ac.AbsoluteUrl,
                    ErrorMessage = ac.ErrorMessage,
                    SiteID = ac.SiteID
                }
            ).ToList();

            WriteVerbose($"{appCatalogsLocalModel.Count} site collection App Catalog{(appCatalogsLocalModel.Count != 1 ? "s have" : " has")} been retrieved");

            if (CurrentSite.ToBool())
            {
                ClientContext.Site.EnsureProperties(s => s.Id);

                WriteVerbose($"Filtering down to only the current site at {Connection.Url} with ID {ClientContext.Site.Id}");
                var currentSite = appCatalogsLocalModel.FirstOrDefault(a => a.SiteID.HasValue && a.SiteID.Value == ClientContext.Site.Id);

                appCatalogsLocalModel.Clear();

                if (currentSite == null)
                {
                    WriteVerbose($"Current site at {Connection.Url} with ID {ClientContext.Site.Id} does not have a site collection App Catalog on it");
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

                        // Deleted sites throw either an exception or return null
                        appCatalogLocalModel.AbsoluteUrl = Tenant.GetSitePropertiesById(appCatalogLocalModel.SiteID.Value, false).Url;
                        results.Add(appCatalogLocalModel);
                    }
                    catch (Exception e)
                    {
                        if (e is NullReferenceException || (e is ServerException se && se.ServerErrorTypeName.Equals("Microsoft.Online.SharePoint.Common.SpoNoSiteException", StringComparison.InvariantCultureIgnoreCase)))
                        {
                            if (!ExcludeDeletedSites.ToBool())
                            {
                                WriteVerbose($"Site collection App Catalog at {appCatalogLocalModel.AbsoluteUrl} regards a site that has been deleted");
                                results.Add(appCatalogLocalModel);
                            }
                            else
                            {
                                WriteVerbose($"Site collection App Catalog at {appCatalogLocalModel.AbsoluteUrl} regards a site that has been deleted. Since the {nameof(ExcludeDeletedSites)} flag has been provided, it will not be included in the results.");
                            }

                            continue;
                        }

                        throw;
                    }
                }
            }

            WriteObject(results, true);
        }
    }
}