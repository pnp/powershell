using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Copy, "PnPFileMetadata")]
    public class CopyFileMetadata : PnPWebCmdlet
    {
        // Batch + cache management
        private const int FileLoadBatchSize = 200;
        private const int UpdateFlushSize = 100;
        private readonly Dictionary<int, string[]> _sourceUserIdentityCache = new();
        private readonly Dictionary<string, int> _targetUserIdByIdentity = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<int> _unmappedSourceUsersWarned = new();

        private static readonly HashSet<string> SystemDateFieldsSet = new(["Created", "Modified"]);

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl")]
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        [Alias("TargetServerRelativeUrl")]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public string[] Fields = ["Author", "Editor", "Created", "Modified"];

        [Parameter(Mandatory = false)]
        public SwitchParameter Recursive = SwitchParameter.Present;

        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used for accessing the source file. If not provided, uses the current connection.")]
        public PnPConnection SourceConnection = null;

        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used for accessing the target file. If not provided, uses the current connection.")]
        public PnPConnection TargetConnection = null;

        protected override void ExecuteCmdlet()
        {
            // Marshal inputs
            Fields = OrganizeFields();

            // Get the contexts for source and target operations
            var sourceContext = SourceConnection?.Context ?? ClientContext;
            var targetContext = TargetConnection?.Context ?? ClientContext;
            SyncResultCount resultTotals;

            // Ensure web URLs are loaded before using them
            var sourceWebServerRelativeUrl = sourceContext.Web.EnsureProperty(w => w.ServerRelativeUrl);
            var targetWebServerRelativeUrl = targetContext.Web.EnsureProperty(w => w.ServerRelativeUrl);

            SourceUrl = GetServerRelativePath(SourceUrl, sourceWebServerRelativeUrl);
            TargetUrl = GetServerRelativePath(TargetUrl, targetWebServerRelativeUrl);

            if (Force || ShouldContinue(string.Format("Synchronize metadata from '{0}' to '{1}'. Recursion: {2}", SourceUrl, TargetUrl, Recursive), Resources.Confirm))
            {
                try
                {
                    WriteVerbose($"Syncing.");
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

                    if (sourceItem.IsFile)
                    {
                        var result = SyncFileMetadata(SourceUrl, TargetUrl, sourceContext, targetContext);
                        resultTotals = new SyncResultCount() { processed = 0, skipped = 0, errored = 0 };
                        addResultToCount(result, ref resultTotals);
                    }
                    else
                    {
                        resultTotals = SyncFolderMetadata(SourceUrl, TargetUrl, sourceContext, targetContext);
                    }

                    WriteObject($"Metadata synchronization completed. Processed: {resultTotals.processed}, Skipped: {resultTotals.skipped}, Errors: {resultTotals.errored}");
                }
                catch (Exception)
                {
                    WriteError(new ErrorRecord(new InvalidOperationException("Metadata synchronization failed."), "MetadataSyncError", ErrorCategory.InvalidOperation, SourceUrl));
                }
            }
        }

        private string[] OrganizeFields()
        {
            // Normalize, distinct (case-insensitive), and order system date fields last
            var normalized = (Fields ?? Array.Empty<string>())
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(f => SystemDateFieldsSet.Contains(f) ? 1 : 0)
                .ToArray();
            return normalized;
        }

        private SyncResultCount SyncFolderMetadata(string sourceFolderUrl, string targetFolderUrl, ClientContext sourceContext, ClientContext targetContext)
        {
            var resultCount = new SyncResultCount() { processed = 0, skipped = 0, errored = 0 };

            try
            {
                // Get source and target folder via ResourcePath and validate existence
                var sourceFolder = sourceContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Uri.UnescapeDataString(sourceFolderUrl)));
                var targetFolder = targetContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Uri.UnescapeDataString(targetFolderUrl)));

                // Confirm existence
                sourceContext.Load(sourceFolder, f => f.Name, f => f.Exists);
                targetContext.Load(targetFolder, f => f.Name, f => f.Exists);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                if (!sourceFolder.Exists)
                {
                    WriteWarning($"Source folder not found: '{sourceFolderUrl}'");
                    resultCount.errored++;
                    return resultCount;
                }

                if (!targetFolder.Exists)
                {
                    WriteWarning($"Target folder not found, skipping subtree: '{targetFolderUrl}'");
                    resultCount.skipped++;
                    return resultCount;
                }

                // Progress: folder being processed
                WriteVerbose($"Folder: '{sourceFolderUrl}' -> '{targetFolderUrl}'");

                // Load folder children (avoid loading all list item fields)
                sourceContext.Load(sourceFolder, f => f.Files, f => f.Folders);
                targetContext.Load(targetFolder, f => f.Files, f => f.Folders);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                // Progress: child counts
                WriteVerbose($" - Contains: {sourceFolder.Files?.Count ?? 0} files, {sourceFolder.Folders?.Count ?? 0} folders");

                // For the folder list items, preload only the needed fields (fetch the list items explicitly)
                var folderNeededFields = new HashSet<string>(Fields, StringComparer.OrdinalIgnoreCase);
                LoadListItemFields(sourceContext, sourceFolder.ListItemAllFields, folderNeededFields);
                LoadListItemFields(targetContext, targetFolder.ListItemAllFields, folderNeededFields);
                sourceContext.ExecuteQueryRetry();
                targetContext.ExecuteQueryRetry();

                // Sync folder metadata (targeted fields already loaded)
                WriteVerbose($" - Syncing folder metadata for '{sourceFolder.Name}'");
                var folderSync = SyncListItemMetadata(sourceFolder.ListItemAllFields, targetFolder.ListItemAllFields, sourceContext, targetContext, skipSourceLoad: true, itemLabel: sourceFolder.Name);
                addResultToCount(folderSync, ref resultCount);

                // When Recursive is enabled, process files and subfolders.
                if (Recursive)
                {
                    // Determine required fields: always Created/Modified
                    var neededFields = new HashSet<string>(Fields, StringComparer.OrdinalIgnoreCase);
                    // Batch-load only needed fields for source and target files
                    PreloadFilesWithFields(sourceContext, sourceFolder.Files, neededFields);
                    PreloadFilesWithFields(targetContext, targetFolder.Files, neededFields);

                    // Process files in the current folder
                    if (sourceFolder.Files != null && sourceFolder.Files.Count > 0)
                    {
                        Dictionary<string, File> targetByName = targetFolder.Files.ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase);

                        var pendingChanges = false;
                        var pendingChangesCount = 0;
                        foreach (var sourceFile in sourceFolder.Files)
                        {
                            WriteVerbose($"   File: {sourceFile.Name}");
                            if (!targetByName.TryGetValue(sourceFile.Name, out var targetFileForName))
                            {
                                var missing = UrlUtility.Combine(targetFolderUrl, sourceFile.Name);
                                WriteWarning($"Target file not found, skipping: '{missing}'");
                                resultCount.skipped++;
                                continue;
                            }

                            var sItem = sourceFile.ListItemAllFields;
                            var tItem = targetFileForName.ListItemAllFields;
                            var r = SyncListItemMetadata(sItem, tItem, sourceContext, targetContext, skipSourceLoad: true, executeImmediately: false, itemLabel: sourceFile.Name);
                            addResultToCount(r, ref resultCount);
                            if (r == SyncResult.Success)
                            {
                                pendingChanges = true;
                                pendingChangesCount++;
                                if (pendingChangesCount >= UpdateFlushSize)
                                {
                                    targetContext.ExecuteQueryRetry();
                                    WriteVerbose($"Flushed {pendingChangesCount} pending updates in folder '{targetFolderUrl}'.");
                                    pendingChanges = false;
                                    pendingChangesCount = 0;
                                }
                            }
                        }

                        if (pendingChanges)
                        {
                            targetContext.ExecuteQueryRetry();
                            WriteVerbose($"Flushed {pendingChangesCount} pending updates in folder '{targetFolderUrl}'.");
                            pendingChanges = false;
                            pendingChangesCount = 0;
                        }
                    }

                    // Process subfolders recursively
                    foreach (var sourceSubfolder in sourceFolder.Folders)
                    {
                        if (sourceSubfolder.Name.StartsWith("_")) continue; // Skip system folders
                        WriteVerbose($"   Folder: {sourceSubfolder.Name}");
                        var sourceSubfolderUrlCombined = UrlUtility.Combine(sourceFolderUrl, sourceSubfolder.Name);
                        var targetSubfolderUrl = UrlUtility.Combine(targetFolderUrl, sourceSubfolder.Name);
                        var subResults = SyncFolderMetadata(sourceSubfolderUrlCombined, targetSubfolderUrl, sourceContext, targetContext);

                        resultCount.processed += subResults.processed;
                        resultCount.skipped += subResults.skipped;
                        resultCount.errored += subResults.errored;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteWarning($"Failed to process folder '{sourceFolderUrl}': {ex.Message}");
                resultCount.errored++;
            }

            return resultCount;
        }


        private SyncResult SyncFileMetadata(string sourceFileUrl, string targetFileUrl, ClientContext sourceContext, ClientContext targetContext)
        {
            try
            {
                // Sync file metadata
                // Decode and use server-relative ResourcePath to reliably check existence
                var decodedSourceUrl = Uri.UnescapeDataString(sourceFileUrl);
                var decodedTargetUrl = Uri.UnescapeDataString(targetFileUrl);

                WriteVerbose($"File: '{decodedSourceUrl}' -> '{decodedTargetUrl}'");

                var sourceFile = sourceContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedSourceUrl));
                var targetFile = targetContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedTargetUrl));

                sourceContext.Load(sourceFile, f => f.Exists, f => f.Name);
                targetContext.Load(targetFile, f => f.Exists, f => f.Name);

                // Preload only the required fields on both list items prior to reading values
                var neededFields = new HashSet<string>(Fields, StringComparer.OrdinalIgnoreCase);
                var sourceFileItem = sourceFile.ListItemAllFields;
                var targetFileItem = targetFile.ListItemAllFields;
                LoadListItemFields(sourceContext, sourceFileItem, neededFields);
                LoadListItemFields(targetContext, targetFileItem, neededFields);

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

                return SyncListItemMetadata(sourceFileItem, targetFileItem, sourceContext, targetContext, skipSourceLoad: true);
            }
            catch (Exception ex)
            {
                WriteWarning($"Failed to sync file metadata from '{sourceFileUrl}' to '{targetFileUrl}': {ex.Message}");
                return SyncResult.Error;
            }
        }

        private SyncResult SyncListItemMetadata(ListItem sourceItem, ListItem targetItem, ClientContext sourceContext, ClientContext targetContext, bool skipSourceLoad = false, bool executeImmediately = true, string itemLabel = null)
        {
            try
            {
                if (!skipSourceLoad)
                {
                    sourceContext.Load(sourceItem);
                    sourceContext.ExecuteQueryRetry();
                }

                var metadataUpdated = false; // track if we changed anything
                var labelPrefix = string.IsNullOrEmpty(itemLabel) ? string.Empty : ($"[{itemLabel}] ");
                var valuesToSet = PrepareItemValues(sourceItem, targetItem, sourceContext, targetContext, Fields, labelPrefix, ref metadataUpdated);

                if (!metadataUpdated)
                {
                    return SyncResult.Skipped;
                }

                try
                {
                    targetItem.SetFieldValues(valuesToSet, this);
                    targetItem.UpdateOverwriteVersion();
                    if (executeImmediately)
                    {
                        targetContext.ExecuteQueryRetry();
                    }
                    return SyncResult.Success;
                }
                catch (Exception ex) when (ex.Message.IndexOf("user", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Fallback: if setting user fields caused an error, retry without Author/Editor
                    var hadAuthor = valuesToSet.ContainsKey("Author");
                    var hadEditor = valuesToSet.ContainsKey("Editor");
                    if (hadAuthor) valuesToSet.Remove("Author");
                    if (hadEditor) valuesToSet.Remove("Editor");

                    if (valuesToSet.Count == 0)
                    {
                        WriteWarning($"{labelPrefix}Failed to sync list item metadata due to user mapping; no other changes to apply. {ex.Message}");
                        return SyncResult.Skipped;
                    }

                    try
                    {
                        targetItem.SetFieldValues(valuesToSet, this);
                        targetItem.UpdateOverwriteVersion();
                        if (executeImmediately)
                        {
                            targetContext.ExecuteQueryRetry();
                        }
                        WriteVerbose($"{labelPrefix}Applied non-user fields after user mapping failure.");
                        return SyncResult.Success;
                    }
                    catch (Exception ex2)
                    {
                        WriteWarning($"{labelPrefix}Failed to sync list item metadata after removing user fields: {ex2.Message}");
                        return SyncResult.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                var labelPrefix = string.IsNullOrEmpty(itemLabel) ? string.Empty : ($"[{itemLabel}] ");
                WriteWarning($"{labelPrefix}Failed to sync list item metadata: {ex.Message}");
                return SyncResult.Error;
            }
        }


        private enum SyncResult { Success, Skipped, Error }

        protected struct SyncResultCount { public int processed, skipped, errored; }

        private void addResultToCount(SyncResult result, ref SyncResultCount resultCount)
        {
            switch (result)
            {
                case SyncResult.Success: resultCount.processed++; break;
                case SyncResult.Skipped: resultCount.skipped++; break;
                case SyncResult.Error: resultCount.errored++; break;
            }
        }


        private Hashtable PrepareItemValues(ListItem sourceItem, ListItem targetItem, ClientContext sourceContext, ClientContext targetContext, string[] fields, string labelPrefix, ref bool metadataUpdated)
        {
            var valuesToSet = new Hashtable(StringComparer.OrdinalIgnoreCase);

            foreach (var fieldName in fields)
            {
                try
                {
                    if (sourceItem.FieldValues.ContainsKey(fieldName) && sourceItem.FieldValues[fieldName] != null)
                    {
                        var sourceValue = sourceItem.FieldValues[fieldName];

                        if (string.Equals(fieldName, "Author", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(fieldName, "Editor", StringComparison.OrdinalIgnoreCase))
                        {
                            if (sourceValue is FieldUserValue userValue && userValue.LookupId > 0)
                            {
                                var mapped = MapUserToTarget(userValue, sourceContext, targetContext);
                                if (mapped != null)
                                {
                                    var current = targetItem[fieldName] as FieldUserValue;
                                    if (current == null || current.LookupId != mapped.LookupId)
                                    {
                                        valuesToSet[fieldName] = mapped; // set FieldUserValue directly
                                        metadataUpdated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var current = targetItem.FieldValues.ContainsKey(fieldName) ? targetItem[fieldName] : null;
                            if (!ValuesEqual(current, sourceValue))
                            {
                                valuesToSet[fieldName] = sourceValue;
                                metadataUpdated = true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    WriteWarning($"{labelPrefix}Failed to prepare field '{fieldName}'.");
                }
            }

            return valuesToSet;
        }


        private void SetSystemDateFields(ListItem sourceItem, ListItem targetItem, string labelPrefix, ref bool metadataUpdated)
        {
            try
            {
                if (sourceItem.FieldValues.ContainsKey("Created") && sourceItem.FieldValues["Created"] is DateTime cdt2)
                {
                    var curCreated = targetItem.FieldValues.ContainsKey("Created") ? targetItem["Created"] as DateTime? : null;
                    if (!curCreated.HasValue || curCreated.Value != cdt2)
                    {
                        targetItem["Created"] = cdt2;
                        metadataUpdated = true;
                    }
                }
                if (sourceItem.FieldValues.ContainsKey("Modified") && sourceItem.FieldValues["Modified"] is DateTime mdt2)
                {
                    var curModified = targetItem.FieldValues.ContainsKey("Modified") ? targetItem["Modified"] as DateTime? : null;
                    if (!curModified.HasValue || curModified.Value != mdt2)
                    {
                        targetItem["Modified"] = mdt2;
                        metadataUpdated = true;
                    }
                }
            }
            catch (Exception)
            {
                WriteWarning($"{labelPrefix}Failed to prepare Created/Modified.");
            }
        }

        private static bool ValuesEqual(object a, object b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a == null || b == null) return false;

            if (a is DateTime adt && b is DateTime bdt)
            {
                return adt == bdt;
            }

            if (a is FieldUserValue au && b is FieldUserValue bu)
            {
                return au.LookupId == bu.LookupId;
            }

            return object.Equals(a, b);
        }

        private (bool IsFile, string Name) GetFileOrFolderInfo(string url, ClientContext context)
        {
            var webServerRelativeUrl = context.Web.EnsureProperty(w => w.ServerRelativeUrl);
            var serverRelativeUrl = GetServerRelativePath(url, webServerRelativeUrl);
            var decodedServerRelative = Uri.UnescapeDataString(serverRelativeUrl);

            // Try as file first using GetFileByServerRelativePath
            try
            {
                var file = context.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(decodedServerRelative));
                context.Load(file, f => f.Name, f => f.Exists);
                context.ExecuteQueryRetry();
                if (file.Exists) return (true, file.Name);
            }
            catch (Exception) { } // keep trying

            // try as folder
            try
            {
                var folder = context.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(decodedServerRelative));
                context.Load(folder, f => f.Name, f => f.Exists);
                context.ExecuteQueryRetry();
                if (folder.Exists) return (false, folder.Name);
            }
            catch (Exception) { } // keep trying

            // add a trailing slash and try as folder again
            if (!decodedServerRelative.EndsWith("/", StringComparison.Ordinal))
            {
                var alt = decodedServerRelative + "/";
                try
                {
                    // retry as folder with trailing slash
                    var altFolder = context.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(alt));
                    context.Load(altFolder, f => f.Name, f => f.Exists);
                    context.ExecuteQueryRetry();
                    if (altFolder.Exists) return (false, altFolder.Name);
                }
                catch (Exception) { } // failover
            }

            return (false, null); // neither file nor folder found
        }

        private static string GetServerRelativePath(string url, string webServerRelativeUrl)
        {
            if (url.StartsWith('/')) return url;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                return uri.AbsolutePath + uri.Query;
            }
            return UrlUtility.Combine(webServerRelativeUrl, url);
        }



        private FieldUserValue MapUserToTarget(FieldUserValue sourceUser, ClientContext sourceContext, ClientContext targetContext)
        {
            try
            {
                if (sourceUser == null || sourceUser.LookupId <= 0) return null;

                if (ReferenceEquals(sourceContext, targetContext))
                {
                    // when both contexts refer to the same site, LookupId will match
                    return new FieldUserValue { LookupId = sourceUser.LookupId };
                }

                // Obtain source identities (email/login) from cache or load
                string email = null;
                string login = null;
                if (!_sourceUserIdentityCache.TryGetValue(sourceUser.LookupId, out var identities))
                {
                    try
                    {
                        var su = sourceContext.Web.GetUserById(sourceUser.LookupId);
                        sourceContext.Load(su, u => u.Email, u => u.LoginName);
                        sourceContext.ExecuteQueryRetry();
                        email = su.Email;
                        login = su.LoginName;
                        _sourceUserIdentityCache[sourceUser.LookupId] = [email, login];
                    }
                    catch (Exception)
                    {
                        // source user load failed
                    }
                }
                else
                {
                    if (identities != null)
                    {
                        if (identities.Length > 0) email = identities[0];
                        if (identities.Length > 1) login = identities[1];
                    }
                }

                // Try email first, then login
                var candidates = new System.Collections.Generic.List<string>();
                if (!string.IsNullOrEmpty(email)) candidates.Add(email);
                if (!string.IsNullOrEmpty(login)) candidates.Add(login);

                foreach (var identity in candidates)
                {
                    if (string.IsNullOrWhiteSpace(identity)) continue;
                    try
                    {
                        if (!_targetUserIdByIdentity.TryGetValue(identity, out int id))
                        {
                            var ensured = targetContext.Web.EnsureUser(identity);
                            targetContext.Load(ensured, u => u.Id);
                            targetContext.ExecuteQueryRetry();
                            id = ensured.Id;
                            _targetUserIdByIdentity[identity] = id;
                        }
                        if (id > 0)
                        {
                            return new FieldUserValue { LookupId = id };
                        }
                        // id <= 0 indicates previous failure; skip retry
                        continue;
                    }
                    catch (Exception)
                    {
                        // EnsureUser failed for identity; store negative cache to avoid repeated retries
                        _targetUserIdByIdentity[identity] = -1;
                    }
                }

                if (_unmappedSourceUsersWarned.Add(sourceUser.LookupId))
                {
                    WriteWarning($"MapUserToTarget: could not map source user id {sourceUser.LookupId}. Leaving target value unchanged.");
                }
                return null;
            }
            catch (Exception)
            {
                // map user unexpected error
                return null;
            }
        }

        private void PreloadFilesWithFields(ClientContext context, Microsoft.SharePoint.Client.FileCollection files, ISet<string> fieldNames)
        {
            if (files == null || files.Count == 0) return;

            int idx = 0;
            foreach (var f in files)
            {
                context.Load(f, x => x.Name);
                foreach (var nf in fieldNames)
                {
                    context.Load(f.ListItemAllFields, i => i[nf]);
                }
                idx++;
                if (idx % FileLoadBatchSize == 0) context.ExecuteQueryRetry();
            }

            if (idx % FileLoadBatchSize != 0) context.ExecuteQueryRetry();
        }

        // Helper: queue loads for specific fields on a ListItem to avoid loading all fields
        private static void LoadListItemFields(ClientContext context, ListItem item, ISet<string> fieldNames)
        {
            if (item == null || fieldNames == null || fieldNames.Count == 0) return;
            foreach (var nf in fieldNames)
            {
                context.Load(item, i => i[nf]);
            }
        }
    }
}
