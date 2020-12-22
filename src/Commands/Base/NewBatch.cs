using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.New, "PnPBatch")]
    public class NewBatch : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(PnPContext.NewBatch());
        }
    }
}