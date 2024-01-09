﻿using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerTypeConfiguration")]
    public class GetContainerTypeConfiguration : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity == Guid.Empty)
            {
                throw new ArgumentException($"Identity {Identity} value is invalid");
            }
            ClientResult<SPContainerTypeConfigurationProperties> sPOContainerTypeConfigurationByContainerTypeId = Tenant.GetSPOContainerTypeConfigurationByContainerTypeId(Identity);
            AdminContext.ExecuteQuery();
            if (sPOContainerTypeConfigurationByContainerTypeId != null && sPOContainerTypeConfigurationByContainerTypeId.Value != null)
            {
                WriteObject(new Model.SharePoint.SPContainerTypeConfigurationPropertiesObj(sPOContainerTypeConfigurationByContainerTypeId.Value));
            }
        }
    }
}
