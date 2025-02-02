using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;
using System.Collections.Generic;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantSyncClientRestriction", DefaultParameterSetName = ParameterAttribute.AllParameterSets)]
    public class SetTenantSyncClientRestriction : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter BlockMacSync;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisableReportProblemDialog;

        [Parameter(Mandatory = false)]
        public List<Guid> DomainGuids;

        [Parameter(Mandatory = false)]
        public SwitchParameter Enable;

        [Parameter(Mandatory = false)]
        public List<string> ExcludedFileExtensions;

        [Parameter(Mandatory = false)]
        public Enums.GrooveBlockOption GrooveBlockOption;

        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);
            AdminContext.ExecuteQueryRetry();

            if (ParameterSpecified(nameof(DomainGuids)))
            {
                Tenant.AllowedDomainListForSyncClient = new List<Guid>(DomainGuids);
            }

            Tenant.BlockMacSync = BlockMacSync.ToBool();
            Tenant.IsUnmanagedSyncClientForTenantRestricted = Enable.ToBool();
            Tenant.DisableReportProblemDialog = DisableReportProblemDialog.ToBool();

            if (ParameterSpecified(nameof(ExcludedFileExtensions)))
            {
                Tenant.ExcludedFileExtensionsForSyncClient = ExcludedFileExtensions;
            }
            
            if(ParameterSpecified(nameof(GrooveBlockOption)))
            {
                switch (GrooveBlockOption)
                {
                    case Enums.GrooveBlockOption.OptOut:
                        Tenant.OptOutOfGrooveBlock = true;
                        Tenant.OptOutOfGrooveSoftBlock = true;
                        break;

                    case Enums.GrooveBlockOption.HardOptin:
                        Tenant.OptOutOfGrooveBlock = false;
                        Tenant.OptOutOfGrooveSoftBlock = true;
                        break;

                    case Enums.GrooveBlockOption.SoftOptin:
                        Tenant.OptOutOfGrooveBlock = true;
                        Tenant.OptOutOfGrooveSoftBlock = false;
                        break;

                    default:
                        throw new PSArgumentException(string.Format(Resources.GrooveBlockOptionNotSupported, nameof(GrooveBlockOption), GrooveBlockOption), nameof(GrooveBlockOption));
                }
            }
            AdminContext.ExecuteQueryRetry();
        }
    }
}