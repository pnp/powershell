using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Reset, "PnPDocumentId")]
    public class ResetDocumentId : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public FilePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var file = Identity.GetFile(ClientContext, this);
            file.EnsureProperties(f => f.ServerRelativeUrl);
            CurrentWeb.EnsureProperty(w => w.Url);

            // Even though the URL parameter states server relative path, it requires the site relative path of the file
            var siteRelativeUrl = file.ServerRelativeUrl.Remove(0, new Uri(CurrentWeb.Url).AbsolutePath.Length);
            var url = $"{CurrentWeb.Url}/_api/SP.DocumentManagement.DocumentId/ResetDocIdByServerRelativePath(decodedUrl='{siteRelativeUrl}')";
            
            WriteVerbose($"Making a POST request to {url} to request a new document ID for the file {file.ServerRelativeUrl}");
            RestHelper.Post(Connection.HttpClient, url, ClientContext);
        }
    }
}
