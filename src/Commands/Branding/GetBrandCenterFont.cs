using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPBrandCenterFont", DefaultParameterSetName = ParameterSet_ALL)]
    [OutputType(typeof(Font), ParameterSetName = new[] { ParameterSet_SINGLE })]
    [OutputType(typeof(IEnumerable<Font>), ParameterSetName = new[] { ParameterSet_ALL })]
    public class GetBrandCenterFont : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        public BrandCenterFontPipeBind Identity { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public Store Store { get; set; } = Store.All;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            if (ParameterSpecified(nameof(Identity)))
            {
                var font = Identity.GetFont(this, ClientContext, Connection, CurrentWeb.Url, Store);
                WriteObject(font, false);
            }
            else
            {
                WriteObject(BrandCenterUtility.GetFonts(this, ClientContext, Connection, CurrentWeb.Url, Store), true);
            }
        }
    }
}
