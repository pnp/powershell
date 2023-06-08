using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Administration;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPBuiltInDesignPackageVisibility")]
    public class GetBuiltInDesignPackageVisibility : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public DesignPackageType DesignPackage;
        
        protected override void ExecuteCmdlet()
        {
            var result = Microsoft.Online.SharePoint.TenantAdministration.Tenant.GetHiddenBuiltInDesignPackages(AdminContext);
            AdminContext.ExecuteQueryRetry();

            var array = new DesignPackageType[4]
            {
                DesignPackageType.Topic,
                DesignPackageType.Showcase,
                DesignPackageType.Blank,
                DesignPackageType.TeamSite
            };

            if (ParameterSpecified(nameof(DesignPackage)))
            {
                if (Array.IndexOf(array, DesignPackage) >= 0)
                {
                    WriteObject((result.Value & DesignPackage) == 0);
                }
                else
                {
                    throw new PSArgumentException(nameof(DesignPackage));
                }
            }
            else
            {
                DesignPackageType[] types = array;
                foreach (var designPackageType in types)
                {
                    var o = new PSObject();
                    o.Properties.Add(new PSNoteProperty("DesignPackage", designPackageType));
                    o.Properties.Add(new PSNoteProperty("IsVisible", (result.Value & designPackageType) == 0));
                    WriteObject(o);
                }
            }
        }
    }
}