﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSearchSettings")]
    public class GetSearchSettings : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Web.EnsureProperties(w => w.SearchScope, w => w.SearchBoxInNavBar, w => w.SearchBoxPlaceholderText);
            ClientContext.Site.EnsureProperties(s => s.SearchBoxInNavBar, s => s.SearchBoxPlaceholderText);

            string siteUrl = ClientContext.Web.GetSiteCollectionSearchCenterUrl();
            string webUrl = ClientContext.Web.GetWebSearchCenterUrl(urlOnly: true);

            PSObject res = new PSObject();
            res.Properties.Add(new PSNoteProperty("Classic Search Center URL", siteUrl));
            res.Properties.Add(new PSNoteProperty("Redirect search URL", webUrl));
            res.Properties.Add(new PSNoteProperty("Site Search Scope", ClientContext.Web.SearchScope));
            res.Properties.Add(new PSNoteProperty("Site collection search box visibility", ClientContext.Site.SearchBoxInNavBar));
            res.Properties.Add(new PSNoteProperty("Site search box visibility", ClientContext.Web.SearchBoxInNavBar));
            res.Properties.Add(new PSNoteProperty("Site collection search box placeholder text", ClientContext.Site.SearchBoxPlaceholderText));
            res.Properties.Add(new PSNoteProperty("Site search box placeholder text", ClientContext.Web.SearchBoxPlaceholderText));
            WriteObject(res);
        }
    }
}