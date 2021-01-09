using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.New, "PnPBatch")]
    public class NewBatch : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var batch = new PnPBatch(PnPContext);
            WriteObject(batch);
        }
    }
}