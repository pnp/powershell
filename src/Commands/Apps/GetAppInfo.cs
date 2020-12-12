using System.Management.Automation;
using System;
using PnP.Framework.Enums;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAppInfo")]
    public class GetAppInfo : PnPAdminCmdlet
    {
        private const string ParameterSet_BYID = "By Product Id";
        private const string ParameterSet_BYNAME = "By Product Name";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYID)]
        public Guid ProductId;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYNAME)]
        [ValidateNotNullOrEmpty]
        public string Name;

        protected override void ExecuteCmdlet()
        {
            IEnumerable<AppInfo> appInfo = null;
            switch (ParameterSetName)
            {
                case ParameterSet_BYID:
                    {
                        appInfo = ClientContext.LoadQuery(this.Tenant.GetAppInfoByProductId(ProductId));
                        break;
                    }
                case ParameterSet_BYNAME:
                    {
                        appInfo = ClientContext.LoadQuery(this.Tenant.GetAppInfoByName(Name));
                        break;
                    }
            }
            ClientContext.ExecuteQueryRetry();
            WriteObject(appInfo, true);
        }
    }
}