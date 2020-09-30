using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;


namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Add, "PnPWorkflowDefinition")]
    public class AddWorkflowDefinition : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public WorkflowDefinition Definition;

        [Parameter(Mandatory = false)]
        public SwitchParameter DoNotPublish;
        protected override void ExecuteCmdlet()
        {
            WriteObject(SelectedWeb.AddWorkflowDefinition(Definition,!DoNotPublish));
        }
    }

}
