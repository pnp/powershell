using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModel")]
    public class GetSyntexModel : PnPWebCmdlet
    {

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var ctx = PnPConnection.Current.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                if (Identity != null)
                {
                    WriteObject(Identity.GetSyntexModel());
                }
                else
                {
                    var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                    var models = syntexContentCenter.GetSyntexModels();
                    WriteObject(models, true);
                }
            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
