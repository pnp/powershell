using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileVersion")]
    public class SetListItemVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemVersionPipeBind Version;

        [Parameter(Mandatory = false)]
        public DateTime? ExpirationDate { get; set; }

    protected override void ExecuteCmdlet()
    {
        var list = List.GetList(Connection.PnPContext);

        // Ensure ExpirationDate is provided
        if (!(ParameterSpecified(nameof(ExpirationDate)))) {
            LogWarning("No expiration date was provided. The operation was not performed.");
            return;
        }
    
        if (list is null)
        {
            throw new PSArgumentException($"Cannot find the list provided through -{nameof(List)}", nameof(List));
        }

        var item = Identity.GetListItem(list);

        if (item is null)
        {
            throw new PSArgumentException($"Cannot find the list item provided through -{nameof(Identity)}", nameof(Identity));
        }

        item.EnsureProperties(i => i.All, i => i.Versions, i => i.File);

        var itemVersionCollection = item.Versions.AsRequested();

        IListItemVersion version = null;
        if (!string.IsNullOrEmpty(Version.VersionLabel))
        {
            version = itemVersionCollection.FirstOrDefault(v => v.VersionLabel == Version.VersionLabel);
        }
        else if (Version.Id != -1)
        {
            version = itemVersionCollection.FirstOrDefault(v => v.Id == Version.Id);
        }

        if (version is null)
        {
            throw new PSArgumentException($"Cannot find the list item version provided through -{nameof(Version)}", nameof(Version.VersionLabel));
        }

        // Only proceed if the item has an associated file (i.e., is a document)
        if (item.File == null)
        {
            throw new PSArgumentException("The specified list item does not represent a file. Expiration date can only be set for file versions in document libraries.");
        }

        // Build REST API URL
        var siteUrl = Connection.Url.TrimEnd('/');
        var fileUrl = item.File.ServerRelativeUrl;
        if (string.IsNullOrEmpty(fileUrl))
        {
            throw new PSArgumentException("Cannot determine the file URL for the list item.");
        }
        var encodedFileUrl = Uri.EscapeDataString(fileUrl);
        var expirationUrl = $"{siteUrl}/_api/web/GetFileByServerRelativePath(DecodedUrl='{encodedFileUrl}')/versions({version.Id})/SetExpirationDate()";

        LogDebug($"Calling: {expirationUrl}");

            // Prepare REST payload
            var expirationPayload = ExpirationDate.HasValue
                ? $"{{ \"expirationDate\": \"{ExpirationDate.Value.ToString("o")}\" }}"
                : "{ \"expirationDate\": null }";

            var stringContent = new StringContent(expirationPayload);

            try
            {
                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient(Connection.Context);
                var accessToken = Connection.Context.GetAccessToken();

                var payload = new { expirationDate = ExpirationDate.HasValue ? ExpirationDate.Value.ToString("o") : null };

                var responseString = Utilities.REST.RestHelper.Post(
                    httpClient,
                    expirationUrl,    // The full REST API URL
                    accessToken,
                    payload           // Pass as an object, not a string
                );


                LogDebug($"Updated expiration date for version {version.VersionLabel} of list item {item.Id} in list {list.Title}");
            }
            catch (Exception ex)
            {
                LogError($"Error setting expiration date: {ex.Message}");
                throw;
            }    
        }
    }
}
