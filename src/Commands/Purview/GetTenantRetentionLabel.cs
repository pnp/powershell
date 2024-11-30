using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantRetentionLabel")]
    [OutputType(typeof(IEnumerable<Model.Graph.Purview.RetentionLabel>))]
    [OutputType(typeof(Model.Graph.Purview.RetentionLabel))]
    [RequiredApiDelegatedPermissions("graph/RecordsManagement.Read.All")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class GetTenantRetentionLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Guid Identity;

        protected override void ExecuteCmdlet()
        {
            string url = "/beta/security/labels/retentionLabels";

            if (ParameterSpecified(nameof(Identity)))
            {
                url += $"/{Identity}";

                var labels = RequestHelper.Get<Model.Graph.Purview.RetentionLabel>(url);
                WriteObject(labels, false);
            }
            else
            {
                var labels = RequestHelper.GetResultCollection<Model.Graph.Purview.RetentionLabel>(url);
                WriteObject(labels, true);
            }
        }
    }
}