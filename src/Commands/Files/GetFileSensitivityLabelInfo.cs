﻿using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Files
{

    [Cmdlet(VerbsCommon.Get, "PnPFileSensitivityLabelInfo")]
    public class GetFileSensitivityLabelInfo: PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;
        protected override void ExecuteCmdlet()
        {
            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            var fileSensitivityLabelInfo = Tenant.GetFileSensitivityLabelInfo(Url);
            AdminContext.Load(fileSensitivityLabelInfo);
            AdminContext.ExecuteQueryRetry();
            WriteObject(new Model.SharePoint.SPOFileSensitivityLabelInfo(fileSensitivityLabelInfo));
        }
    }
}
