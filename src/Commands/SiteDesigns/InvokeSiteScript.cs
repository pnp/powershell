using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteScript", DefaultParameterSetName = ParameterSet_SCRIPTCONTENTS)]
    public class InvokeSiteScript : PnPWebCmdlet
    {
        private const string ParameterSet_SITESCRIPTREFERENCE = "By Site Script Reference";
        private const string ParameterSet_SCRIPTCONTENTS = "By providing script contents";

        [Parameter(ParameterSetName = ParameterSet_SITESCRIPTREFERENCE, Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(ParameterSetName = ParameterSet_SITESCRIPTREFERENCE)]
        [Parameter(ParameterSetName = ParameterSet_SCRIPTCONTENTS)]
        [Parameter(Mandatory = false)]
        public string WebUrl;
        
        [Parameter(ParameterSetName = ParameterSet_SCRIPTCONTENTS)]
        [Parameter(Mandatory = false)]
        public string Script;

        protected override void ExecuteCmdlet()
        {
            // var url = CurrentWeb.EnsureProperty(w => w.Url);
            // var tenantUrl = UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            // using (var tenantContext = ClientContext.Clone(tenantUrl))
            // {
            //     var tenant = new Tenant(tenantContext);

            //     // Retrieve the site scripts
            //     var scripts = Identity.GetTenantSiteScript(tenant);

            //     if (scripts == null || scripts.Length == 0)
            //     {
            //         throw new PSArgumentException("No site scripts found matching the identity provided through Identity", nameof(Identity));
            //     }

            //     foreach (var script in scripts)
            //     {
            //         WriteVerbose($"Invoking site script '{script.Title}' ({script.Id})");

            //         var results = tenant.ApplyListDesign(WebUrl, script.Id);
            //         tenantContext.Load(results);
            //         tenantContext.ExecuteQueryRetry();
            //         WriteObject(results, true);
            //     }

            var hostUrl = ParameterSpecified(nameof(WebUrl)) ? WebUrl : CurrentWeb.Url;

            

            if(ParameterSpecified(nameof(Script)))
            {
                var escapedScript = System.Text.RegularExpressions.Regex.Replace(Script.Replace("\\\"", "\\\\\\\""), "(?<!\\\\)\"", "\\\"", System.Text.RegularExpressions.RegexOptions.Singleline);
                var content = new StringContent(string.Concat(@"{ ""script"": """, escapedScript, " \"}"));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var results = GraphHelper.PostAsync(this.HttpClient, $"{hostUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ExecuteTemplateScript()", AccessToken, content).GetAwaiter().GetResult();

                WriteObject(results);
            }
            else
            {

            }

            // var record = new PSObject();B

            // foreach (var item in results.Items)
            // {
            //     record.Properties.Add(new PSVariableProperty(new PSVariable(item.Key.Split('|')[1], item.Value)));
            // }
            // WriteObject(record);                
        }
    }
}