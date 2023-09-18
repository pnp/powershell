using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolder", DefaultParameterSetName = ParameterSet_CURRENTWEBROOTFOLDER)]
    [OutputType(typeof(Folder))]
    public class GetFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        private const string ParameterSet_CURRENTWEBROOTFOLDER = "Root folder of the current Web";
        private const string ParameterSet_LISTROOTFOLDER = "Root folder of a list";
        private const string ParameterSet_FOLDERBYURL = "Folder by url";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Alias("RelativeUrl")]
        public string Url;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_LISTROOTFOLDER)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Folder, object>>[] { f => f.ServerRelativeUrl, f => f.Name, f => f.TimeLastModified, f => f.ItemCount };

            Folder folder = null;
            switch(ParameterSetName)
            {
                case ParameterSet_CURRENTWEBROOTFOLDER:
                {
                    WriteVerbose("Getting root folder of the current web");
                    folder = CurrentWeb.RootFolder;
                    break;
                }

                case ParameterSet_LISTROOTFOLDER:
                {
                    WriteVerbose("Getting root folder of the provided list");
                    var list = List.GetList(CurrentWeb);
                    folder = list.RootFolder;
                    break;
                }

                case ParameterSet_FOLDERBYURL:
                {
                    WriteVerbose("Getting folder at the provided url");
                    var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                    if (!Url.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        Url = UrlUtility.Combine(webServerRelativeUrl, Url);
                    }
                    folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Url));
                    break;
                }
            }

            WriteVerbose("Retrieving folder properties");
            folder?.EnsureProperties(RetrievalExpressions);
            WriteObject(folder);
        }
    }
}
