﻿using Microsoft.SharePoint.Client;

using System;
using System.Linq.Expressions;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteVersionPolicy")]
    [OutputType(typeof(PnP.PowerShell.Commands.Model.SharePoint.SiteVersionPolicy))]
    public class GetSiteVersionPolicy : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Site, s => s.Url, s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.VersionPolicies);
            ClientContext.ExecuteQueryRetry();
            var site = ClientContext.Site;

            var vp = new SiteVersionPolicy();
            vp.Url = site.Url;

            if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.ServerObjectIsNull == true)
            {
                vp.Description = "No Site Level Policy Set";
            }
            else
            {
                site.EnsureProperties(s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit, s => s.VersionPolicyForNewLibrariesTemplate.VersionPolicies);

                vp.DefaultTrimMode = site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode.ToString();

                if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                {
                    vp.Description = "Site has Automatic Policy Set";
                }
                else
                {
                    if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.ExpireAfter)
                    {
                        vp.DefaultExpireAfterDays = site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultExpireAfterDays.ToString();
                        vp.Description = "Site has Manual settings with specific count and time limits";
                    }
                    else if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.NoExpiration)
                    {
                        vp.Description = "Site has Manual settings with specific version count limit and no time limits";
                    }
                    vp.MajorVersionLimit = site.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit.ToString();
                }
            }

            WriteObject(vp);
        }
    }
}
