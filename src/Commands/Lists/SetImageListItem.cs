using System;
using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPImageListItemColumn", DefaultParameterSetName = ParameterSet_ASServerRelativeUrl)]
    [OutputType(typeof(ListItem))]
    public class ImageListItemColumn : PnPWebCmdlet
    {
        private const string ParameterSet_ASServerRelativeUrl = "Using server relative url";
        private const string ParameterSet_ASPath = "Uploaded from file system";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ASServerRelativeUrl)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ASPath)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ASServerRelativeUrl)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ASPath)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ASServerRelativeUrl)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ASPath)]
        public FieldPipeBind Field = new FieldPipeBind();

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASServerRelativeUrl)]
        public string ServerRelativePath;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASPath)]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASServerRelativeUrl)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPath)]
        public ListItemUpdateType UpdateType;
        protected override void ExecuteCmdlet()
        {
            if (Identity == null || (Identity.Item == null && Identity.Id == 0))
            {
                throw new PSArgumentException($"No -Identity has been provided specifying the item to update", nameof(Identity));
            }

            List list;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }
            else
            {
                if (Identity.Item == null)
                {
                    throw new PSArgumentException($"No -List has been provided specifying the list to update the item in", nameof(Identity));
                }

                list = Identity.Item.ParentList;
            }

            var item = Identity.GetListItem(list)
                ?? throw new PSArgumentException($"Provided -Identity is not valid.", nameof(Identity));

            var f = Field.Field;

            if (f == null)
            {
                if (Field.Id != Guid.Empty)
                {
                    f = list.Fields.GetById(Field.Id);
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    f = list.Fields.GetByInternalNameOrTitle(Field.Name);
                }
            }
            ClientContext.Load(f);
            ClientContext.ExecuteQueryRetry();

            if (f.FieldTypeKind != FieldType.Thumbnail)
            {
                throw new PSInvalidOperationException("Field Type is not Image, please use a valid field");
            }

            var tenantUri = new Uri(Connection.Url);
            var web = ClientContext.Web;

            File file;
            if (ParameterSpecified(nameof(ServerRelativePath)))
            {
                file = web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(ServerRelativePath));
                ClientContext.Load(file, fi => fi.UniqueId, fi => fi.ServerRelativePath, fi => fi.Name);
                ClientContext.ExecuteQueryRetry();
            }
            else
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                var fileName = System.IO.Path.GetFileName(Path);

                
                web.EnsureProperty(w => w.ServerRelativeUrl);

                var createdList = web.Lists.EnsureSiteAssetsLibrary();
                ClientContext.Load(createdList, l => l.RootFolder);
                ClientContext.ExecuteQueryRetry();

                Folder folder = null;
                var folderPath = $"/{createdList.RootFolder.Name}/Lists/{list.Id}";

                // Try to create the folder
                WriteVerbose("Ensuring necessary folder path for the image to be uploaded.");
                folder = web.EnsureFolderPath(folderPath);
                WriteVerbose("Uploading the file to be set as a thumbnail image.");
                file = folder.UploadFile(fileName, Path, true);

                file.EnsureProperties(fi => fi.UniqueId, fi => fi.ServerRelativePath, fi => fi.Name);
            }

            var payload = new
            {
                type = "thumbnail",
                fileName = file.Name,
                nativeFile = new { },
                fieldName = f.InternalName,
                serverUrl = "https://" + tenantUri.Host,
                fieldId = f.Id,
                serverRelativeUrl = file.ServerRelativePath.DecodedUrl,
                id = file.UniqueId.ToString()
            };

            item[f.InternalName] = JsonSerializer.Serialize(payload);

            ListItemHelper.UpdateListItem(item, UpdateType);

            ClientContext.ExecuteQueryRetry();
            ClientContext.Load(item);
            WriteObject(item);
        }
    }
}