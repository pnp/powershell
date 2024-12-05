using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Get, "PnPInPlaceRecordsManagement")]
    public class GetInPlaceRecordsManagement : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(ClientContext.Site.IsFeatureActive(new Guid(Microsoft.SharePoint.Client.RecordsManagementExtensions.INPLACE_RECORDS_MANAGEMENT_FEATURE_ID)));
        }

    }

}
