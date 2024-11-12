using PnP.Core.Model.SharePoint;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.Syntex;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Unpublish, "PnPSyntexModel")]
    [OutputType(typeof(SyntexPublicationResult))]
    public class UnPublishSyntexModel : PnPWebCmdlet
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

                    modelToPublish.UnPublishModelBatch(Batch.Batch, new SyntexModelUnPublishOptions()
                    {
                        TargetSiteUrl = TargetSiteUrl,
                        TargetWebServerRelativeUrl = TargetWebServerRelativeUrl,
                        TargetLibraryServerRelativeUrl = TargetLibraryServerRelativeUrl,
                    });
                }
                else
                {
                    // Get the model we're publishing
                    ISyntexModel modelToUnPublish = Model.GetSyntexModel(Connection);

                    if (modelToUnPublish == null)
                    {
                        throw new PSArgumentException("Provide a valid model to unpublish");
                    }

                    // resolve the list 
                    IList listToUnPublishModelFrom = null;
                    using (var listContext = Connection.CloneContext(ListWebUrl))
                    {
                        var pnpContext = PnPCoreSdk.Instance.GetPnPContext(listContext);
                        listToUnPublishModelFrom = List.GetList(pnpContext);
                    }

                    if (listToUnPublishModelFrom == null)
                    {
                        throw new PSArgumentException("Provide a valid list to unpublish the Syntex model from");
                    }

                    var unPublishResult = modelToUnPublish.UnPublishModel(listToUnPublishModelFrom);
                    if (unPublishResult != null)
                    {
                        WriteObject(new SyntexPublicationResult()
                        {
                            ErrorMessage = unPublishResult.ErrorMessage,
                            StatusCode = unPublishResult.StatusCode,
                            Publication = new Model.Syntex.SyntexModelPublication()
                            {
                                ModelUniqueId = unPublishResult.Publication.ModelUniqueId,
                                TargetSiteUrl = unPublishResult.Publication.TargetSiteUrl,
                                TargetWebServerRelativeUrl = unPublishResult.Publication.TargetWebServerRelativeUrl,
                                TargetLibraryServerRelativeUrl = unPublishResult.Publication.TargetLibraryServerRelativeUrl,
                                ViewOption = unPublishResult.Publication.ViewOption
                            }
                        });
                    }
                }
            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
