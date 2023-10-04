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

                var s = new PSObject();
                s.Properties.Add(new PSVariableProperty(new PSVariable("Url", site.Url)));

                if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.ServerObjectIsNull == true)
                {
                    s.Properties.Add(new PSVariableProperty(new PSVariable("VersionPolicy", "No Site Level Policy Set")));
                }
                else
                {
                    site.EnsureProperties(s => s.VersionPolicyForNewLibrariesTemplate, s => s.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit, s => s.VersionPolicyForNewLibrariesTemplate.VersionPolicies);

                    s.Properties.Add(new PSVariableProperty(new PSVariable("DefaultTrimMode", site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode)));

                    if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode != VersionPolicyTrimMode.AutoExpiration)
                    {
                        if (site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.ExpireAfter)
                        {
                            s.Properties.Add(new PSVariableProperty(new PSVariable("DefaultExpireAfterDays", site.VersionPolicyForNewLibrariesTemplate.VersionPolicies.DefaultExpireAfterDays)));
                        }
                        s.Properties.Add(new PSVariableProperty(new PSVariable("MajorVersionLimit", site.VersionPolicyForNewLibrariesTemplate.MajorVersionLimit)));
                    }
                }

                WriteObject(s);
            }
            else
            {
                ClientContext.Load(site, RetrievalExpressions);
                ClientContext.ExecuteQueryRetry();
                WriteObject(site);
            }
        }
    }
}
