using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsDiagnostic.Resolve, "PnPFolder")]
    public class ResolveFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        // The messages SharePoint sends with these are localized, the error codes are not
        private const int FileNotFoundServerErrorCode = -2147024894;
        private const int NotInAListServerErrorCode = -2113929210;

        [Parameter(Mandatory = true, Position = 0)]
        public string SiteRelativePath = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.InvocationName.ToLower() == "ensure-pnpfolder")
            {
                LogWarning("Ensure-PnPFolder has been deprecated. Use Resolve-PnPFolder with the same parameters instead.");
            }

            var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var targetServerRelativeUrl = IsInsideWeb(SiteRelativePath, webServerRelativeUrl)
                ? SiteRelativePath
                : UrlUtility.Combine(webServerRelativeUrl, SiteRelativePath);

            var segments = targetServerRelativeUrl.Substring(webServerRelativeUrl.Length).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segment in segments)
            {
                if (segment.EndsWith('.'))
                {
                    ThrowTerminatingError(new ErrorRecord(
                        new PSArgumentException($"Folder names cannot end on a period (.), which '{segment}' in the provided path does.", nameof(SiteRelativePath)),
                        "InvalidFolderName",
                        ErrorCategory.InvalidArgument,
                        SiteRelativePath));
                }
            }

            // Resolving by path avoids the folder collection enumeration of PnP Framework's EnsureFolderPath, which exceeds the list view threshold past 5000 items
            var folder = TryGetFolder(targetServerRelativeUrl);

            if (folder == null)
            {
                LogDebug($"Folder '{targetServerRelativeUrl}' does not exist yet, walking the path to create the missing folders");

                var currentFolder = CurrentWeb.RootFolder;
                var currentServerRelativeUrl = webServerRelativeUrl;
                var creating = false;

                foreach (var segment in segments)
                {
                    currentServerRelativeUrl = UrlUtility.Combine(currentServerRelativeUrl, segment);

                    // Once a folder turns out to be missing none of the ones below it can exist, so stop probing
                    if (!creating)
                    {
                        folder = TryGetFolder(currentServerRelativeUrl);
                        if (folder != null)
                        {
                            currentFolder = folder;
                            continue;
                        }
                        creating = true;
                    }

                    LogDebug($"Creating folder '{currentServerRelativeUrl}'");
                    folder = currentFolder.Folders.AddUsingPath(ResourcePath.FromDecodedUrl(segment), new FolderCollectionAddParameters());
                    LoadCreatedFolder(folder);

                    SetTitle(folder, segment);
                    currentFolder = folder;
                }
            }

            if (folder == null)
            {
                ThrowTerminatingError(new ErrorRecord(
                    new PSArgumentException($"The folder '{targetServerRelativeUrl}' could not be resolved or created.", nameof(SiteRelativePath)),
                    "FolderNotResolved",
                    ErrorCategory.ObjectNotFound,
                    SiteRelativePath));
            }

            WriteObject(folder);
        }

        /// <summary>
        /// Whether the path already carries the server relative url of the web, in which case combining the two would look for it underneath itself. The prefix has to end on a segment boundary, so that a web at /sites/hr does not lay claim to a path under /sites/hrdocs.
        /// </summary>
        private static bool IsInsideWeb(string path, string webServerRelativeUrl)
        {
            if (!path.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return webServerRelativeUrl.EndsWith('/') || path.Length == webServerRelativeUrl.Length || path[webServerRelativeUrl.Length] == '/';
        }

        /// <summary>
        /// Queues the default properties, as PnP Framework's EnsureFolderPath returned them, whatever -Includes asks for on top, and the list item carrying the Title.
        /// </summary>
        private void LoadFolder(Folder folder, bool withListItem)
        {
            ClientContext.Load(folder);

            foreach (var expression in RetrievalExpressions)
            {
                if (!withListItem && IsListItemAllFields(expression))
                {
                    LogWarning("The folder belongs to no list, so ListItemAllFields is not retrieved for it.");
                    continue;
                }

                ClientContext.Load(folder, expression);
            }

            if (withListItem)
            {
                ClientContext.Load(folder, f => f.ListItemAllFields);
            }
        }

        /// <summary>Whether the expression asks for ListItemAllFields itself, which a folder outside of a list cannot answer for.</summary>
        private static bool IsListItemAllFields(Expression<Func<Folder, object>> expression)
        {
            var body = expression.Body is UnaryExpression unary ? unary.Operand : expression.Body;
            return body is MemberExpression member && member.Member.Name == nameof(Folder.ListItemAllFields);
        }

        /// <summary>Loads a freshly created folder, falling back for one which lives outside of a list and therefore carries no list item. The folder itself is created by the batch that fails on the list item.</summary>
        private void LoadCreatedFolder(Folder folder)
        {
            LoadFolder(folder, withListItem: true);

            try
            {
                ClientContext.ExecuteQueryRetry();
            }
            catch (ServerException e) when (e.ServerErrorCode == NotInAListServerErrorCode)
            {
                LoadFolder(folder, withListItem: false);
                ClientContext.ExecuteQueryRetry();
            }
        }

        /// <summary>
        /// Returns the folder, or null when SharePoint reports none is there.
        /// The Exists property cannot be used for this, as resolving a missing folder raises a FileNotFoundException instead of returning it as false.
        /// Any other failure surfaces, so that a path we were merely denied access to is not created over.
        /// </summary>
        private Folder TryGetFolder(string serverRelativeUrl, bool withListItem = true)
        {
            var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            LoadFolder(folder, withListItem);

            try
            {
                ClientContext.ExecuteQueryRetry();
            }
            catch (ServerException e) when (e.ServerErrorCode == FileNotFoundServerErrorCode)
            {
                return null;
            }
            catch (ServerException e) when (withListItem && e.ServerErrorCode == NotInAListServerErrorCode)
            {
                // The root folder of a web belongs to no list, so it carries no Title to read
                return TryGetFolder(serverRelativeUrl, withListItem: false);
            }

            return folder;
        }

        /// <summary>
        /// Creating a folder by path leaves its Title empty, while PnP Framework's EnsureFolderPath set it to the folder name. 
        /// Folders which carry no list item, and lists which do not have the Title field, are left alone as EnsureFolderPath did.
        /// </summary>
        private void SetTitle(Folder folder, string title)
        {
            if (!folder.IsObjectPropertyInstantiated(nameof(Folder.ListItemAllFields)))
            {
                LogDebug("The created folder carries no list item, leaving its Title unset");
                return;
            }

            var listItem = folder.ListItemAllFields;
            if (listItem == null || listItem.ServerObjectIsNull == true || listItem.FieldValues == null || !listItem.FieldValues.ContainsKey("Title"))
            {
                LogDebug("The created folder carries no Title field, leaving it unset");
                return;
            }

            LogDebug($"Setting the Title of the created folder to '{title}'");
            listItem["Title"] = title;
            listItem.Update();
            ClientContext.ExecuteQueryRetry();
        }
    }
}
