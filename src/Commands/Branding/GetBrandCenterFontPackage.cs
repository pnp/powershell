using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPBrandCenterFontPackage", DefaultParameterSetName = ParameterSet_ALL)]
    [OutputType(typeof(FontPackage), ParameterSetName = new[] { ParameterSet_SINGLE })]
    [OutputType(typeof(IEnumerable<FontPackage>), ParameterSetName = new[] { ParameterSet_ALL })]
    public class GetBrandCenterFontPackage : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        [ArgumentCompleter(typeof(BrandCenterFontPackageCompleter))]
        public BrandCenterFontPackagePipeBind Identity { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public Store Store { get; set; } = Store.All;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            if (ParameterSpecified(nameof(Identity)))
            {
                var font = Identity.GetFontPackage(this, ClientContext, CurrentWeb.Url, Store);
                WriteObject(font, false);
            }
            else
            {
                WriteObject(BrandCenterUtility.GetFontPackages(this, ClientContext, CurrentWeb.Url, Store), true);
            }
        }
    }
}
