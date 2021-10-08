using Microsoft.SharePoint.Client;
using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class FolderPipeBind
    {
        private readonly Folder _folder;
        private readonly Guid _id;
        private readonly string _name;

        public FolderPipeBind()
        {
            _folder = null;
            _id = Guid.Empty;
            _name = string.Empty;
        }

        public FolderPipeBind(Folder folder)
        {
            _folder = folder;
        }

        public FolderPipeBind(Guid guid)
        {
            _id = guid;
        }

        public FolderPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _name = id;
            }
        }

        public Guid Id => _id;

        public Folder Folder => _folder;

        public string ServerRelativeUrl => _name;

        internal Folder GetFolder(Web web, bool throwError = true)
        {
            try
            {
                Folder folder = null;
                if (Folder != null)
                {
                    folder = Folder;
                }
                else if (Id != Guid.Empty)
                {
                    folder = web.GetFolderById(Id);
                }
                else if (!string.IsNullOrEmpty(ServerRelativeUrl))
                {
                    folder = web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(ServerRelativeUrl));
                }
                if (folder != null)
                {
                    web.Context.Load(folder);
                    web.Context.ExecuteQueryRetry();
                }
                return folder;
            }
            catch
            {
                if (throwError)
                    throw;
                else
                    return null;
            }
        }
    }
}
