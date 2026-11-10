using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.New, "PnPBatch")]
    [OutputType(typeof(PnPBatch))]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet creates an empty batch object and performs no request. The permissions needed follow from the cmdlets that are added to the batch.")]
    public class NewBatch : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter RetainRequests;
        protected override void ExecuteCmdlet()
        {
            var batch = new PnPBatch(Connection.PnPContext, RetainRequests);
            WriteObject(batch);
        }
    }
}