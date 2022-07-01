using PnP.Core.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsData.Update, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(IVivaDashboard))]
    public class UpdateVivaConnectionsACE : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid Identity;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string PropertiesJSON;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string IconProperty;

        [Parameter(Mandatory = false)]
        public int Order;

        [Parameter(Mandatory = false)]
        public CardSize CardSize = CardSize.Medium;

        protected override void ExecuteCmdlet()
        {
            if (PnPContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = PnPContext.Web.GetVivaDashboardAsync().GetAwaiter().GetResult();
                var aceToUpdate = dashboard.ACEs.FirstOrDefault(p => p.InstanceId == Identity);

                if (aceToUpdate != null)
                {
                    bool updateRequired = false;
                    if (ParameterSpecified(nameof(Title)))
                    {
                        aceToUpdate.Title = Title;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(PropertiesJSON)))
                    {
                        aceToUpdate.Properties = JsonSerializer.Deserialize<JsonElement>(PropertiesJSON);
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(Description)))
                    {
                        aceToUpdate.Description = Description;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(IconProperty)))
                    {
                        aceToUpdate.IconProperty = IconProperty;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(CardSize)))
                    {
                        aceToUpdate.CardSize = CardSize;
                        updateRequired = true;
                    }

                    if (updateRequired)
                    {
                        if (ParameterSpecified(nameof(Order)) && Order > -1)
                        {
                            dashboard.UpdateACE(aceToUpdate, Order);
                        }
                        else
                        {
                            dashboard.UpdateACE(aceToUpdate);
                        }

                        dashboard.Save();

                        dashboard = PnPContext.Web.GetVivaDashboardAsync().GetAwaiter().GetResult();
                        WriteObject(dashboard, true);
                    }
                }
                else
                {
                    WriteWarning("ACE with specified instance ID not found");
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
