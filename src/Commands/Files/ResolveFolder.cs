using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsDiagnostic.Resolve, "PnPFolder")]
    public class ResolveFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string SiteRelativePath = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.InvocationName.ToLower() == "ensure-pnpfolder")
            {
                LogWarning("Ensure-PnPFolder has been deprecated. Use Resolve-PnPFolder with the same parameters instead.");
            }

            var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            // Resolve folders through GetFolderByServerRelativePath rather than PnP Framework's
            // EnsureFolderPath, as the latter enumerates the entire Folders collection of each path
            // segment which exceeds the list view threshold on folders holding more than 5000 items
            var targetServerRelativeUrl = UrlUtility.Combine(webServerRelativeUrl, SiteRelativePath);
            var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(targetServerRelativeUrl));
            ClientContext.Load(folder, f => f.Exists);
            ClientContext.ExecuteQueryRetry();

            if (!folder.Exists)
            {
                // Walk the path segment by segment, creating each folder that does not exist yet
                var currentFolder = CurrentWeb.RootFolder;
                var currentServerRelativeUrl = webServerRelativeUrl;
                foreach (var segment in SiteRelativePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    currentServerRelativeUrl = UrlUtility.Combine(currentServerRelativeUrl, segment);
                    folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(currentServerRelativeUrl));
                    ClientContext.Load(folder, f => f.Exists);
                    ClientContext.ExecuteQueryRetry();

                    if (!folder.Exists)
                    {
                        folder = currentFolder.Folders.AddUsingPath(ResourcePath.FromDecodedUrl(segment), new FolderCollectionAddParameters());
                        ClientContext.ExecuteQueryRetry();
                    }
                    currentFolder = folder;
                }
            }

            folder.EnsureProperties(RetrievalExpressions);
            WriteObject(folder);
        }
    }
}
