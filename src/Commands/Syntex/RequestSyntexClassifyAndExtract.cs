using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.Syntex;

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsLifecycle.Request, "PnPSyntexClassifyAndExtract")]
    [OutputType(typeof(SyntexClassifyAndExtractResult))]
    public class RequestSyntexClassifyAndExtract : PnPWebCmdlet
    {
        const string ParameterSet_LIST = "List";
        const string Parameterset_FILE = "File";
        const string Parameterset_FOLDER = "Folder";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter Force = false;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter OffPeak = false;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_FILE)]
        public string FileUrl;

        [Parameter(Mandatory = false, ParameterSetName = Parameterset_FILE)]
        public PnPBatch Batch;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_FOLDER)]
        public FolderPipeBind Folder;


        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            if (ParameterSpecified(nameof(List)))
            {
                IList list = List.GetList(ctx);

                // If a list is above 5K we default to off-peak processing
                if (list.ItemCount > 5000)
                {
                    OffPeak = true;
                }

                if (OffPeak)
                {
                    var classifyAndExtractResult = list.ClassifyAndExtractOffPeak();
                    WriteObject(new SyntexClassifyAndExtractResult()
                    {
                        Created = classifyAndExtractResult.Created,
                        DeliverDate = classifyAndExtractResult.DeliverDate,
                        ErrorMessage = classifyAndExtractResult.ErrorMessage,
                        Id = classifyAndExtractResult.Id,
                        Status = classifyAndExtractResult.Status,
                        StatusCode = classifyAndExtractResult.StatusCode,
                        TargetServerRelativeUrl = classifyAndExtractResult.TargetServerRelativeUrl,
                        TargetSiteUrl = classifyAndExtractResult.TargetSiteUrl,
                        TargetWebServerRelativeUrl = classifyAndExtractResult.TargetWebServerRelativeUrl,
                        WorkItemType = classifyAndExtractResult.WorkItemType,
                    });
                }
                else
                {
                    var classifyAndExtractResults = list.ClassifyAndExtract(force: Force.IsPresent);

                    List<Model.Syntex.SyntexClassifyAndExtractResult> classifyAndExtractResultsOutput = new List<Model.Syntex.SyntexClassifyAndExtractResult>();
                    if (classifyAndExtractResults != null && classifyAndExtractResults.Any())
                    {
                        foreach (var classifyAndExtractResult in classifyAndExtractResults)
                        {
                            classifyAndExtractResultsOutput.Add(new SyntexClassifyAndExtractResult()
                            {
                                Created = classifyAndExtractResult.Created,
                                DeliverDate = classifyAndExtractResult.DeliverDate,
                                ErrorMessage = classifyAndExtractResult.ErrorMessage,
                                Id = classifyAndExtractResult.Id,
                                Status = classifyAndExtractResult.Status,
                                StatusCode = classifyAndExtractResult.StatusCode,
                                TargetServerRelativeUrl = classifyAndExtractResult.TargetServerRelativeUrl,
                                TargetSiteUrl = classifyAndExtractResult.TargetSiteUrl,
                                TargetWebServerRelativeUrl = classifyAndExtractResult.TargetWebServerRelativeUrl,
                                WorkItemType = classifyAndExtractResult.WorkItemType,
                            });
                        }
                    }
                    WriteObject(classifyAndExtractResultsOutput, true);
                }

            }
            else if (ParameterSpecified(nameof(Folder)))
            {
                IFolder folder = Folder.GetFolder(ctx);
                var classifyAndExtractResult = folder.ClassifyAndExtractOffPeak();
                WriteObject(new SyntexClassifyAndExtractResult()
                {
                    Created = classifyAndExtractResult.Created,
                    DeliverDate = classifyAndExtractResult.DeliverDate,
                    ErrorMessage = classifyAndExtractResult.ErrorMessage,
                    Id = classifyAndExtractResult.Id,
                    Status = classifyAndExtractResult.Status,
                    StatusCode = classifyAndExtractResult.StatusCode,
                    TargetServerRelativeUrl = classifyAndExtractResult.TargetServerRelativeUrl,
                    TargetSiteUrl = classifyAndExtractResult.TargetSiteUrl,
                    TargetWebServerRelativeUrl = classifyAndExtractResult.TargetWebServerRelativeUrl,
                    WorkItemType = classifyAndExtractResult.WorkItemType,
                });
            }
            else
            {
                ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

                if (!FileUrl.ToLower().StartsWith(ctx.Web.ServerRelativeUrl.ToLower()))
                {
                    serverRelativeUrl = UrlUtility.Combine(ctx.Web.ServerRelativeUrl, FileUrl);
                }
                else
                {
                    serverRelativeUrl = FileUrl;
                }

                var file = ctx.Web.GetFileByServerRelativeUrl(serverRelativeUrl);

                if (ParameterSpecified(nameof(Batch)))
                {
                    file.ClassifyAndExtractBatch(Batch.Batch);
                }
                else
                {
                    var classifyAndExtractResult = file.ClassifyAndExtract();

                    if (classifyAndExtractResult != null)
                    {
                        WriteObject(new SyntexClassifyAndExtractResult()
                        {
                            Created = classifyAndExtractResult.Created,
                            DeliverDate = classifyAndExtractResult.DeliverDate,
                            ErrorMessage = classifyAndExtractResult.ErrorMessage,
                            Id = classifyAndExtractResult.Id,
                            Status = classifyAndExtractResult.Status,
                            StatusCode = classifyAndExtractResult.StatusCode,
                            TargetServerRelativeUrl = classifyAndExtractResult.TargetServerRelativeUrl,
                            TargetSiteUrl = classifyAndExtractResult.TargetSiteUrl,
                            TargetWebServerRelativeUrl = classifyAndExtractResult.TargetWebServerRelativeUrl,
                            WorkItemType = classifyAndExtractResult.WorkItemType,
                        });
                    }
                }
            }
        }
    }
}
