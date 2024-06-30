using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using PnP.PowerShell.Commands.Utilities.REST;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Text.Json.Nodes;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Copy, "PnPList", DefaultParameterSetName = ParameterSet_TOCURRENTSITEBYPIPE)]
    [OutputType(typeof(List))]
    public class CopyList : PnPWebCmdlet
    {
        private const string ParameterSet_LISTBYPIPE = "By piping in a list";
        private const string ParameterSet_LISTBYURL = "By providing a list URL";
        private const string ParameterSet_TOCURRENTSITEBYURL = "To the current site by URL";
        private const string ParameterSet_TOCURRENTSITEBYPIPE = "To the current site by pipe";

        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYURL, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYPIPE, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYPIPE, Mandatory = false)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYURL, Mandatory = false)]
        public string Title;

        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYPIPE, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYPIPE, Mandatory = true, ValueFromPipeline = true)]
        public ListPipeBind Identity;

        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYURL, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYURL, Mandatory = true)]
        public string SourceListUrl;

        [Parameter(ParameterSetName = ParameterSet_LISTBYPIPE, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYURL, Mandatory = true)]
        public string DestinationWebUrl;              

        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYPIPE, Mandatory = false)]
        [Parameter(ParameterSetName = ParameterSet_TOCURRENTSITEBYURL, Mandatory = false)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYPIPE, Mandatory = false)]
        [Parameter(ParameterSetName = ParameterSet_LISTBYURL, Mandatory = false)]
        public SwitchParameter WhatIf;
        
        protected override void ExecuteCmdlet()
        {
            if(ParameterSpecified(nameof(Identity)))
            {
                // Retrieve the list to copy
                WriteVerbose($"Looking up list provided through {nameof(Identity)}");
                var list = Identity.GetList(ClientContext.Web);

                if(list == null)
                {
                    throw new PSArgumentException($"List provided through {nameof(Identity)} could not be found", nameof(Identity));
                }

                // Define the full URL to the list to copy
                var hostUri = new Uri(Connection.Url);
                SourceListUrl = $"{hostUri.Scheme}://{hostUri.Authority}{list.RootFolder.ServerRelativeUrl}";
            }

            // Generate a site script from the list that needs to be copied
            WriteVerbose($"Generating script from list at {SourceListUrl}");
            var generatedScript = RestHelper.Post<RestResult<string>>(Connection.HttpClient, $"{Connection.Url}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScriptFromList()", ClientContext, new { listUrl = SourceListUrl});

            // Take the site script of the list to copy
            var script = generatedScript.Content;

            if (ParameterSpecified(nameof(Title)) && !string.IsNullOrWhiteSpace(Title))
            {
                // Update the list name in the site script in the first *_listName binding parameter 
                WriteVerbose($"Setting list title to '{Title}'");

                JsonNode scriptAsJson = JsonNode.Parse(script);

                var listNameElement = scriptAsJson["bindings"].AsObject().Where(b => b.Key.EndsWith("_listName")).First();
                listNameElement.Value["defaultValue"] = Title;

                script = scriptAsJson.ToJsonString();
            }

            // Check if we need to set the destination to the current site
            if(ParameterSetName == ParameterSet_TOCURRENTSITEBYPIPE || ParameterSetName == ParameterSet_TOCURRENTSITEBYURL)
            {
                DestinationWebUrl = Connection.Url;
            }

            if(ParameterSpecified(nameof(WhatIf)))
            {
                WriteVerbose($"Skipping execution of site script to site at {DestinationWebUrl} due to {nameof(WhatIf)} flag being provided");
                return;
            }

            // Execute site script on destination site so the list will be created
            WriteVerbose($"Executing site script to site at {DestinationWebUrl}");
            var actionResults = RestHelper.Post<RestResultCollection<InvokeSiteScriptActionResponse>>(Connection.HttpClient, $"{DestinationWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ExecuteTemplateScript()", ClientContext, new { script = script});
            
            // Ensure site script actions have been executed
            if(actionResults.Items.Count() == 0)
            {
                throw new PSInvalidOperationException($"List copy failed. No site script actions have been executed.");
            }

            // Display the results of each action in verbose
            foreach(var actionResult in actionResults.Items)
            {
                WriteVerbose($"Action {actionResult.Title} {(actionResult.ErrorCode != 0 ? $"failed: {actionResult.OutcomeText}" : "succeeded")}");
            }

            // Ensure the list creation succeeded
            if(actionResults.Items.ElementAt(0).ErrorCode != 0)
            {
                throw new PSInvalidOperationException($"List copy failed with error {actionResults.Items.ElementAt(0).OutcomeText}");
            }

            //Create a ClientContext to the web where the list has been created
            var destinationContext = ClientContext.Clone(DestinationWebUrl);

            // Retrieve the newly created list
            var newListId = actionResults.Items.ElementAt(0).TargetId;

            WriteVerbose($"Retrieving newly created list hosted in {DestinationWebUrl} with ID {newListId}");
            var createdList = destinationContext.Web.Lists.GetById(Guid.Parse(newListId));
            destinationContext.Load(createdList, l => l.Id, l => l.BaseTemplate, l => l.OnQuickLaunch, l => l.DefaultViewUrl, l => l.Title, l => l.Hidden, l => l.ContentTypesEnabled, l => l.RootFolder.ServerRelativeUrl);
            destinationContext.ExecuteQueryRetry();

            // Return the new list
            WriteObject(createdList);
        }
    }
}
