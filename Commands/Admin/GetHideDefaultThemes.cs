using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHideDefaultThemes")]
    public class GetHideDefaultThemes : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var value = Tenant.EnsureProperty(t => t.HideDefaultThemes);
            WriteObject(value);
        }
    }
}