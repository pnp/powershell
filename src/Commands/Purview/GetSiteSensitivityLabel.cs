using Microsoft.SharePoint.Client;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteSensitivityLabel")]
    [OutputType(typeof(PnP.PowerShell.Commands.Model.SharePoint.SensitivityLabel))]
    public class GetSiteSensitivityLabel : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Site, s => s.SensitivityLabelInfo);
            ClientContext.ExecuteQueryRetry();

            WriteObject(new PnP.PowerShell.Commands.Model.SharePoint.SensitivityLabel {
                Id = Guid.TryParse(ClientContext.Site.SensitivityLabelInfo.Id, out Guid labelGuid) ? (Guid?) labelGuid : null,
                DisplayName = ClientContext.Site.SensitivityLabelInfo.DisplayName
            }, false);
        }
    }
}