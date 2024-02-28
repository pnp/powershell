using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using System.Management.Automation;
using File = Microsoft.SharePoint.Client.File;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFileVersion", DefaultParameterSetName = "Return as file object")]
 
    public class GetFileVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = false)]
        public SwitchParameter? UseVersionExpirationReport;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            File file;

            file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            
            var doesUseVersionExpirationReport = UseVersionExpirationReport.HasValue && UseVersionExpirationReport.Value;
            
            if (doesUseVersionExpirationReport)
            {
                ClientContext.Load(file, f => f.Exists, f => f.VersionExpirationReport.IncludeWithDefaultProperties(i => i.CreatedBy, i => i.SnapshotDate, i => i.ExpirationDate));
            }
            else
            {
                ClientContext.Load(file, f => f.Exists, f => f.Versions.IncludeWithDefaultProperties(i => i.CreatedBy, i => i.SnapshotDate, i => i.ExpirationDate));
            }

            ClientContext.ExecuteQueryRetry();

            if (file.Exists)
            {
                var versions = doesUseVersionExpirationReport ? file.VersionExpirationReport : file.Versions;
                ClientContext.ExecuteQueryRetry();
                WriteObject(versions, true);
            }
        }
    }
}
