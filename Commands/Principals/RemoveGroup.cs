using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPGroup", DefaultParameterSetName = "All")]
    public class RemoveGroup : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public GroupPipeBind Identity = new GroupPipeBind();

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            Group group = Identity.GetGroup(SelectedWeb);
            if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveGroup0, group.Title), Properties.Resources.Confirm))
            {
                SelectedWeb.SiteGroups.Remove(group);

                ClientContext.ExecuteQueryRetry();
            }
        }
    }



}
