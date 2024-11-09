using Microsoft.SharePoint.Client;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteVersionPolicy")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [OutputType(typeof(SiteVersionPolicy))]
    public class GetSiteVersionPolicy : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Site, s => s.Url, s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit);
            ClientContext.ExecuteQueryRetry();
            var site = ClientContext.Site;

            var vp = new SiteVersionPolicy();
            vp.Url = site.Url;

            if (site.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit == -1)
            {
                vp.Description = "No Site Level Policy Set for new document libraries";
            }
            else
            {
                site.EnsureProperties(s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit, s => s.VersionPolicyForNewLibrariesTemplate.VersionPolicies);

                vp.DefaultTrimMode = site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode.ToString();

                if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                {
                    vp.Description = "Site has Automatic Policy Set for new document libraries";
                }
                else
                {
                    if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.ExpireAfter)
                    {
                        vp.DefaultExpireAfterDays = site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultExpireAfterDays.ToString();
                        vp.Description = "Site has Manual settings with specific count and time limits for new document libraries";
                    }
                    else if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.NoExpiration)
                    {
                        vp.Description = "Site has Manual settings with specific version count limit and no time limits for new document libraries";
                    }
                    vp.MajorVersionLimit = site.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit.ToString();
                }
            }

            WriteObject(vp);
        }
    }
}
