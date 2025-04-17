using PnP.Core.Model.SharePoint;
using PnP.Framework;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.Syntex;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Publish, "PnPSyntexModel")]
    [OutputType(typeof(SyntexPublicationResult))]
    public class PublishSyntexModel : PnPWebCmdlet
    {
        const string ParameterSet_SINGLE = "Single";
        const string Parameterset_BATCHED = "Batched";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        public string ListWebUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public MachineLearningPublicationViewOption PublicationViewOption = MachineLearningPublicationViewOption.NewViewAsDefault;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetSiteUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetWebServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetLibraryServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        [ValidateNotNull]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            var ctx = Connection.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                if (ParameterSpecified(nameof(Batch)))
                {
                    // Get the model we're publishing
                    ISyntexModel modelToPublish = Model.GetSyntexModel(Batch, Connection);

                    if (modelToPublish == null)
                    {
                        throw new PSArgumentException("Provide a valid model to publish");
                    }

                    modelToPublish.PublishModelBatch(Batch.Batch, new SyntexModelPublishOptions() 
                    {
                        TargetSiteUrl = TargetSiteUrl,
                        TargetWebServerRelativeUrl = TargetWebServerRelativeUrl,
                        TargetLibraryServerRelativeUrl = TargetLibraryServerRelativeUrl,
                        ViewOption = PublicationViewOption
                    });
                }
                else
                {
                    // Get the model we're publishing
                    ISyntexModel modelToPublish = Model.GetSyntexModel(Connection);

                    if (modelToPublish == null)
                    {
                        throw new PSArgumentException("Provide a valid model to publish");
                    }

                    // resolve the list 
                    IList listToPublishModelTo = null;
                    using (var listContext = Connection.CloneContext(ListWebUrl))
                    {
                        var pnpContext = PnPCoreSdk.Instance.GetPnPContext(listContext);
                        listToPublishModelTo = List.GetList(pnpContext);
                    }

                    if (listToPublishModelTo == null)
                    {
                        throw new PSArgumentException("Provide a valid list to publish the Syntex model to");
                    }

                    var publishResult = modelToPublish.PublishModel(listToPublishModelTo, PublicationViewOption);
                    if (publishResult != null)
                    {
                        WriteObject(new SyntexPublicationResult()
                        {
                            ErrorMessage = publishResult.ErrorMessage,
                            StatusCode = publishResult.StatusCode,
                            Publication = new Model.Syntex.SyntexModelPublication()
                            {
                                ModelUniqueId = publishResult.Publication.ModelUniqueId,
                                TargetSiteUrl = publishResult.Publication.TargetSiteUrl,
                                TargetWebServerRelativeUrl = publishResult.Publication.TargetWebServerRelativeUrl,
                                TargetLibraryServerRelativeUrl = publishResult.Publication.TargetLibraryServerRelativeUrl,
                                ViewOption = publishResult.Publication.ViewOption
                            }
                        });
                    }
                }
            }
            else
            {
                LogWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
