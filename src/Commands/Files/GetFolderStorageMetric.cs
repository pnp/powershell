using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderStorageMetric", DefaultParameterSetName = ParameterSet_BYSITRERELATIVEURL)]
    [OutputType(typeof(Model.SharePoint.FolderStorageMetric))]
    public class GetFolderStorageMetric : PnPWebCmdlet
    {        
        private const string ParameterSet_BYSITRERELATIVEURL = "Folder via site relative URL";
        private const string ParameterSet_BYLIST = "Folder via list";
        private const string ParameterSet_BYFOLDER = "Folder via pipebind";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYSITRERELATIVEURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYLIST)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYFOLDER)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Folder targetFolder = null;
            switch (ParameterSetName)
            {
                case ParameterSet_BYFOLDER:
                    targetFolder = Identity.GetFolder(CurrentWeb);
                    break;
                case ParameterSet_BYLIST:
                    var list = List.GetList(CurrentWeb);
                    if (list != null)
                    {
                        targetFolder = list.RootFolder;
                    }
                    break;

                case ParameterSet_BYSITRERELATIVEURL:
                    string serverRelativeUrl = null;
                    if (!string.IsNullOrEmpty(FolderSiteRelativeUrl))
                    {
                        var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                        serverRelativeUrl = UrlUtility.Combine(webUrl, FolderSiteRelativeUrl);
                    }

                    targetFolder = (string.IsNullOrEmpty(FolderSiteRelativeUrl)) ? CurrentWeb.RootFolder : CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
                    break;
                
                default:
                    throw new NotImplementedException($"Parameter set {ParameterSetName} not implemented");
            }

            if (targetFolder != null)
            {
                ClientContext.Load(targetFolder, t => t.ServerRelativeUrl);
                ClientContext.ExecuteQueryRetry();

                IFolder folderWithStorageMetrics = PnPContext.Web.GetFolderByServerRelativeUrlAsync(targetFolder.ServerRelativeUrl, f => f.StorageMetrics).GetAwaiter().GetResult();
                var storageMetrics = folderWithStorageMetrics.StorageMetrics;

                WriteObject(new Model.SharePoint.FolderStorageMetric
                {
                    LastModified = storageMetrics.LastModified,
                    TotalFileCount = storageMetrics.TotalFileCount,
                    TotalFileStreamSize = storageMetrics.TotalFileStreamSize,
                    TotalSize = storageMetrics.TotalSize
                });
            }
            else
            {
                throw new PSArgumentException($"Can't find the specified folder.");
            }
        }       
    }
}