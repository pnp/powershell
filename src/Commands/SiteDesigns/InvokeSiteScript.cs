using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteScript", DefaultParameterSetName = ParameterSet_SCRIPTCONTENTS)]
    [OutputType(typeof(IEnumerable<InvokeSiteScriptActionResponse>))]
    public class InvokeSiteScript : PnPWebCmdlet
    {
        private const string ParameterSet_SITESCRIPTREFERENCE = "By Site Script Reference";
        private const string ParameterSet_SCRIPTCONTENTS = "By providing script contents";

        [Parameter(ParameterSetName = ParameterSet_SITESCRIPTREFERENCE, Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(ParameterSetName = ParameterSet_SITESCRIPTREFERENCE, Mandatory = true)]
        [Parameter(ParameterSetName = ParameterSet_SCRIPTCONTENTS, Mandatory = false)]
        public string WebUrl;
        
        [Parameter(ParameterSetName = ParameterSet_SCRIPTCONTENTS, Mandatory = true)]
        public string Script;

        [Parameter(ParameterSetName = ParameterSet_SITESCRIPTREFERENCE, Mandatory = false)]
        [Parameter(ParameterSetName = ParameterSet_SCRIPTCONTENTS, Mandatory = false)]
        public SwitchParameter WhatIf;

        protected override void ExecuteCmdlet()
        {
            string hostUrl;
            if(ParameterSpecified(nameof(WebUrl)) && !string.IsNullOrWhiteSpace(WebUrl))
            {
                hostUrl = WebUrl;
            }
            else
            {
                CurrentWeb.EnsureProperty(w => w.Url);
                hostUrl = CurrentWeb.Url;
            }

            LogDebug($"Site scripts will be applied to site {hostUrl}");

            IEnumerable<InvokeSiteScriptActionResponse> result = null;
            switch(ParameterSetName)
            {
                case ParameterSet_SCRIPTCONTENTS:
                    if(ParameterSpecified(nameof(WhatIf)))
                    {
                        LogDebug($"Provided Site Script through {nameof(Script)} will not be executed due to {nameof(WhatIf)} option being provided");
                    }
                    else
                    {
                        LogDebug($"Executing provided script");
                        result = Utilities.SiteTemplates.InvokeSiteScript(SharePointRequestHelper, Script, hostUrl).Items;
                    }
                    break;

                case ParameterSet_SITESCRIPTREFERENCE:
                    // Try to create an admin context from the current context
                    var tenant = new Tenant(ClientContext);
                    var scripts = Identity.GetTenantSiteScript(tenant);

                    if (scripts == null || scripts.Length == 0)
                    {
                        throw new PSArgumentException($"No site scripts found matching the identity provided through {nameof(Identity)}", nameof(Identity));
                    }

                    // Execute each of the Site Scripts that have been found
                    foreach (var script in scripts)
                    {
                        script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description, s => s.IsSiteScriptPackage);

                        if(ParameterSpecified(nameof(WhatIf)))
                        {
                            LogDebug($"Site script '{script.Title}' ({script.Id}) will not be executed due to {nameof(WhatIf)} option being provided");
                        }
                        else
                        {
                            LogDebug($"Executing site script '{script.Title}' ({script.Id})");
                            result =Utilities.SiteTemplates.InvokeSiteScript(SharePointRequestHelper, script, hostUrl).Items;
                        }
                    }
                    break;
            }

            // Only if there are results, show them
            if (result != null)
            {
                LogDebug($"Site script result: {result.Count(r => r.ErrorCode == 0)} actions successful, {result.Count(r => r.ErrorCode != 0)} failed");
                WriteObject(result, true);
            }
        }
    }
}