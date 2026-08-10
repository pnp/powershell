using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph.Purview;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFileRetentionLabel")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]
    [OutputType(typeof(FileRetentionLabel))]
    public class GetFileRetentionLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                // We can't deal with absolute URLs
                Url = UrlUtility.MakeRelativeUrl(Url);
            }

            Connection.PnPContext.Web.EnsureProperties(w => w.ServerRelativeUrl);

            var webUrl = Connection.PnPContext.Web.ServerRelativeUrl;

            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            var file = Utilities.FileUrlResolver.ResolveFile(Url, webUrl, ClientContext, ClientContext?.Web, Connection.PnPContext);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID);

            var requestUrl = $"v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/retentionLabel";
            
            var results = GraphRequestHelper.Get<FileRetentionLabel>(requestUrl);
            WriteObject(results, true);
        }
    }
}
