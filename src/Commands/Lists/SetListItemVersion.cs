using System;
using System.Linq;
using System.Management.Automation;
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

            item.EnsureProperties(i => i.All, i => i.Versions);

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
                throw new PSArgumentException($"Cannot find the list item version provided through -{nameof(Version)}", nameof(Version));
            }

            // Build REST API URL
            var siteUrl = Connection.Url.TrimEnd('/');
            var fileUrl = item["FileRef"]?.ToString();
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

            // Use PnPHttpClient and RestHelper.Post for the REST call
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient(Connection.Context);
            var accessToken = Connection.Context.GetAccessToken();

            var response = Utilities.REST.RestHelper.Post(
                httpClient,
                expirationUrl,
                expirationPayload,
                accessToken);

            LogDebug($"Updated expiration date for version {version.VersionLabel} of list item {item.Id} in list {list.Title}");
        }

    }
}
