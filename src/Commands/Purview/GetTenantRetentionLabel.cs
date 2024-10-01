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
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/RecordsManagement.Read.All")]
    [WriteAliasWarning("Get-PnPRetentionLabel will be renamed to Get-PnPTenantRetentionLabel in a future version, please update your scripts now already to use this cmdlet name instead")]
    [Alias("Get-PnPRetentionLabel")]
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

                var labels = GraphHelper.Get<Model.Graph.Purview.RetentionLabel>(this, Connection, url, AccessToken);
                WriteObject(labels, false);
            }
            else
            {
                var labels = GraphHelper.GetResultCollection<Model.Graph.Purview.RetentionLabel>(this, Connection, url, AccessToken);
                WriteObject(labels, true);
            }
        }
    }
}