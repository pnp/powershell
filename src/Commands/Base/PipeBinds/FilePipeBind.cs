using System;
using Microsoft.SharePoint.Client;
using System.Management.Automation;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class FilePipeBind
    {
        #region Properties

        public Guid? Id { get; private set; }
        public File File { get; private set; }
        public IFile CoreFile { get; private set; }
        public ListItem ListItem { get; private set; }
        public string ServerRelativeUrl { get; private set; }

        #endregion

        #region Constructors

        public FilePipeBind()
        {
        }

        public FilePipeBind(Guid id)
        {
            Id = id;
        }

        public FilePipeBind(ListItem listItem)
        {
            ListItem = listItem;
        }

        public FilePipeBind(File file)
        {
            File = file;
        }

        public FilePipeBind(IFile coreFile)
        {
            CoreFile = coreFile;
        }

        public FilePipeBind(string id)
        {
            if (Guid.TryParse(id, out Guid fileGuid))
            {
                Id = fileGuid;
            }
            else
            {
                ServerRelativeUrl = id;
            }
        }

        #endregion

        #region Methods

        internal IFile GetCoreFile(PnPContext context, Cmdlet cmdlet = null)
        {
            if(CoreFile != null)
            {
                cmdlet?.WriteVerbose("File determined based on CoreFile instance");
                return CoreFile;
            }

            if (File != null)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on CSOM File instance");
                File.EnsureProperties(f => f.UniqueId);
                return context.Web.GetFileById(File.UniqueId);
            }

            if (ListItem != null)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on CSOM ListItem instance");
                ListItem.EnsureProperties(i => i.File);
                return context.Web.GetFileById(ListItem.File.UniqueId);
            }

            if (Id.HasValue)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on file id");
                return context.Web.GetFileById(Id.Value);
            }

            if (!string.IsNullOrEmpty(ServerRelativeUrl))
            {
                cmdlet?.WriteVerbose("File will be retrieved based on server relative url");
                return context.Web.GetFileByServerRelativeUrl(ServerRelativeUrl);
            }

            throw new PSInvalidOperationException("No information available to retrieve file");
        }

        internal File GetFile(ClientContext context, Cmdlet cmdlet = null)
        {
            if (File != null)
            {
                cmdlet?.WriteVerbose("File determined based on CSOM File instance");
                return File;
            }

            if (CoreFile != null)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on PnP Core File instance");
                CoreFile.EnsureProperties(f => f.UniqueId);
                return context.Web.GetFileById(File.UniqueId);
            }

            if (ListItem != null)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on CSOM ListItem instance");
                ListItem.EnsureProperties(i => i.File);
                return context.Web.GetFileById(ListItem.File.UniqueId);
            }

            if (Id.HasValue)
            {
                cmdlet?.WriteVerbose("File will be retrieved based on file id");
                return context.Web.GetFileById(Id.Value);
            }

            if (!string.IsNullOrEmpty(ServerRelativeUrl))
            {
                cmdlet?.WriteVerbose("File will be retrieved based on server relative url");
                return context.Web.GetFileByServerRelativeUrl(ServerRelativeUrl);
            }

            throw new PSInvalidOperationException("No information available to retrieve file");
        }

        #endregion
    }
}