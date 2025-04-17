using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteArchiveState")]
    public class SetSiteArchiveState : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public SPOSitePipeBind Identity;

        [Parameter(Mandatory = true, Position = 1)]
        public SPOArchiveState ArchiveState;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            SpoOperation spoOperation = null;
            switch (ArchiveState)
            {
                case SPOArchiveState.Archived:
                    {
                        WriteObject("The site and its contents cannot be accessed when a site is archived. The site needs to be reactivated if it needs to be accessed. Archived sites can be reactivated instantaneously, without any additional charges within 7 days of the action. After 7 days, reactivations will be charged as per Microsoft 365 Archive Billing, and will take time.");

                        if (Force || ShouldContinue(Identity.Url, Properties.Resources.Confirm))
                        {
                            spoOperation = Tenant.ArchiveSiteByUrl(Identity.Url);
                            AdminContext.Load(spoOperation);
                            AdminContext.ExecuteQueryRetry();
                        }

                        break;
                    }
                case SPOArchiveState.Active:
                    {
                        SiteProperties sitePropertiesByUrl = Tenant.GetSitePropertiesByUrl(Identity.Url, includeDetail: false);
                        AdminContext.Load(sitePropertiesByUrl);
                        AdminContext.ExecuteQueryRetry();

                        switch (sitePropertiesByUrl.ArchiveStatus)
                        {
                            case "FullyArchived":
                                {
                                    LogWarning("Reactivating a site from \"Archived\" state is a paid operation. It can take upto 24hrs for the site to be reactivated. Performing the operation \"Reactivate Archived site\" on target.");
                                    if (Force || ShouldContinue(Identity.Url, Properties.Resources.Confirm))
                                    {
                                        spoOperation = Tenant.UnarchiveSiteByUrl(Identity.Url);
                                        AdminContext.Load(spoOperation);
                                        AdminContext.ExecuteQueryRetry();
                                        WriteObject("Reactivation in progress. It may take upto 24hrs for reactivation to complete.");
                                    }
                                    break;
                                }
                            case "RecentlyArchived":
                                {
                                    string resourceString = "Reactivating a site from \"Recently Archived\" state is free. Site will be available as an Active site soon.";
                                    WriteObject(resourceString);
                                    if (Force || ShouldContinue(Identity.Url, Properties.Resources.Confirm))
                                    {
                                        spoOperation = Tenant.UnarchiveSiteByUrl(Identity.Url);
                                        AdminContext.Load(spoOperation);
                                        AdminContext.ExecuteQueryRetry();
                                    }
                                    break;
                                }
                            case "NotArchived":
                                WriteObject("The site is already in Active state and cannot be reactivated.");
                                return;
                            case "Reactivating":
                                WriteObject("The site is already reactivating and cannot be reactivated.");
                                return;
                        }
                        break;
                    }
                default:
                    throw new InvalidOperationException("OperationAborted");
            }
            if (!NoWait.ToBool())
            {
                PollOperation(spoOperation);
            }
        }
    }
}
