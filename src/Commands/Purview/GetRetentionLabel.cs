using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Get, "PnPRetentionLabel")]
    [OutputType(typeof(IEnumerable<Model.Graph.Purview.RetentionLabel>))]
    [OutputType(typeof(Model.Graph.Purview.RetentionLabel))]
    public class GetAvailableRetentionLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Guid Identity;

        protected override void ExecuteCmdlet()
        {
            string url;
            url = "/beta/security/labels/retentionLabels";

            if (ParameterSpecified(nameof(Identity)))
            {
                url += $"/{Identity}";

                var labels = GraphHelper.GetAsync<Model.Graph.Purview.RetentionLabel>(Connection, url, AccessToken).GetAwaiter().GetResult();
                WriteObject(labels, false);
            }
            else
            {
                var labels = GraphHelper.GetResultCollectionAsync<Model.Graph.Purview.RetentionLabel>(Connection, url, AccessToken).GetAwaiter().GetResult();
                WriteObject(labels, true);
            }
        }
    }
}