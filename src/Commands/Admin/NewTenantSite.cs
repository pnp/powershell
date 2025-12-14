using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSite")]
    public class NewTenantSite : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSetName_Wait = "By Wait";

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Title;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Url;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Owner = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public uint Lcid = 1033;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Template = "STS#0";

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int TimeZone;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public double ResourceQuota = 0;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public double ResourceQuotaWarningLevel = 0;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public long StorageQuota = 100;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public long StorageQuotaWarningLevel = 100;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter RemoveDeletedSite;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_Wait)]
        public SwitchParameter Wait;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_Wait)]
        public SharingCapabilities? SharingCapability;

        protected override void ExecuteCmdlet()
        {
            if (!Url.ToLower().StartsWith("https://") && !Url.ToLower().StartsWith("http://"))
            {
                Uri uri = BaseUri;
                Url = $"{uri.ToString().TrimEnd('/')}/{Url.TrimStart('/')}";
            }

            if (ParameterSpecified(nameof(RemoveDeletedSite)))
            {
                Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

                Guid newSiteId = Tenant.CreateSiteCollection(Url, Title, Owner, Template, (int)StorageQuota,
                    (int)StorageQuotaWarningLevel, TimeZone, (int)ResourceQuota, (int)ResourceQuotaWarningLevel, Lcid,
                    RemoveDeletedSite, Wait, Wait == true ? timeoutFunction : null);

                if (newSiteId != Guid.Empty && Wait && SharingCapability.HasValue)
                {
                    SetSharingCapability(Url, SharingCapability.Value);
                }
            }
            else
            {
                SiteCreationProperties siteCreationProperties = new SiteCreationProperties
                {
                    Url = Url,
                    Owner = Owner,
                    Title = Title,
                    Template = Template,
                    StorageMaximumLevel = StorageQuota,
                    StorageWarningLevel = StorageQuotaWarningLevel,
                    TimeZoneId = TimeZone,
                    UserCodeMaximumLevel = ResourceQuota,
                    UserCodeWarningLevel = ResourceQuotaWarningLevel,
                    Lcid = Lcid
                };

                SpoOperation spoOperation = Tenant.CreateSite(siteCreationProperties);
                AdminContext.Load(spoOperation, s => s.IsComplete, s => s.PollingInterval, s => s.HasTimedout);
                AdminContext.ExecuteQueryRetry();

                if (Wait)
                {
                    while (!spoOperation.IsComplete)
                    {
                        if (spoOperation.HasTimedout)
                        {
                            throw new TimeoutException("Wait for site creation operation to complete has timed out.");
                        }
                        Task.Delay(TimeSpan.FromMilliseconds(spoOperation.PollingInterval)).GetAwaiter().GetResult();
                        spoOperation.RefreshLoad();
                        
                        if (((Cmdlet)this).Stopping)
                        {
                            ((Cmdlet)this).WriteWarning("Cmdlet execution interrupted by user, stopping wait for site creation operation to complete.");
                            break;
                        }
                        AdminContext.Load(spoOperation, s => s.IsComplete, s => s.PollingInterval, s => s.HasTimedout);
                        AdminContext.ExecuteQueryRetry();
                    }
                }

                if (Wait && SharingCapability.HasValue)
                {
                    SetSharingCapability(Url, SharingCapability.Value);
                }
            }
        }

        private void SetSharingCapability(string url, SharingCapabilities sharingCapability)
        {
            var props = Tenant.GetSitePropertiesByUrl(url, true);
            Tenant.Context.Load(props);
            Tenant.Context.ExecuteQueryRetry();
            
            props.SharingCapability = sharingCapability;
            var op = props.Update();
            AdminContext.Load(op, i => i.IsComplete, i => i.PollingInterval);
            AdminContext.ExecuteQueryRetry();
        }

        private bool TimeoutFunction(TenantOperationMessage message)
        {
            if (message == TenantOperationMessage.CreatingSiteCollection)
            {
                Host.UI.Write(".");
            }
            return Stopping;
        }
    }
}
