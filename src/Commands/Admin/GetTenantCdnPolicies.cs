using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantCdnPolicies")]
    public class GetTenantCdnPolicies : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            var result = Tenant.GetTenantCdnPolicies(CdnType);
            AdminContext.ExecuteQueryRetry();

            WriteObject(Parse(result),true);
        }

        private Dictionary<Microsoft.Online.SharePoint.TenantAdministration.SPOTenantCdnPolicyType, string> Parse(IList<string> entries)
        {
            var returnDict = new Dictionary<SPOTenantCdnPolicyType, string>();
            foreach(var entry in entries)
            {
                var entryArray = entry.Split(new[] { ';' });
                returnDict.Add((SPOTenantCdnPolicyType)Enum.Parse(typeof(SPOTenantCdnPolicyType), entryArray[0]), entryArray[1]);
            }
            return returnDict;
        }
    }
}