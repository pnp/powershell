﻿using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Get, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(AdaptiveCardExtension))]
    public class GetVivaConnectionsDashboard : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public VivaACEPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;
            if (pnpContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = pnpContext.Web.GetVivaDashboard();

                if (ParameterSpecified(nameof(Identity)))
                {
                    var aceToRetrieve = Identity.GetACE(dashboard, this);
                    if (aceToRetrieve != null)
                    {
                        WriteObject(aceToRetrieve);
                    }
                    else
                    {
                        WriteWarning("ACE with specified identifier not found");
                    }
                }
                else
                {
                    WriteObject(dashboard.ACEs, true);
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
