using PnP.Core.Model.SharePoint;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Publish, "PnPSyntexModel")]
    public class PublishSyntexModel : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = true)]
        public string ListWebUrl;

        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public MachineLearningPublicationViewOption PublicationViewOption = MachineLearningPublicationViewOption.NewViewAsDefault;

        protected override void ExecuteCmdlet()
        {
            var ctx = PnPConnection.Current.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                // Get the model we're publishing
                ISyntexModel modelToPublish = Model.GetSyntexModel();

                if (modelToPublish == null)
                {
                    throw new ArgumentException("Provide a valid model to publish");
                }

                // resolve the list 
                IList listToPublishModelTo = null;
                using (var listContext = PnPConnection.Current.CloneContext(ListWebUrl))
                {
                    var pnpContext = PnPCoreSdk.Instance.GetPnPContext(listContext);
                    listToPublishModelTo = List.GetList(pnpContext);
                }

                if (listToPublishModelTo == null)
                {
                    throw new ArgumentException("Provide a valid list to publish the Syntex model to");
                }

                WriteObject(modelToPublish.PublishModel(listToPublishModelTo, PublicationViewOption));                

            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
