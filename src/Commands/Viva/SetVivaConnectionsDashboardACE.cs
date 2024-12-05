using PnP.Core.Model.SharePoint;
using System.Text.Json;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Set, "PnPVivaConnectionsDashboardACE", DefaultParameterSetName = ParameterSet_TYPEDPROPERTIES)]
    [Alias("Update-PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(IVivaDashboard))]
    public class SetVivaConnectionsACE : PnPWebCmdlet
    {
        private const string ParameterSet_JSONProperties = "Update using JSON properties";
        private const string ParameterSet_TYPEDPROPERTIES = "Update using typed properties";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public VivaACEPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public string Title;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        public string PropertiesJSON;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public object Properties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public string Description;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public string IconProperty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public int Order;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_JSONProperties)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TYPEDPROPERTIES)]
        public CardSize CardSize = CardSize.Medium;

        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;
            if (pnpContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = pnpContext.Web.GetVivaDashboard();

                var aceToUpdate = Identity.GetACE(dashboard, this);

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

                    if (ParameterSpecified(nameof(Properties)))
                    {
                        // Serialize the properties object to JSON so that the JsonPropertyName attributes get applied for correct naming and casing and then assign the result back
                        var serializedProperties = JsonSerializer.Serialize(Properties as CardDesignerProps);
                        aceToUpdate.Properties = serializedProperties;
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

                        dashboard = pnpContext.Web.GetVivaDashboard();
                        WriteObject(dashboard, true);
                    }
                }
                else
                {
                    WriteWarning("ACE with specified identifier not found");
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}