using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Extensions;
using PnP.PowerShell.Commands.Provisioning.Site;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderStorageMetric")]
    public class GetFolderStorageMetric : PnPWebCmdlet
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";
        private const string ParameterSet_LISTNAME = "Folder via listName";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_LISTNAME)]
        public string ListName;

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public FolderPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Folder targetFolder = null;
            if (ParameterSetName == ParameterSet_FOLDERSBYPIPE && Identity != null)
            {
                targetFolder = Identity.GetFolder(CurrentWeb);
            }
            if (ParameterSetName == ParameterSet_LISTNAME && ListName != null)
            {
                var list = CurrentWeb.GetListByTitle(ListName);
                if (list != null)
                {
                    targetFolder = list.RootFolder;
                }
            }
            else
            {
                string serverRelativeUrl = null;
                if (!string.IsNullOrEmpty(FolderSiteRelativeUrl))
                {
                    var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                    serverRelativeUrl = UrlUtility.Combine(webUrl, FolderSiteRelativeUrl);
                }

                targetFolder = (string.IsNullOrEmpty(FolderSiteRelativeUrl)) ? CurrentWeb.RootFolder : CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            }

            if (targetFolder != null)
            {
                ClientContext.Load(targetFolder, t => t.ServerRelativeUrl);
                ClientContext.ExecuteQueryRetry();

                IFolder folderWithStorageMetrics = PnPContext.Web.GetFolderByServerRelativeUrlAsync(targetFolder.ServerRelativeUrl, f => f.StorageMetrics).GetAwaiter().GetResult();
                var storageMetrics = folderWithStorageMetrics.StorageMetrics;

                WriteObject(storageMetrics);
            }
            else
            {
                throw new PSArgumentException($"Can't find the specified folder.");
            }
        }       
    }
}
