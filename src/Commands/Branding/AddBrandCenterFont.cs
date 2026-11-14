using System.Collections;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPBrandCenterFont")]
    [OutputType(typeof(Microsoft.SharePoint.Client.File))]
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

            var fileName = System.IO.Path.GetFileName(Path);
            LogDebug($"Uploading the font to the Brand Center font library root folder at {brandCenterFontLibrary.RootFolder.ServerRelativeUrl}");
            var file = brandCenterFontLibrary.RootFolder.UploadFile(fileName, Path, true);

            // Brand Center lists a font only when its family name and font faces are set, as the Brand
            // Center page does when it uploads a font. Read them from the font file.
            var values = new Hashtable();
            var details = ReadFontDetails(fileName, Path);
            if (details.HasValue)
            {
                LogDebug($"Setting the font family name to '{details.Value.Family}' and the font face to '{details.Value.Face}'");
                values["_SPFontFamilyName"] = details.Value.Family;
                values["_SPFontFaces"] = details.Value.Face;
            }

            if (ParameterSpecified(nameof(Visible)) && Visible.HasValue)
            {
                LogDebug($"Setting the font visibility to {Visible.Value}");
                values["_SPFontVisible"] = Visible.Value ? "True" : "False";
            }

            if (values.Count > 0)
            {
                ListItemHelper.SetFieldValues(file.ListItemAllFields, values, this);
                file.ListItemAllFields.UpdateOverwriteVersion();
            }

            brandCenterContext.Load(file);
            brandCenterContext.ExecuteQueryRetry();

            LogDebug("Font uploaded successfully");
            WriteObject(file);
        }

        /// <summary>
        /// Family name and font face of a .ttf/.otf file, for example "TT Norms Pro" and "Black Italic".
        /// Returns null, with a warning, when they cannot be read.
        /// </summary>
        private (string Family, string Face)? ReadFontDetails(string fileName, string path)
        {
            if (!CustomFontsCatalogPublisher.IsOfficeFont(fileName))
            {
                LogWarning($"The font family name and font face of '{fileName}' are only read from .ttf and .otf files. Brand Center will not list this font until they are set.");
                return null;
            }

            try
            {
                var face = OfficeFontFace.Read(fileName, System.IO.File.ReadAllBytes(path));
                return (face.Family, face.Subfamily);
            }
            catch (InvalidDataException ex)
            {
                LogWarning($"Could not read the font family name and font face of '{fileName}' ({ex.Message}). Brand Center will not list this font until they are set.");
                return null;
            }
        }
    }
}
