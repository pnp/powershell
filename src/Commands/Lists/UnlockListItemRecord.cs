using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Unlock, "PnPListItemRecord")]
    public class PnPListItemRecord : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            var item = Identity.GetListItem(list);

            WriteVerbose($"Unlock the record {Identity.Id} from list {List}");

            ClientContext.Load(ClientContext.Site, s => s.Url);
            ClientContext.ExecuteQueryRetry();

            var payload = new
            {
                listUrl = list.RootFolder.ServerRelativeUrl,
                itemId = Identity.Id
            };

            try
            {
                RestHelper.Post(Connection.HttpClient, $"{ClientContext.Site.Url}/_api/SP.CompliancePolicy.SPPolicyStoreProxy.UnlockRecordItem()", AccessToken, payload);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Unlock the record {Identity.Id} from list {List} with exception: {ex.Message}", ex);
            }
        }
    }
}