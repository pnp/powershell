using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using AngleSharp.Io;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItemVersion")]
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
                       
            // Use SharePointRequestHelper to make the REST call
            try
            {
                var response = SharePointRequestHelper.PostHttpContent(
                    expirationUrl,
                    new StringContent(
                        JsonSerializer.Serialize(expirationPayload, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true }),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    )
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    throw new Exception($"Failed to set expiration date. Status: {response.StatusCode}, Content: {errorContent}");
                }

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
