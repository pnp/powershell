using Microsoft.SharePoint.Client;

using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSite")]
    [OutputType(typeof(Microsoft.SharePoint.Client.Site))]
    public class GetSite : PnPRetrievalsCmdlet<Microsoft.SharePoint.Client.Site>
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter GetVersionPolicy;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Microsoft.SharePoint.Client.Site, object>>[] { s => s.Url, s => s.CompatibilityLevel };
            var site = ClientContext.Site;

            if (ParameterSpecified(nameof(GetVersionPolicy)))
            {
                site.EnsureProperties(s => s.Url, s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.VersionPolicies);

                var vp = new VersionPolicy();
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
            else
            {
                ClientContext.Load(site, RetrievalExpressions);
                ClientContext.ExecuteQueryRetry();
                WriteObject(site);
            }
        }
    }

    public class VersionPolicy
    {
        public string Url { get; set; }
        public string DefaultTrimMode { get; set; }
        public string DefaultExpireAfterDays { get; set; }
        public string MajorVersionLimit { get; set; }
        public string Description { get; set; }
    }
}
