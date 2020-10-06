using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Get, "InPlaceRecordsManagement")]
    public class GetInPlaceRecordsManagement : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(ClientContext.Site.IsFeatureActive(new Guid(Microsoft.SharePoint.Client.RecordsManagementExtensions.INPLACE_RECORDS_MANAGEMENT_FEATURE_ID)));
        }

    }

}
