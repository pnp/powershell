using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Copy, "PnPFileMetadata")]
    public class CopyFileMetadata : PnPWebCmdlet
    {
    // Cache for resolved users on the target to minimize EnsureUser calls
    private readonly System.Collections.Generic.Dictionary<string, int> _targetUserCache = new System.Collections.Generic.Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl")]
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        [Alias("TargetServerRelativeUrl")]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public string[] Fields = new[] { "Created", "Modified", "Author", "Editor" };

    [Parameter(Mandatory = false)]
    public SwitchParameter Recursive = SwitchParameter.Present;

        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used for accessing the source file. If not provided, uses the current connection.")]
        public PnPConnection SourceConnection = null;

        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used for accessing the target file. If not provided, uses the current connection.")]
        public PnPConnection TargetConnection = null;

        protected override void ExecuteCmdlet()
        {
            // Get the contexts for source and target operations
            var sourceContext = GetSourceContext();
            var targetContext = GetTargetContext();
            
            // Ensure we have valid contexts
            if (sourceContext == null)
            {
                throw new InvalidOperationException("Source connection is not available. Please provide a valid SourceConnection or ensure you are connected.");
            }
            
            if (targetContext == null)
            {
                throw new InvalidOperationException("Target connection is not available. Please provide a valid TargetConnection or ensure you are connected.");
            }
            
            var sourceWebServerRelativeUrl = sourceContext.Web.EnsureProperty(w => w.ServerRelativeUrl);
            var targetWebServerRelativeUrl = targetContext.Web.EnsureProperty(w => w.ServerRelativeUrl);

            WriteVerbose($"Source context web URL: {sourceWebServerRelativeUrl}");
            WriteVerbose($"Target context web URL: {targetWebServerRelativeUrl}");

            // Process URLs relative to their respective contexts
            SourceUrl = ProcessUrl(SourceUrl, sourceWebServerRelativeUrl);
            TargetUrl = ProcessUrl(TargetUrl, targetWebServerRelativeUrl);

            WriteVerbose($"Processed SourceUrl: {SourceUrl}");
            WriteVerbose($"Processed TargetUrl: {TargetUrl}");

            if (Force || ShouldContinue(string.Format("Synchronize metadata from '{0}' to '{1}'", SourceUrl, TargetUrl), Resources.Confirm))
            {
                try
                {
                    WriteVerbose($"Starting metadata synchronization from '{SourceUrl}' to '{TargetUrl}'");
                    WriteVerbose($"Fields to sync: {string.Join(", ", Fields)}");
                    
                    // Determine if source is a file or folder (using source context)
                    var sourceItem = GetFileOrFolderInfo(SourceUrl, sourceContext);
                    if (sourceItem.Name == null)
                    {
                        WriteError(new ErrorRecord(
                            new PSArgumentException($"Source path '{SourceUrl}' not found."),
                            "SourceNotFound",
                            ErrorCategory.ObjectNotFound,
                            SourceUrl));
                        return;
                    }

                    var itemsProcessed = 0;
                    var itemsSkipped = 0;
                    var itemsErrored = 0;

                    if (sourceItem.IsFile)
                    {
                        var result = SyncFileMetadata(SourceUrl, TargetUrl, sourceContext, targetContext);
                        if (result == SyncResult.Success) itemsProcessed++;
                        else if (result == SyncResult.Skipped) itemsSkipped++;
                        else itemsErrored++;
                    }
                    else
                    {
                        var results = SyncFolderMetadataRecursive(SourceUrl, TargetUrl, sourceContext, targetContext);
                        itemsProcessed = results.Processed;
                        itemsSkipped = results.Skipped;
                        itemsErrored = results.Errored;
                    }

                    WriteObject($"Metadata synchronization completed. Processed: {itemsProcessed}, Skipped: {itemsSkipped}, Errors: {itemsErrored}");
                }
                catch (Exception ex)
                {
                    WriteError(new ErrorRecord(ex, "MetadataSyncError", ErrorCategory.InvalidOperation, SourceUrl));
                }
            }
        }

        private (int Processed, int Skipped, int Errored) SyncFolderMetadataRecursive(string sourceFolderUrl, string targetFolderUrl)
        {
            return SyncFolderMetadataRecursive(sourceFolderUrl, targetFolderUrl, ClientContext, ClientContext);
        }

        private (int Processed, int Skipped, int Errored) SyncFolderMetadataRecursive(string sourceFolderUrl, string targetFolderUrl, ClientContext sourceContext, ClientContext targetContext)
        {
            var processed = 0;
            var skipped = 0;
            var errored = 0;

            try
            {
                WriteVerbose($"Processing folder: {sourceFolderUrl}");
                
                // Get source and target folder via ResourcePath and validate existence
                var sourceFolder = sourceContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Uri.UnescapeDataString(sourceFolderUrl)));
                var targetFolder = targetContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Uri.UnescapeDataString(targetFolderUrl)));

                // Step 1: check existence first to avoid exceptions when loading list item of a non-existing folder
                sourceContext.Load(sourceFolder, f => f.Name, f => f.Exists);
                targetContext.Load(targetFolder, f => f.Name, f => f.Exists);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                if (!sourceFolder.Exists)
                {
                    WriteWarning($"Source folder not found: '{sourceFolderUrl}'");
                    errored++;
                    return (processed, skipped, errored);
                }

                if (!targetFolder.Exists)
                {
                    WriteWarning($"Target folder not found, skipping subtree: '{targetFolderUrl}'");
                    skipped++;
                    return (processed, skipped, errored);
                }

                // Step 2: load heavy properties once existence is confirmed
                sourceContext.Load(sourceFolder, f => f.ListItemAllFields, f => f.Files, f => f.Folders);
                targetContext.Load(targetFolder, f => f.ListItemAllFields);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                // Sync folder metadata if both have list items
                if (!sourceFolder.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault() &&
                    !targetFolder.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault())
                {
                    var result = SyncListItemMetadata(sourceFolder.ListItemAllFields, targetFolder.ListItemAllFields, sourceContext, targetContext);
                    if (result == SyncResult.Success) processed++;
                    else if (result == SyncResult.Skipped) skipped++;
                    else errored++;
                }

                // When Recursive is enabled, process files and subfolders; otherwise only the folder item itself
                if (Recursive)
                {
                    // Process files in the current folder
                    if (sourceFolder.Files != null && sourceFolder.Files.Count > 0)
                    {
                        foreach (var sourceFile in sourceFolder.Files)
                        {
                            var src = UrlUtility.Combine(sourceFolderUrl, sourceFile.Name);
                            var dst = UrlUtility.Combine(targetFolderUrl, sourceFile.Name);
                            var r = SyncFileMetadata(src, dst, sourceContext, targetContext);
                            if (r == SyncResult.Success) processed++;
                            else if (r == SyncResult.Skipped) skipped++;
                            else errored++;
                        }
                    }

                    // Process subfolders recursively
                    foreach (var sourceSubfolder in sourceFolder.Folders)
                    {
                        if (sourceSubfolder.Name.StartsWith("_")) continue; // Skip system folders

                        var sourceSubfolderUrlCombined = UrlUtility.Combine(sourceFolderUrl, sourceSubfolder.Name);
                        var targetSubfolderUrl = UrlUtility.Combine(targetFolderUrl, sourceSubfolder.Name);
                        var subResults = SyncFolderMetadataRecursive(sourceSubfolderUrlCombined, targetSubfolderUrl, sourceContext, targetContext);

                        processed += subResults.Processed;
                        skipped += subResults.Skipped;
                        errored += subResults.Errored;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteWarning($"Failed to process folder '{sourceFolderUrl}': {ex.Message}");
                errored++;
            }

            return (processed, skipped, errored);
        }

        private SyncResult SyncFileMetadata(string sourceFileUrl, string targetFileUrl)
        {
            return SyncFileMetadata(sourceFileUrl, targetFileUrl, ClientContext, ClientContext);
        }

        private SyncResult SyncFileMetadata(string sourceFileUrl, string targetFileUrl, ClientContext sourceContext, ClientContext targetContext)
        {
            try
            {
                WriteVerbose($"Syncing file metadata: {sourceFileUrl} -> {targetFileUrl}");
                // Decode and use server-relative ResourcePath to reliably check existence
                var decodedSourceUrl = Uri.UnescapeDataString(sourceFileUrl);
                var decodedTargetUrl = Uri.UnescapeDataString(targetFileUrl);

                var sourceFile = sourceContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedSourceUrl));
                var targetFile = targetContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedTargetUrl));

                sourceContext.Load(sourceFile, f => f.Exists, f => f.Name, f => f.ListItemAllFields);
                targetContext.Load(targetFile, f => f.Exists, f => f.Name, f => f.ListItemAllFields);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                if (!sourceFile.Exists)
                {
                    WriteWarning($"Source file not found: '{decodedSourceUrl}'");
                    return SyncResult.Error;
                }

                if (!targetFile.Exists)
                {
                    WriteWarning($"Target file not found, skipping: '{decodedTargetUrl}'");
                    return SyncResult.Skipped;
                }

                if (!sourceFile.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault() &&
                    !targetFile.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault())
                {
                    return SyncListItemMetadata(sourceFile.ListItemAllFields, targetFile.ListItemAllFields, sourceContext, targetContext);
                }
                else
                {
                    WriteWarning($"Cannot access list item for file '{sourceFileUrl}' or '{targetFileUrl}'");
                    return SyncResult.Skipped;
                }
            }
            catch (Exception ex)
            {
                WriteWarning($"Failed to sync file metadata from '{sourceFileUrl}' to '{targetFileUrl}': {ex.Message}");
                return SyncResult.Error;
            }
        }

        private SyncResult SyncListItemMetadata(ListItem sourceItem, ListItem targetItem, ClientContext sourceContext, ClientContext targetContext)
        {
            try
            {
                // Load necessary fields from source using the source context
                sourceContext.Load(sourceItem);
                sourceContext.ExecuteQueryRetry();

                var metadataUpdated = false; // track if we changed anything

                // Always set Created/Modified from source when available
                try
                {
                    if (sourceItem.FieldValues.ContainsKey("Created") && sourceItem.FieldValues["Created"] is DateTime cdt)
                    {
                        targetItem["Created"] = cdt;
                        metadataUpdated = true;
                        WriteVerbose($"Updated Created: {cdt}");
                    }
                    if (sourceItem.FieldValues.ContainsKey("Modified") && sourceItem.FieldValues["Modified"] is DateTime mdt)
                    {
                        targetItem["Modified"] = mdt;
                        metadataUpdated = true;
                        WriteVerbose($"Updated Modified: {mdt}");
                    }
                }
                catch (Exception ex)
                {
                    WriteWarning($"Failed to set Created/Modified: {ex.Message}");
                }

                // Process the rest of the requested fields
                foreach (var fieldName in Fields)
                {
                    if (fieldName == "Created" || fieldName == "Modified")
                    {
                        continue; // already handled
                    }

                    try
                    {
                        if (sourceItem.FieldValues.ContainsKey(fieldName) && sourceItem.FieldValues[fieldName] != null)
                        {
                            var sourceValue = sourceItem.FieldValues[fieldName];

                            if (fieldName == "Author" || fieldName == "Editor")
                            {
                                if (sourceValue is FieldUserValue userValue && userValue.LookupId > 0)
                                {
                                    var mapped = MapUserToTarget(userValue, sourceContext, targetContext);
                                    if (mapped != null)
                                    {
                                        targetItem[fieldName] = mapped;
                                        metadataUpdated = true;
                                        WriteVerbose($"Updated {fieldName} (mapped to target)");
                                    }
                                }
                            }
                            else
                            {
                                targetItem[fieldName] = sourceValue;
                                metadataUpdated = true;
                                WriteVerbose($"Updated {fieldName}: {sourceValue}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteWarning($"Failed to sync field '{fieldName}': {ex.Message}");
                    }
                }

                if (metadataUpdated)
                {
                    targetItem.UpdateOverwriteVersion();
                    targetContext.ExecuteQueryRetry();
                    return SyncResult.Success;
                }
                else
                {
                    return SyncResult.Skipped;
                }
            }
            catch (Exception ex)
            {
                WriteWarning($"Failed to sync list item metadata: {ex.Message}");
                return SyncResult.Error;
            }
        }

        private (bool IsFile, string Name) GetFileOrFolderInfo(string url, ClientContext context)
        {
            var webServerRelativeUrl = context.Web.EnsureProperty(w => w.ServerRelativeUrl);
            WriteVerbose($"GetFileOrFolderInfo: Checking URL '{url}' using context for '{webServerRelativeUrl}'");

            // Ensure we have a server-relative URL (starting with '/')
            string serverRelativeUrl = url;
            if (!serverRelativeUrl.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                serverRelativeUrl = UrlUtility.Combine(webServerRelativeUrl, serverRelativeUrl);
                WriteVerbose($"Normalized to server-relative URL: '{serverRelativeUrl}'");
            }

            // CSOM ResourcePath.FromDecodedUrl expects a decoded server-relative path
            var decodedServerRelative = Uri.UnescapeDataString(serverRelativeUrl);
            WriteVerbose($"Decoded server-relative URL for lookup: '{decodedServerRelative}'");

            try
            {
                // Try as file first using GetFileByServerRelativePath
                WriteVerbose($"Trying as file (server-relative): {decodedServerRelative}");
                var file = context.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedServerRelative));
                context.Load(file, f => f.Name, f => f.Exists);
                context.ExecuteQueryRetry();

                WriteVerbose($"File exists check result: {file.Exists}");
                if (file.Exists)
                {
                    WriteVerbose($"Found file: {file.Name}");
                    return (true, file.Name);
                }
            }
            catch (Exception ex)
            {
                WriteVerbose($"Exception when checking as file: {ex.Message}");
            }

            try
            {
                WriteVerbose($"Trying as folder (server-relative): {decodedServerRelative}");
                var folder = context.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(decodedServerRelative));
                context.Load(folder, f => f.Name, f => f.Exists);
                context.ExecuteQueryRetry();

                WriteVerbose($"Folder exists check result: {folder.Exists}");
                if (folder.Exists)
                {
                    WriteVerbose($"Found folder: {folder.Name}");
                    return (false, folder.Name);
                }
            }
            catch (Exception ex)
            {
                WriteVerbose($"Exception when checking as folder: {ex.Message}");
            }

            // Fallback: if the URL didn't work due to trailing slash difference, try toggling it
            if (!decodedServerRelative.EndsWith("/", StringComparison.Ordinal))
            {
                var alt = decodedServerRelative + "/";
                try
                {
                    WriteVerbose($"Retry as folder with trailing slash: {alt}");
                    var altFolder = context.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(alt));
                    context.Load(altFolder, f => f.Name, f => f.Exists);
                    context.ExecuteQueryRetry();
                    if (altFolder.Exists)
                    {
                        WriteVerbose($"Found folder (alt): {altFolder.Name}");
                        return (false, altFolder.Name);
                    }
                }
                catch (Exception ex)
                {
                    WriteVerbose($"Alt folder check failed: {ex.Message}");
                }
            }

            WriteVerbose("Neither file nor folder found");
            return (false, null);
        }

        private string ProcessUrl(string url, string webServerRelativeUrl)
        {
            WriteVerbose($"ProcessUrl input: url='{url}', webServerRelativeUrl='{webServerRelativeUrl}'");
            
            // Handle absolute URLs first
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                // Extract just the path and query from the absolute URL
                url = uri.AbsolutePath + uri.Query;
                WriteVerbose($"After absolute URL conversion: '{url}'");
            }
            
            // If we're connected at tenant level (webServerRelativeUrl = '/') or the URL is already server-relative,
            // just return the server-relative URL as-is
            if (webServerRelativeUrl == "/" || url.StartsWith("/"))
            {
                WriteVerbose($"Returning server-relative URL: '{url}'");
                return url;
            }
            
            // If it doesn't start with /, make it relative to current web
            var result = UrlUtility.Combine(webServerRelativeUrl, url);
            WriteVerbose($"Made relative to current web: '{result}'");
            return result;
        }

        private ClientContext GetSourceContext()
        {
            if (SourceConnection != null)
            {
                WriteVerbose("Using provided SourceConnection");
                return SourceConnection.Context;
            }
            WriteVerbose("Using current connection for source");
            
            // Check if we have a current connection
            try
            {
                return ClientContext;
            }
            catch
            {
                return null;
            }
        }

        private ClientContext GetTargetContext()
        {
            if (TargetConnection != null)
            {
                WriteVerbose("Using provided TargetConnection");
                return TargetConnection.Context;
            }
            WriteVerbose("Using current connection for target");
            
            // Check if we have a current connection
            try
            {
                return ClientContext;
            }
            catch
            {
                return null;
            }
        }

        private enum SyncResult
        {
            Success,
            Skipped,
            Error
        }

        private FieldUserValue MapUserToTarget(FieldUserValue sourceUser, ClientContext sourceContext, ClientContext targetContext)
        {
            if (sourceUser == null || sourceUser.LookupId <= 0) return null;

            string email = null;
            string login = null;
            try
            {
                var su = sourceContext.Web.GetUserById(sourceUser.LookupId);
                sourceContext.Load(su, u => u.Email, u => u.LoginName);
                sourceContext.ExecuteQueryRetry();
                email = su.Email;
                login = su.LoginName;
            }
            catch (Exception ex)
            {
                WriteVerbose($"MapUserToTarget: failed to load source user {sourceUser.LookupId}: {ex.Message}");
            }

            // Prefer email if available; fallback to login name
            string[] identities = string.IsNullOrEmpty(email)
                ? new[] { login }
                : (string.IsNullOrEmpty(login) ? new[] { email } : new[] { email, login });

            foreach (var identity in identities)
            {
                if (string.IsNullOrWhiteSpace(identity)) continue;
                try
                {
                    if (!_targetUserCache.TryGetValue(identity, out int id))
                    {
                        var ensured = targetContext.Web.EnsureUser(identity);
                        targetContext.Load(ensured, u => u.Id);
                        targetContext.ExecuteQueryRetry();
                        id = ensured.Id;
                        _targetUserCache[identity] = id;
                    }

                    if (id > 0)
                    {
                        return new FieldUserValue { LookupId = id };
                    }
                }
                catch (Exception ex)
                {
                    WriteVerbose($"MapUserToTarget: EnsureUser failed for '{identity}': {ex.Message}");
                    // Try next identity
                }
            }
            WriteWarning($"MapUserToTarget: could not map source user id {sourceUser.LookupId}. Leaving target value unchanged.");
            return null;
        }
    }
}
