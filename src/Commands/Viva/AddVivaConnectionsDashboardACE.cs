﻿using PnP.Core.Model.SharePoint;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Add, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(IVivaDashboard))]
    public class AddVivaConnectionsDashboardACE : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public DefaultACE Identity;

        [Parameter(Mandatory = true)]
        public int Order = 0;

        [Parameter(Mandatory = false)]
        public string Title = "";

        [Parameter(Mandatory = false)]
        public string PropertiesJSON;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string IconProperty;

        [Parameter(Mandatory = false)]
        public CardSize CardSize = CardSize.Medium;

        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;
            if (pnpContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = pnpContext.Web.GetVivaDashboard();

                var cardDesignerACE = dashboard.NewACE(Identity, CardSize);
                cardDesignerACE.Title = Title;
                if (ParameterSpecified(nameof(PropertiesJSON)))
                {
                    cardDesignerACE.Properties = JsonSerializer.Deserialize<JsonElement>(PropertiesJSON);
                }

                if (ParameterSpecified(nameof(Description)))
                {
                    cardDesignerACE.Description = Description;
                }

                if (ParameterSpecified(nameof(IconProperty)))
                {
                    cardDesignerACE.IconProperty = IconProperty;
                }

                if (ParameterSpecified(nameof(Order)) && Order > -1)
                {
                    dashboard.AddACE(cardDesignerACE, Order);
                }
                else
                {
                    dashboard.AddACE(cardDesignerACE);
                }

                dashboard.Save();

                // load the dashboard again
                dashboard = pnpContext.Web.GetVivaDashboard();
                WriteObject(dashboard, true);
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
