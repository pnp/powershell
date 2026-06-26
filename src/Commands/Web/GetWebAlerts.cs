using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPWebAlert", DefaultParameterSetName = ParameterSet_All)]
    [OutputType(typeof(WebAlert))]
    public class GetWebAlert : PnPWebCmdlet
    {
        private const string ParameterSet_ByListId = "By List Id";
        private const string ParameterSet_ByListUrl = "By List Url";
        private const string ParameterSet_ByListTitle = "By List Title";
        private const string ParameterSet_All = "All";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ByListId)]
        public Guid ListId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ByListUrl)]
        public string ListUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ByListTitle)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public string ListTitle;

        [Parameter(Mandatory = false)]
        public string UserName;

        [Parameter(Mandatory = false)]
        public Guid UserId;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(UserName)) && ParameterSpecified(nameof(UserId)))
            {
                throw new PSArgumentException("Specify either UserName or UserId, but not both.");
            }

            var webUrl = CurrentWeb.EnsureProperty(w => w.Url);

            var requestUrl = $"{webUrl}/_api/web/alerts?$expand=List,User,List/Rootfolder,Item&$select=*,List/Id,List/Title,List/Rootfolder/ServerRelativeUrl,Item/ID,Item/FileRef,Item/Guid";

            var filters = new List<string>();
            Guid? listIdValue = null;

            if (ParameterSpecified(nameof(ListId)) && ListId != Guid.Empty)
            {
                listIdValue = ListId;
            }
            else if (ParameterSpecified(nameof(ListUrl)))
            {
                var list = CurrentWeb.GetListByUrl(ListUrl);
                if (list != null)
                {
                    CurrentWeb.Context.Load(list, l => l.Id);
                    CurrentWeb.Context.ExecuteQueryRetry();
                    listIdValue = list.Id;
                }
            }
            else if (ParameterSpecified(nameof(ListTitle)))
            {
                var list = CurrentWeb.GetListByTitle(ListTitle);
                if (list != null)
                {
                    CurrentWeb.Context.Load(list, l => l.Id);
                    CurrentWeb.Context.ExecuteQueryRetry();
                    listIdValue = list.Id;
                }
            }

            if (listIdValue.HasValue)
            {
                filters.Add($"List/Id eq guid'{listIdValue}'");
            }


            if (ParameterSpecified(nameof(UserName)) && !string.IsNullOrEmpty(UserName))
            {
                filters.Add($"User/UserPrincipalName eq '{Uri.EscapeDataString(UserName)}'");
            }
            else if (ParameterSpecified(nameof(UserId)) && UserId != Guid.Empty)
            {
                var userPrincipalName = GetUserPrincipalNameByUserId(UserId);
                if (!string.IsNullOrEmpty(userPrincipalName.ToLower()))
                {
                    filters.Add($"User/UserPrincipalName eq '{Uri.EscapeDataString(userPrincipalName)}'");
                }
            }

            if (filters.Any())
            {
                requestUrl += $"&$filter={string.Join(" and ", filters)}";
            }

            try
            {
                WriteVerbose($"Retrieving alerts from '{webUrl}'...");

                var alerts = RestHelper.Get<RestResultCollection<WebAlert>>(
                    Connection.HttpClient,
                    requestUrl,
                    ClientContext,
                    false);

                if (alerts?.Items != null)
                {
                    WriteObject(alerts.Items, true);
                }
            }
            catch (Exception ex)
            {
                throw new PSInvalidOperationException($"Failed to retrieve alerts: {ex.Message}", ex);
            }
        }

        private string GetUserPrincipalNameByUserId(Guid userId)
        {
            try
            {
                var result = RestHelper.Get(
                    Connection.HttpClient,
                    $"https://{Connection.GraphEndPoint}/v1.0/users/{userId}?$select=userPrincipalName",
                    GraphAccessToken);  

                if (!string.IsNullOrEmpty(result))
                {
                    var userInfo = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(result);
                    if (userInfo.TryGetProperty("userPrincipalName", out var upnElement))
                    {
                        return upnElement.GetString();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteWarning($"Could not retrieve user principal name for user ID '{userId}': {ex.Message}");
            }
            return null;
        }
    }
}
