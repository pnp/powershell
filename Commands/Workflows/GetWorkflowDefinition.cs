using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Get, "PnPWorkflowDefinition")]
    public class GetWorkflowDefinition : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name;

        [Parameter(Mandatory = false)]
        public SwitchParameter PublishedOnly = true;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrEmpty(Name))
            {
                var servicesManager = new WorkflowServicesManager(ClientContext, SelectedWeb);
                var deploymentService = servicesManager.GetWorkflowDeploymentService();
                var definitions = deploymentService.EnumerateDefinitions(PublishedOnly);

                ClientContext.Load(definitions);

                ClientContext.ExecuteQueryRetry();
                WriteObject(definitions, true);
            }
            else
            {
                WriteObject(SelectedWeb.GetWorkflowDefinition(Name, PublishedOnly));
            }
        }
    }

}
