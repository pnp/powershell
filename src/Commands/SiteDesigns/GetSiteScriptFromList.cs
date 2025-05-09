﻿using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteScriptFromList", DefaultParameterSetName = ParameterSet_ByLIST)]
    [OutputType(typeof(string))]
    public class GetSiteScriptFromList : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_BYURL = "By Url";
        private const string ParameterSet_ByLIST = "By List";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYURL)]
        public string Url;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ByLIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            if(ParameterSpecified(nameof(List)))
            {
                ClientContext.Web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
                var siteUrl = ClientContext.Web.Url.Remove(ClientContext.Web.Url.Length - ClientContext.Web.ServerRelativeUrl.Length);
                Url = siteUrl + List.GetList(ClientContext.Web, null).RootFolder.ServerRelativeUrl;
            }

            LogDebug($"Getting Site Script from list {Url}");

            var script = Tenant.GetSiteScriptFromList(AdminContext, Url);
            AdminContext.ExecuteQueryRetry();
            WriteObject(script.Value);
        }
    }
}