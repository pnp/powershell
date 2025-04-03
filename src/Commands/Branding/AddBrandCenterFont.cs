using System.Collections;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPBrandCenterFont")]
    [OutputType(typeof(File))]
    public class AddBrandCenterFont : PnPWebCmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true)]
        public string Path = string.Empty;

        [Parameter(Mandatory = false)]
        public bool? Visible = true;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var brandCenterConfig = BrandCenterUtility.GetBrandCenterConfiguration(this, ClientContext);
            if (brandCenterConfig == null || !brandCenterConfig.IsBrandCenterSiteFeatureEnabled || string.IsNullOrEmpty(brandCenterConfig.SiteUrl))
            {
                throw new PSArgumentException("Brand Center is not enabled for this tenant");
            }

            LogDebug($"Connecting to the Brand Center site at {brandCenterConfig.SiteUrl}");
            using var brandCenterContext = Connection.CloneContext(brandCenterConfig.SiteUrl);

            LogDebug($"Retrieving the Brand Center font library with ID {brandCenterConfig.BrandFontLibraryId}");
            var brandCenterFontLibrary = brandCenterContext.Web.GetListById(brandCenterConfig.BrandFontLibraryId);
            brandCenterContext.Load(brandCenterFontLibrary, l => l.RootFolder);
            brandCenterContext.ExecuteQueryRetry();

            LogDebug($"Uploading the font to the Brand Center font library root folder at {brandCenterFontLibrary.RootFolder.ServerRelativeUrl}");
            var file = brandCenterFontLibrary.RootFolder.UploadFile(System.IO.Path.GetFileName(Path), Path, true);

            if (ParameterSpecified(nameof(Visible)) && Visible.HasValue)
            {
                LogDebug($"Setting the font visibility to {Visible.Value}");
                ListItemHelper.SetFieldValues(file.ListItemAllFields, new Hashtable { { "_SPFontVisible", Visible.Value ? "True" : "False" } }, this);
                file.ListItemAllFields.UpdateOverwriteVersion();
            }

            brandCenterContext.Load(file);
            brandCenterContext.ExecuteQueryRetry();

            LogDebug("Font uploaded successfully");
            WriteObject(file);
        }
    }
}
