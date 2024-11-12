using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq.Expressions;
using System;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.PowerShell.Commands.Attributes;
using System.Collections.Generic;
using System.Management.Automation.Language;
using System.Collections;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base;
using System.Linq;
using AngleSharp.Dom;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPList")]
    [OutputType(typeof(List))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Selected")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.ReadWrite.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Manage.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Write")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Manage")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    public class GetList : PnPWebRetrievalsCmdlet<List>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0), ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter ThrowExceptionIfListNotFound;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = [l => l.Id, l => l.BaseTemplate, l => l.OnQuickLaunch, l => l.DefaultViewUrl, l => l.Title, l => l.Hidden, l => l.RootFolder.ServerRelativeUrl];

            if (Identity != null)
            {
                var list = Identity.GetList(CurrentWeb);
                if (ThrowExceptionIfListNotFound && list == null)
                {
                    throw new PSArgumentException(string.Format(Resources.ListNotFound, Identity), nameof(Identity));
                }

                list?.EnsureProperties(RetrievalExpressions);
                WriteObject(list);
            }
            else
            {
                var query = CurrentWeb.Lists.IncludeWithDefaultProperties(RetrievalExpressions);
                var lists = ClientContext.LoadQuery(query);
                ClientContext.ExecuteQueryRetry();
                WriteObject(lists, true);
            }
        }
    }
}

