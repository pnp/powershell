using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Set, "PnPSearchSettings")]
    public class SetSearchSettings : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SearchBoxInNavBarType? SearchBoxInNavBar;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string SearchPageUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string SearchBoxPlaceholderText;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SearchScopeType? SearchScope;

        [Parameter(Mandatory = false)]
        public SearchSettingsScope Scope = SearchSettingsScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (this.ClientContext.Url.ToLower().Contains("-my."))
            {
                throw new InvalidOperationException("This cmdlet does not work for OneDrive for Business sites");
            }

            bool hasSearchPageUrl = ParameterSpecified(nameof(SearchPageUrl));
            if (hasSearchPageUrl && SearchPageUrl == null)
            {
                SearchPageUrl = string.Empty;
            }

            bool hasSearchPlaceholderText = ParameterSpecified(nameof(SearchBoxPlaceholderText));
            if (hasSearchPlaceholderText && SearchBoxPlaceholderText == null)
            {
                SearchBoxPlaceholderText = string.Empty;
            }

            if (!Force && SearchBoxInNavBar.HasValue && SearchBoxInNavBar.Value == SearchBoxInNavBarType.Hidden)
            {
                var url = ClientContext.Url;
                var uri = new Uri(url);
                var uriParts = uri.Host.Split('.');

                var rootUrl = $"https://{string.Join(".", uriParts)}";

                bool shouldContinue;
                if (ClientContext.Url.TrimEnd('/').Equals(rootUrl.TrimEnd('/'), System.StringComparison.InvariantCultureIgnoreCase))
                {
                    shouldContinue = ShouldContinue($"If you hide the suite bar search box in the root site collection, take notice that this will also affect the SharePoint Home Site, as well as the current site ({url}), it's lists and it's libraries.\r\n\r\nOnly continue if you are aware of the implications.", Resources.Confirm);
                }
                else
                {
                    shouldContinue = ShouldContinue($"Hiding the search box will hide the search box from the current site ({url}), as well as it's lists and libraries.\r\n\r\nOnly continue if you are aware of the implications.", Resources.Confirm);
                }

                if (!shouldContinue)
                {
                    return;
                }
            }

            if (this.Scope == SearchSettingsScope.Site)
            {            
                ClientContext.Site.RootWeb.EnsureProperty(w => w.SearchScope);

                if (this.SearchBoxInNavBar.HasValue)
                {
                    ClientContext.Site.SearchBoxInNavBar = this.SearchBoxInNavBar.Value;
                }
                if (hasSearchPageUrl)
                {
                    ClientContext.Web.SetSiteCollectionSearchCenterUrl(SearchPageUrl, Connection.TenantAdminUrl);
                }
                if (hasSearchPlaceholderText)
                {
                    ClientContext.Site.SetSearchBoxPlaceholderText(SearchBoxPlaceholderText, Connection.TenantAdminUrl);
                }
                if (SearchScope.HasValue && ClientContext.Site.RootWeb.SearchScope != SearchScope.Value)
                {
                    ClientContext.Site.RootWeb.SearchScope = SearchScope.Value;
                    ClientContext.Site.RootWeb.Update();
                }
            }
            else
            {
                ClientContext.Web.EnsureProperty(w => w.SearchScope);
                if (this.SearchBoxInNavBar.HasValue)
                {
                    ClientContext.Web.SearchBoxInNavBar = this.SearchBoxInNavBar.Value;
                }
                if (hasSearchPageUrl)
                {
                    ClientContext.Web.SetWebSearchCenterUrl(SearchPageUrl, Connection.TenantAdminUrl);
                }
                if (hasSearchPlaceholderText)
                {
                    ClientContext.Web.SetSearchBoxPlaceholderText(SearchBoxPlaceholderText, Connection.TenantAdminUrl);
                }
                if (SearchScope.HasValue && ClientContext.Web.SearchScope != SearchScope.Value)
                {
                    ClientContext.Web.SearchScope = SearchScope.Value;
                }
                ClientContext.Web.Update();
            }

            ClientContext.ExecuteQueryRetry();
        }
    }
}