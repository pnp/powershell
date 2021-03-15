using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModelPublication")]
    public class GetSyntexModelPublication : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        protected override void ExecuteCmdlet()
        {
            var ctx = PnPConnection.Current.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                // Get the model we're publishing
                ISyntexModel model = Model.GetSyntexModel();

                if (model == null)
                {
                    throw new ArgumentException("Provide a valid model to get publications for");
                }

                WriteObject(model.GetModelPublications());
            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
