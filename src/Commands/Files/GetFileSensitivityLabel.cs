using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph.Purview;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFileSensitivityLabel")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]
    [OutputType(typeof(SensitivityLabels))]
    public class GetFileSensitivityLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                // We can't deal with absolute URLs
                Url = UrlUtility.MakeRelativeUrl(Url);
            }

            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            Connection.PnPContext.Web.EnsureProperties(w => w.ServerRelativeUrl);

            var webUrl = Connection.PnPContext.Web.ServerRelativeUrl;

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            var file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(Url);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID);

            var requestUrl = $"v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/extractSensitivityLabels";

            var results = GraphHelper.Post<SensitivityLabels>(this, Connection, requestUrl, AccessToken);
            WriteObject(results.Labels, true);
        }
    }
}
