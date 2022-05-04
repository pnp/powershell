﻿using PnP.Core.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Remove, "PnPVivaConnectionsDashboardACE")]
    public class RemoveVivaConnectionsACE : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid Identity;
        protected override void ExecuteCmdlet()
        {
            if (PnPContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = PnPContext.Web.GetVivaDashboardAsync().GetAwaiter().GetResult();
                var aceToRemove = dashboard.ACEs.FirstOrDefault(p => p.InstanceId == Identity);

                if (aceToRemove != null)
                {
                    dashboard.RemoveACE(aceToRemove.InstanceId);
                    dashboard.Save();                   
                }
                else
                {
                    WriteWarning("ACE with specified instance Id not found");
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
