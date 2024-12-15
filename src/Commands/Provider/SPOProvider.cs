﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Client;
using PnP.Framework.Diagnostics;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Provider.Parameters;
using PnP.PowerShell.Commands.Provider.SPOProxy;
using File = Microsoft.SharePoint.Client.File;
using PnPResources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Provider
{
    [CmdletProvider(PSProviderName, ProviderCapabilities.ShouldProcess)]
    public class SPOProvider : NavigationCmdletProvider, IContentCmdletProvider
    {
        //Constants
        public const string PSProviderName = "SharePoint";

        //Private properties
        private const string Pattern = @"^[\\w\\d\\.\\s]*$";
        private const string PathSeparator = "/";
        private const int DefaultItemCacheTimeout = 1000; //1 second default caching of file/folder object
        private const int DefaultWebCacheTimeout = 1000 * 60 * 10; //10 minutes default caching of web object

        //Init
        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
#if DEBUG
            SessionState.PSVariable.Set("WebRequestCounter", 0);
#endif
            return base.Start(providerInfo);
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            Log.Debug("SPOProvider","NewDrive (Drive.Name = ’{drive.Name}’, Drive.Root = ’{drive.Root}’)");

            var spoParameters = DynamicParameters as SPODriveParameters;
            Web web = null;

            if (spoParameters?.Context != null)
            {
                var webUrl = spoParameters.Url ?? spoParameters.Context.Url;
                web = spoParameters.Context.Clone(webUrl).Web;
            }
            else if (spoParameters?.Web != null)
            {
                var webUrl = spoParameters.Url ?? spoParameters.Web.EnsureProperty(w => w.Url);
                web = spoParameters.Web.Context.Clone(webUrl).Web;
            }
            else if (PnPConnection.Current != null)
            {
                var webUrl = spoParameters?.Url ?? PnPConnection.Current.Context.Web.EnsureProperty(w => w.Url);
                web = PnPConnection.Current.Context.Clone(webUrl).Web;
            }
            else
            {
                WriteErrorInternal(PnPResources.NoDefaultSharePointConnection, drive.Root, ErrorCategory.ConnectionError);
            }

            if (web != null)
            {
#if DEBUG
                SetCounter(web.Context);
#endif
                var itemTimeout = (spoParameters != null && spoParameters.ItemCacheTimeout != default(int)) ? spoParameters.ItemCacheTimeout : DefaultItemCacheTimeout;
                var webTimeout = (spoParameters != null && spoParameters.WebCacheTimeout != default(int)) ? spoParameters.WebCacheTimeout : DefaultWebCacheTimeout;
                var normalizePath = NormalizePath(IsPropertyAvailable(web, "ServerRelativeUrl") ? web.ServerRelativeUrl : web.EnsureProperty(w => w.ServerRelativeUrl));

                //Set root to host root and current location to normalized path
                var normalizedDrive = new SPODriveInfo(new PSDriveInfo(drive.Name, drive.Provider, "/", drive.Description, drive.Credential, drive.DisplayRoot))
                {
                    Web = web,
                    ItemTimeout = itemTimeout,
                    WebTimeout = webTimeout,
                    CurrentLocation = normalizePath
                };

                //Add web to cache
                normalizedDrive.CachedWebs.Add(new SPODriveCacheWeb
                {
                    Web = normalizedDrive.Web,
                    LastRefresh = DateTime.Now,
                    Path = normalizePath

                });

                //Add proxy aliases
                if (spoParameters == null || !spoParameters.NoProxyCmdLets)
                {
                    SPOProxyImplementation.AddAlias(SessionState);

                }
                return normalizedDrive;
            }
            return null;
        }

        protected override object NewDriveDynamicParameters()
        {
            return new SPODriveParameters();
        }

        protected override PSDriveInfo RemoveDrive(PSDriveInfo drive)
        {
            Log.Debug("SPOProvider","RemoveDrive (Drive.Name = ’{drive.Name}’)");

            var spoDrive = drive as SPODriveInfo;
            if (spoDrive == null) return null;

            //Remove proxy aliases
            if (spoDrive.Provider.Drives.Count < 2)
            {
                SPOProxyImplementation.RemoveAlias(SessionState);
            }

            spoDrive.ClearState();
            return spoDrive;
        }

        //Validate
        protected override bool IsValidPath(string path)
        {
            return Regex.IsMatch(path, Pattern);
        }

        protected override bool IsItemContainer(string path)
        {
            try
            {
                if (IsPathDrive(path)) return true;
                return GetFileOrFolder(path, false) is Folder;
            }
            catch (Exception e)
            {
                WriteErrorInternal(e.Message, path, exception: e);
                return false;
            }
        }

        protected override bool ItemExists(string path)
        {
            if (IsPathDrive(path)) return true;
            try
            {
                if (path.EndsWith("*")) return false;
                return GetFileOrFolder(path, false) != null;
            }
            catch (Exception e)
            {
                WriteErrorInternal(e.Message, path, exception: e);
                return false;
            }
        }

        protected override bool HasChildItems(string path)
        {
            try
            {
                var folder = GetFileOrFolder(path) as Folder;
                if (folder != null)
                {
                    var folderAndFiles = GetFolderItems(folder);
                    return folderAndFiles.Any();
                }
                return false;
            }
            catch (Exception e)
            {
                WriteErrorInternal(e.Message, path, exception: e);
                return false;
            }
        }

        //Path
        protected override string MakePath(string parent, string child)
        {
            var result = base.MakePath(parent, child);
            return result;
        }

        protected override string GetParentPath(string path, string root)
        {
            var result = base.GetParentPath(path, root);
            return result;
        }

        protected override string GetChildName(string path)
        {
            var result = base.GetChildName(path);
            return result;
        }

        protected override string NormalizeRelativePath(string path, string basePath)
        {
            var result = base.NormalizeRelativePath(path, basePath);
            return result;
        }

        //Get
        protected override void GetItem(string path)
        {
            var obj = GetFileOrFolder(path);
            if (obj != null)
            {
                WriteItemObject(obj, GetServerRelativePath(path), (obj is Folder));
            }
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            var folder = GetFileOrFolder(path) as Folder;
            if (folder != null)
            {

                //Get current spo-drive
                var spoDrive = GetCurrentDrive(path);
                if (spoDrive == null) return;

                //Save original timeout
                var originalItemTimeout = spoDrive.ItemTimeout;

                //Set timeout temporary to 5 minutes
                spoDrive.ItemTimeout = 1000 * 60 * 5;

                //Get data
                var spoParameters = DynamicParameters as SPOChildItemsParameters;
                var folderAndFiles = spoParameters != null && spoParameters.Limit != SPOChildItemsParameters.Limits.Default ? GetFolderItems(folder, false, (int)spoParameters.Limit).ToArray() : GetFolderItems(folder).ToArray();

                //Output result
                folderAndFiles.OfType<Folder>().ToList().ForEach(subFolder => WriteItemObject(subFolder, subFolder.ServerRelativeUrl, true));
                folderAndFiles.OfType<File>().ToList().ForEach(file => WriteItemObject(file, file.ServerRelativeUrl, false));

                //Restore item cache timeout
                spoDrive.ItemTimeout = originalItemTimeout;

                //Iterate sub folders
                if (recurse)
                {
                    folderAndFiles.OfType<Folder>().ToList().ForEach(subFolder => GetChildItems(subFolder.ServerRelativeUrl, true));
                }
            }
            else
            {
                WriteErrorInternal("No folder at end of path", path, ErrorCategory.InvalidOperation);
            }
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            return new SPOChildItemsParameters();
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            var folder = GetFileOrFolder(path) as Folder;
            if (folder != null)
            {

                //Get current spo-drive
                var spoDrive = GetCurrentDrive(path);
                if (spoDrive == null) return;

                //Save original timeout
                var originalItemTimeout = spoDrive.ItemTimeout;

                //Set timeout temporary to 5 minutes
                spoDrive.ItemTimeout = 1000 * 60 * 5;

                //Get data
                var spoParameters = DynamicParameters as SPOChildItemsParameters;
                var folderAndFiles = spoParameters != null && spoParameters.Limit != SPOChildItemsParameters.Limits.Default ? GetFolderItems(folder, false, (int)spoParameters.Limit).ToArray() : GetFolderItems(folder).ToArray();
                var serverRelativePath = GetServerRelativePath(path);

                foreach (var subFolder in folderAndFiles.OfType<Folder>().ToList())
                {
                    string name;
                    if (string.IsNullOrEmpty(subFolder.Name))
                    {
                        var serverRelativeUrl = subFolder.EnsureProperty(s => s.ServerRelativeUrl);
                        name = serverRelativeUrl.Split(PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last();
                    }
                    else
                    {
                        name = subFolder.Name;
                    }
                    WriteItemObject(name, serverRelativePath, true);
                }

                foreach (var file in folderAndFiles.OfType<File>().ToList())
                {
                    WriteItemObject(file.Name, serverRelativePath, false);
                }

                //Restore item cache timeout
                spoDrive.ItemTimeout = originalItemTimeout;
            }
            else
            {
                WriteErrorInternal("No folder at end of path", path, ErrorCategory.InvalidOperation);
            }
        }

        protected override object GetChildNamesDynamicParameters(string path)
        {
            return new SPOChildItemsParameters();
        }

        //Set
        protected override void CopyItem(string path, string copyPath, bool recurse)
        {
            CopyMoveImplementation(path, copyPath, recurse);
        }

        protected override void MoveItem(string path, string destination)
        {
            CopyMoveImplementation(path, destination, true, false);
        }

        protected override void RenameItem(string path, string newName)
        {
            if (newName.Contains(PathSeparator))
            {
                var msg = "Name can not contain path separator";
                var err = new ArgumentException(msg);
                WriteErrorInternal(msg, path, ErrorCategory.InvalidArgument, true, err);
                return;
            }
            CopyMoveImplementation(path, GetParentServerRelativePath(path) + PathSeparator + newName, true, false);
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            var obj = GetFileOrFolder(path) as ClientObject;

            if (obj != null && ShouldProcess(GetServerRelativePath(path), $"{((Force) ? "Delete" : "Recycle")} {obj.GetType().Name}"))
            {
                if (obj is File)
                {
                    var file = obj as File;
                    if (Force)
                    {
                        file.DeleteObject();
                    }
                    else
                    {
                        file.Recycle();
                    }
                    file.Context.ExecuteQueryRetry();
                }
                else if (obj is Folder)
                {
                    var folder = obj as Folder;
                    if (Force)
                    {
                        folder.DeleteObject();
                    }
                    else
                    {
                        folder.Recycle();
                    }
                    folder.Context.ExecuteQueryRetry();
                }
            }
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            var serverRelativePath = GetServerRelativePath(path);
            var web = FindWebInPath(serverRelativePath);
            var webRelativePath = Regex.Replace(serverRelativePath, $@"^{web.EnsureProperty((w => w.ServerRelativeUrl))}", string.Empty, RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(webRelativePath)) webRelativePath = PathSeparator;

            if (string.IsNullOrEmpty(itemTypeName)) itemTypeName = "File";

            if (itemTypeName.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                var pathParts = webRelativePath.Split(@"\/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var parentFolder = web.EnsureFolder(web.RootFolder, string.Join(PathSeparator, pathParts.Take(pathParts.Length - 1)));

                var fileCreationInfo = new FileCreationInformation
                {
                    Url = serverRelativePath,
                    Overwrite = Force
                };

                if (newItemValue is string)
                {
                    fileCreationInfo.Content = System.Text.Encoding.UTF8.GetBytes((string)newItemValue);
                }
                else if (newItemValue is byte[])
                {
                    fileCreationInfo.Content = (byte[])newItemValue;
                }
                else if (newItemValue is Stream)
                {
                    fileCreationInfo.ContentStream = (Stream)newItemValue;
                }
                else
                {
                    fileCreationInfo.Content = new byte[0];
                }

                var file = parentFolder.Files.Add(fileCreationInfo);
                parentFolder.Context.Load(file);
                parentFolder.Context.ExecuteQueryRetry();
                SetCachedItem(serverRelativePath, file);

                WriteItemObject(file, path, false);

            }
            else if (itemTypeName.Equals("folder", StringComparison.InvariantCultureIgnoreCase) || itemTypeName.Equals("directory", StringComparison.InvariantCultureIgnoreCase))
            {
                var folder = web.EnsureFolder(web.RootFolder, webRelativePath);
                SetCachedItem(serverRelativePath, folder);

                WriteItemObject(folder, path, true);
            }
            else
            {
                WriteErrorInternal("Only File or Folder (Directory) supported for Type", path, ErrorCategory.InvalidArgument);
            }
        }

        //Content
        public IContentReader GetContentReader(string path)
        {
            var obj = GetFileOrFolder(path);
            if (obj is Folder)
            {
                WriteErrorInternal("Directories have no content", path, ErrorCategory.InvalidOperation);
            }
            if (obj is File)
            {
                var file = obj as File;
                var isString = false;
                var contentParameters = DynamicParameters as SPOContentParameters;
                if (contentParameters != null)
                {
                    isString = contentParameters.IsBinary;
                }
                return new SPOContentReaderWriter(file, isString);
            }

            return null;
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            return new SPOContentParameters();
        }

        public IContentWriter GetContentWriter(string path)
        {
            var obj = GetFileOrFolder(path, false);
            if (obj is Folder)
            {
                WriteErrorInternal("Directories have no content", path, ErrorCategory.InvalidOperation);
            }
            if (obj == null)
            {
                NewItem(path, "File", null);
                obj = GetFileOrFolder(path, useCache: false);
            }
            if (obj is File)
            {
                var file = obj as File;
                var isBinary = false;
                var contentParameters = DynamicParameters as SPOContentParameters;
                if (contentParameters != null)
                {
                    isBinary = contentParameters.IsBinary;
                }

                if (ShouldProcess($"Set content in {GetServerRelativePath(path)}"))
                {
                    return new SPOContentReaderWriter(file, isBinary);
                }
            }

            return null;
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            return new SPOContentParameters();
        }

        public void ClearContent(string path)
        {
            var obj = GetFileOrFolder(path);
            if (obj is Folder)
            {
                WriteErrorInternal("Directories have no content", path, ErrorCategory.InvalidOperation);
            }
            if (obj is File)
            {
                if (ShouldProcess($"Clear content from {GetServerRelativePath(path)}"))
                {
                    var file = obj as File;
                    file.SaveBinary(new FileSaveBinaryInformation());
                }
            }
        }

        public object ClearContentDynamicParameters(string path)
        {
            return null;
        }

#region Helpers

        //Get helpers
        private object GetFileOrFolder(string path, bool throwError = true, bool useCache = true)
        {
            var spoDrive = GetCurrentDrive(path);
            if (spoDrive == null) return null;

            var serverRelativePath = GetServerRelativePath(path);
            if (string.IsNullOrEmpty(serverRelativePath)) return null;

            //Try get cached item
            if (useCache)
            {
                var fileOrFolder = GetCachedItem(serverRelativePath);
                if (fileOrFolder != null) return fileOrFolder.Item;
            }

            //Find web closes to object
            var web = FindWebInPath(serverRelativePath);
            spoDrive.Web = web;
            var webUrl = web.EnsureProperty(w => w.ServerRelativeUrl);

            //If path is current web root return root folder
            if (serverRelativePath.Equals(webUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                web.EnsureProperty(w => w.RootFolder);
                SetCachedItem(serverRelativePath, web.RootFolder);
                return web.RootFolder;
            }

            //Search object
            var result = ExecuteObjectSearch(serverRelativePath, web);
            if (result is File || result is Folder)
            {
                //Cache item
                SetCachedItem(serverRelativePath, result);
                return result;
            }

            //Cache not found item
            SetCachedItem(serverRelativePath, null);

            //Error
            if (throwError && result is Exception)
            {
                var ex = result as Exception;
                WriteErrorInternal(ex.Message, path, ErrorCategory.ObjectNotFound, exception: ex);
            }

            return null;
        }

        private object ExecuteObjectSearch(string serverRelativePath, Web web)
        {
            //Workaround for CSOM v15.0 to not get deleted items from object data
            web.ClearObjectData();

            var ctx = web.Context;
            File file;
            Folder folder;
            var scope = new ExceptionHandlingScope(ctx);
            var tryFileFirst = serverRelativePath.Split(PathSeparator.ToCharArray()).Last().Contains(".");

            if (tryFileFirst)
            {
                using (scope.StartScope())
                {
                    using (scope.StartTry())
                    {
                        file = web.GetFileByServerRelativeUrl(serverRelativePath);
                        ctx.Load(file);
                    }
                    using (scope.StartCatch())
                    {
                        folder = web.GetFolderByServerRelativeUrl(serverRelativePath);
                        ctx.Load(folder);
                    }
                }
            }
            else
            {
                using (scope.StartScope())
                {
                    using (scope.StartTry())
                    {
                        folder = web.GetFolderByServerRelativeUrl(serverRelativePath);
                        ctx.Load(folder);
                    }
                    using (scope.StartCatch())
                    {
                        file = web.GetFileByServerRelativeUrl(serverRelativePath);
                        ctx.Load(file);
                    }
                }
            }

            try
            {
                ctx.ExecuteQueryRetry();
            }
            catch (Exception e)
            {
                return e;
            }

            //Check if we got data
            if (IsPropertyAvailable(file, "Name"))
            {
                return file;
            }
            else if (IsPropertyAvailable(folder, "Name"))
            {
                return folder;
            }

            return null;
        }

        private IEnumerable<object> GetFolderItems(Folder folder, bool throwError = false, int? limit = null)
        {
            var folderAndFiles = new List<object>();
            try
            {
                if (folder != null)
                {
                    var serverRelativePath = GetServerRelativePath(folder.EnsureProperty(f => f.ServerRelativeUrl));

                    //Get cached child items
                    var cachedFolderAndFiles = GetCachedChildItems(serverRelativePath);
                    if (cachedFolderAndFiles != null) return cachedFolderAndFiles;

                    var ctx = folder.Context as ClientContext;

                    if (ctx != null)
                    {
                        var webUrl = GetServerRelativePath(ctx.Web.EnsureProperty(w => w.ServerRelativeUrl));

                        //If root of web get sub-sites
                        if (serverRelativePath.Equals(webUrl, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var subWebs = ctx.Web.EnsureProperty(w => w.Webs.Include(sw => sw.RootFolder)).ToList();
                            folderAndFiles.AddRange(subWebs.Select(subWeb => subWeb.RootFolder));
                        }
                    }

                    //If large document library use CamlQuery
                    folder.EnsureProperty(p => p.ItemCount);
                    if (folder.ItemCount > 5000)
                    {
                        if (!limit.HasValue)
                        {
                            WriteWarning("Large document library! Only displaying the 100 first items. Use Get-ChildItems -Limit <int>|All");
                        }
                        WriteWarning("Large document library! Template folder and files will not be shown.");

                        folderAndFiles.AddRange(limit.HasValue ? GetListItems(folder, limit.Value) : GetListItems(folder));
                    }
                    else
                    {
                        //Get files and folders
                        var files = folder.Context.LoadQuery(folder.Files.Include(f => f.Name, f => f.ServerRelativeUrl, f => f.TimeLastModified, f => f.Length)).OrderBy(f => f.Name);
                        var folders = folder.Context.LoadQuery(folder.Folders.Include(f => f.Name, f => f.ItemCount, f => f.TimeLastModified, f => f.ServerRelativeUrl)).OrderBy(f => f.Name);

                        folder.Context.ExecuteQueryRetry();

                        //Merge
                        folderAndFiles.AddRange(folders);
                        folderAndFiles.AddRange(files);
                    }

                    //Cache the result
                    SetCachedChildItems(serverRelativePath, folderAndFiles);
                }
            }
            catch (Exception e)
            {
                if (throwError)
                {
                    WriteErrorInternal(e.Message, "GetFolderItems", exception: e);
                }
            }
            return limit.HasValue && limit > 0 ? folderAndFiles.Take(limit.Value) : folderAndFiles;
        }

        private IEnumerable<object> GetListItems(Folder folder, long limit = 100)
        {
            var list = ((ClientContext)folder.Context).Web.GetListByUrl(folder.ServerRelativeUrl);
            var folderAndFiles = new List<object>();
            limit = limit < 1 ? folder.ItemCount : limit;
            var rowLimit = limit > 5000 ? 5000 : limit;

            var query = new CamlQuery();
            query.FolderServerRelativeUrl = folder.ServerRelativeUrl;

            query.ViewXml = $@"
                <View Scope=''>
                    <Query>
                        <OrderBy><FieldRef Name='FileRef' Ascending='true'/></OrderBy>
                    </Query>
                    <RowLimit>{rowLimit}</RowLimit>
                </View>";

            do
            {
                var listItems = list.GetItems(query);
                folder.Context.Load(listItems, items => items.ListItemCollectionPosition, items => items.Include(i => i.File, i => i.Folder, i => i.FileSystemObjectType));
                folder.Context.ExecuteQueryRetry();

                folderAndFiles.AddRange(listItems.Where(i => i.FileSystemObjectType == FileSystemObjectType.Folder).Select(f => f.Folder));
                folderAndFiles.AddRange(listItems.Where(i => i.FileSystemObjectType == FileSystemObjectType.File).Select(f => f.File));

                query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
            }
            while (query.ListItemCollectionPosition != null && folderAndFiles.Count < limit);

            return folderAndFiles;
        }

        private Web FindWebInPath(string serverRelativePath)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return null;

            var pathParts = serverRelativePath.Split(PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var webUrl = spoDrive.Web.EnsureProperty(w => w.Url);
            var webUri = new Uri(webUrl);
            var hostUrl = $"{webUri.Scheme}://{webUri.Host}/";

            for (var i = pathParts.Length; i >= 0; i--)
            {
                var path = string.Join(PathSeparator, pathParts.Take(i));
                var fullUrl = hostUrl + path;
                var partsServerRelativePath = PathSeparator + path;

                try
                {
                    var cachedWeb = GetCachedWeb(partsServerRelativePath);
                    if (cachedWeb == null)
                    {
                        var ctx = spoDrive.Web.Context.Clone(fullUrl);
                        ctx.ExecuteQueryRetry();
#if DEBUG
                        SetCounter(ctx);
#endif
                        SetCachedWeb(partsServerRelativePath, ctx.Web);
                        return ctx.Web;
                    }
                    else if (cachedWeb.Web != null)
                    {
                        return cachedWeb.Web;
                    }
                }
                catch
                {
                    SetCachedWeb(partsServerRelativePath, null);
                }
            }
            return null;
        }

        private void SetCounter(ClientRuntimeContext ctx)
        {
            ctx.ExecutingWebRequest += (sender, args) =>
            {
                var psVariable = SessionState.PSVariable.Get("WebRequestCounter");
                var counter = (int?)psVariable?.Value ?? 0;
                counter++;
                SessionState.PSVariable.Set("WebRequestCounter", counter);
            };
        }

        //CopyMove helper
        private void CopyMoveImplementation(string sourcePath, string targetPath, bool recurse = false, bool isCopyOperation = true, bool reCreateSourceFolder = true)
        {
            var sourceUrl = GetServerRelativePath(sourcePath);
            var source = GetFileOrFolder(sourcePath) as ClientObject;
            if (source == null) return;

            var targetUrl = GetServerRelativePath(targetPath);
            var targetIsFile = targetUrl.Split(PathSeparator.ToCharArray()).Last().Contains(".");
            var targetFolderPath = (targetIsFile) ? GetParentServerRelativePath(targetUrl) : targetUrl;
            var sourceWeb = ((ClientContext)source.Context).Web;
            var targetWeb = FindWebInPath(targetFolderPath);

            if (!isCopyOperation && targetUrl.StartsWith(sourceUrl))
            {
                var msg = "Cannot move source. Target is inside source";
                var err = new ArgumentException(msg);
                WriteErrorInternal(msg, sourcePath, ErrorCategory.InvalidOperation, true, err);
                return;
            }

            if (targetWeb == null)
            {
                var msg = "Target web not found";
                var err = new FileNotFoundException(msg);
                WriteErrorInternal(msg, sourcePath, ErrorCategory.ObjectNotFound, true, err);
                return;
            }

            var isSameWeb = IsSameWeb(sourceWeb, targetWeb);
            var targetFolder = GetFileOrFolder(targetFolderPath, false) as Folder;
            var endOfPathFolderCreated = false;
            if (targetFolder == null)
            {
                //Create target folder
                endOfPathFolderCreated = true;
                targetFolder = targetWeb.EnsureFolderPath(PathSeparator + GetWebRelativePath(targetFolderPath));
            }

            if (ShouldProcess($"{GetServerRelativePath(sourcePath)} to {targetUrl}"))
            {
                if (source is File)
                {
                    var sourceFile = source as File;
                    var targetFilePath = (targetIsFile) ? targetUrl : targetFolderPath + PathSeparator + sourceFile.Name;

                    if (isSameWeb)
                    {
                        if (isCopyOperation)
                        {
                            sourceFile.CopyTo(targetFilePath, Force);
                        }
                        else
                        {
                            var moveToOperations = (Force) ? MoveOperations.Overwrite : MoveOperations.None;
                            sourceFile.MoveTo(targetFilePath, moveToOperations);
                        }
                        sourceFile.Context.ExecuteQueryRetry();
                    }
                    else
                    {
                        var sourceStream = sourceFile.OpenBinaryStream();
                        sourceFile.Context.ExecuteQueryRetry();

                        targetFolder.UploadFile(targetFilePath.Split(PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last(), sourceStream.Value, Force);

                        if (!isCopyOperation)
                        {
                            sourceFile.DeleteObject();
                            sourceFile.Context.ExecuteQueryRetry();
                        }
                    }
                }
                else if (source is Folder && !targetIsFile)
                {
                    var sourceFolder = source as Folder;
                    var rootFolder = (endOfPathFolderCreated || !reCreateSourceFolder) ? targetFolder : targetFolder.CreateFolder(sourceFolder.Name);

                    if (recurse)
                    {
                        var folderAndFiles = GetFolderItems(sourceFolder);

                        foreach (var folder in folderAndFiles.OfType<Folder>())
                        {
                            var subFolder = rootFolder.CreateFolder(folder.Name);
                            CopyMoveImplementation(folder.ServerRelativeUrl, subFolder.ServerRelativeUrl, recurse, isCopyOperation, false);
                        }

                        foreach (var file in folderAndFiles.OfType<File>())
                        {
                            CopyMoveImplementation(file.ServerRelativeUrl, rootFolder.ServerRelativeUrl, recurse, isCopyOperation);
                        }
                    }

                    if (!isCopyOperation)
                    {
                        sourceFolder.DeleteObject();
                        sourceFolder.Context.ExecuteQueryRetry();
                    }
                }
                else
                {
                    WriteErrorInternal("Operation not supported", sourcePath, ErrorCategory.InvalidOperation);
                }
            }
        }

        //Drive helpers
        private SPODriveInfo GetCurrentDrive(string path)
        {
            return PSDriveInfo as SPODriveInfo ?? GetDrive(path);
        }

        private SPODriveInfo GetDrive(string path)
        {
            if (!path.Contains(":")) return null;
            return ProviderInfo.Drives.FirstOrDefault(d => path.StartsWith(d.Name, StringComparison.InvariantCultureIgnoreCase)) as SPODriveInfo;
        }

        private string RemoveDriveFromPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            var spoDrive = GetCurrentDrive(path);
            if (spoDrive == null) return path;

            var result = Regex.Replace(path, $@"^{spoDrive.Name}:", string.Empty);

            return result;
        }

        private bool IsPathDrive(string path)
        {
            var spoDrive = GetCurrentDrive(path);
            if (spoDrive == null) return false;

            var normalizedPath = GetServerRelativePath(path);
            return normalizedPath.Equals(spoDrive.Root, StringComparison.InvariantCultureIgnoreCase);
        }

        //Validate helpers
        private bool IsPropertyAvailable(ClientObject clientObject, string propertyName)
        {
            return clientObject.IsObjectPropertyInstantiated(propertyName) || clientObject.IsPropertyAvailable(propertyName);
        }

        private bool IsInSameSiteCollection(Web web1, Web web2)
        {
            var ctx1 = web1.Context as ClientContext;
            var ctx2 = web2.Context as ClientContext;

            if (ctx1 == null || ctx2 == null) return false;

            ctx1.Load(ctx1.Site, s => s.Url);
            ctx2.Load(ctx2.Site, s => s.Url);
            ctx1.ExecuteQueryRetry();
            ctx2.ExecuteQueryRetry();

            return ctx1.Site.Url.Equals(ctx2.Site.Url);
        }

        private bool IsSameWeb(Web web1, Web web2)
        {
            var url1 = web1.EnsureProperty(w => w.Url);
            var url2 = web2.EnsureProperty(w => w.Url);

            return url1.Equals(url2);
        }

        //Error helper
        internal void WriteErrorInternal(string message, object path, ErrorCategory errorCategory = ErrorCategory.NotSpecified, bool terminate = false, Exception exception = null)
        {
            exception = exception ?? new Exception(message);
            var error = new ErrorRecord(exception, message, errorCategory, path);

            if (terminate)
            {
                ThrowTerminatingError(error);
            }
            else
            {
                WriteError(error);
            }
        }

        //Path helpers
        private string GetServerRelativePath(string path)
        {
            var spoDrive = GetCurrentDrive(path);
            if (spoDrive == null) return null;

            var normalizePath = NormalizePath(path);
            normalizePath = RemoveDriveFromPath(normalizePath);
            if (!normalizePath.StartsWith(PathSeparator))
            {
                normalizePath = NormalizePath(spoDrive.Root.TrimEnd(PathSeparator.ToCharArray()) + PathSeparator + normalizePath);
            }
            return normalizePath;
        }

        private string GetParentServerRelativePath(string path)
        {
            var serverRelativePath = GetServerRelativePath(path);
            if (string.IsNullOrEmpty(serverRelativePath)) return serverRelativePath;

            var index = serverRelativePath.LastIndexOf(PathSeparator);
            if (index > -1)
            {
                return serverRelativePath.Substring(0, index);
            }

            return serverRelativePath;
        }

        private string GetWebRelativePath(string serverRelativePath)
        {
            if (string.IsNullOrEmpty(serverRelativePath)) return serverRelativePath;

            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return null;

            var webPath = spoDrive.Web.EnsureProperty(w => w.ServerRelativeUrl);
            var result = Regex.Replace(serverRelativePath, $@"^{webPath}", string.Empty);
            if (string.IsNullOrEmpty(result)) result = PathSeparator;
            return result;
        }

        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            var result = Regex.Replace(path, @"\\", PathSeparator);
            //result = result.Replace("*", string.Empty);
            result = Regex.Replace(result, @"/{2,}", PathSeparator);
            result = result.TrimEnd(PathSeparator.ToCharArray());
            result = string.IsNullOrEmpty(result) ? PathSeparator : result;

            return result;
        }

        //Cache helpers
        private SPODriveCacheItem GetCachedItem(string serverRelativePath)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return null;

            var cachedItem = spoDrive.CachedItems.FirstOrDefault(c => c.Path == serverRelativePath && (new TimeSpan(DateTime.Now.Ticks - c.LastRefresh.Ticks)).TotalMilliseconds < spoDrive.ItemTimeout);
            return cachedItem;
        }

        private void SetCachedItem(string serverRelativePath, object obj)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return;

            spoDrive.CachedItems.RemoveAll(item => item.Path == serverRelativePath);
            spoDrive.CachedItems.Add(new SPODriveCacheItem
            {
                Path = serverRelativePath,
                LastRefresh = DateTime.Now,
                Item = obj
            });
        }

        private IEnumerable<object> GetCachedChildItems(string serverRelativePath)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return null;

            var nowTicks = DateTime.Now.Ticks;
            var childItems = spoDrive.CachedItems.Where(c => GetParentServerRelativePath(c.Path) == serverRelativePath && (new TimeSpan(nowTicks - c.LastRefresh.Ticks)).TotalMilliseconds < spoDrive.ItemTimeout);
            var spoDriveCacheItems = childItems as SPODriveCacheItem[] ?? childItems.ToArray();
            return spoDriveCacheItems.Any() ? spoDriveCacheItems.Select(c => c.Item) : null;
        }

        private void SetCachedChildItems(string parentServerRelativePath, IEnumerable<object> childItems)
        {
            var spoDrive = GetCurrentDrive(parentServerRelativePath);
            if (spoDrive == null) return;

            foreach (var item in childItems)
            {
                var name = string.Empty;

                if (item is Folder)
                {
                    var folder = item as Folder;
                    name = string.IsNullOrEmpty(folder.Name) ? folder.ServerRelativeUrl.Split(PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last() : folder.Name;
                }
                else if (item is File)
                {
                    var file = item as File;
                    name = file.Name;
                }
                SetCachedItem(parentServerRelativePath.TrimEnd(PathSeparator.ToCharArray()) + PathSeparator + name, item);
            }
        }

        private SPODriveCacheWeb GetCachedWeb(string serverRelativePath)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return null;

            var result = spoDrive.CachedWebs.FirstOrDefault(c => c.Path == serverRelativePath && (new TimeSpan(DateTime.Now.Ticks - c.LastRefresh.Ticks)).TotalMilliseconds < spoDrive.WebTimeout * 100);
            return result;
        }

        private void SetCachedWeb(string serverRelativePath, Web web)
        {
            var spoDrive = GetCurrentDrive(serverRelativePath);
            if (spoDrive == null) return;

            spoDrive.CachedWebs.RemoveAll(c => c.Path.Equals(serverRelativePath));
            spoDrive.CachedWebs.Add(new SPODriveCacheWeb
            {
                Web = web,
                Path = serverRelativePath,
                LastRefresh = DateTime.Now
            });
        }

#endregion
    }
}
