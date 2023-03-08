using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.Framework.Entities;

using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSite")]
    public class NewTenantSite : PnPAdminCmdlet
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
            Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

            Guid newSiteId = Tenant.CreateSiteCollection(Url, Title, Owner, Template, (int)StorageQuota,
                (int)StorageQuotaWarningLevel, TimeZone, (int)ResourceQuota, (int)ResourceQuotaWarningLevel, Lcid,
                RemoveDeletedSite, Wait, Wait == true ? timeoutFunction : null);

            if (newSiteId != Guid.Empty && Wait && SharingCapability.HasValue)
            {
                var props = Tenant.GetSitePropertiesByUrl(Url, true);
                Tenant.Context.Load(props);
                Tenant.Context.ExecuteQueryRetry();

                props.SharingCapability = SharingCapability.Value;

                var op = props.Update();
                ClientContext.Load(op, i => i.IsComplete, i => i.PollingInterval);
                ClientContext.ExecuteQueryRetry();
            }
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
