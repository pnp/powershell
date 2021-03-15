using PnP.Core.Model.SharePoint;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Unpublish, "PnPSyntexModel")]
    public class UnPublishSyntexModel : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = true)]
        public string ListWebUrl;

        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            var ctx = PnPConnection.Current.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                // Get the model we're publishing
                ISyntexModel modelToUnPublish = Model.GetSyntexModel();

                if (modelToUnPublish == null)
                {
                    throw new ArgumentException("Provide a valid model to unpublish");
                }

                // resolve the list 
                IList listToUnPublishModelFrom = null;
                using (var listContext = PnPConnection.Current.CloneContext(ListWebUrl))
                {
                    var pnpContext = PnPCoreSdk.Instance.GetPnPContext(listContext);
                    listToUnPublishModelFrom = List.GetList(pnpContext);
                }

                if (listToUnPublishModelFrom == null)
                {
                    throw new ArgumentException("Provide a valid list to unpublish the Syntex model from");
                }

                WriteObject(modelToUnPublish.UnPublishModel(listToUnPublishModelFrom));                

            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
