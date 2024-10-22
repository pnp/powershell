using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPBatch", DefaultParameterSetName = PARAMETERSET_Default)]
    [OutputType(typeof(BatchResult), ParameterSetName = new[] { PARAMETERSET_Default })]
    [OutputType(typeof(BatchResult), typeof(Model.BatchRequest), ParameterSetName = new[] { PARAMETERSET_Detailed })]
    public class InvokeBatch : PnPWebCmdlet
    {
        public const string PARAMETERSET_Default = "Default";
        public const string PARAMETERSET_Detailed = "Detailed";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_Default)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_Detailed)]
        public PnPBatch Batch;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Default)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Detailed)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Detailed)]
        public SwitchParameter Details;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Default)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Detailed)]
        public SwitchParameter StopOnException;

        protected override void ExecuteCmdlet()
        {
            bool batchExecuted = Batch.Executed;
            if (batchExecuted)
            {
                if (Force || ShouldContinue($"Batch has been invoked before with {Batch.Requests.Count} requests. Invoke again?", Properties.Resources.Confirm))
                {
                    batchExecuted = false;
                }
            }
            if (!batchExecuted)
            {
                var results = Batch.Execute(StopOnException);
                if (results != null && results.Any())
                {
                    var resultList = new List<BatchResult>();
                    foreach (var result in results)
                    {
                        resultList.Add(new BatchResult() { ErrorMessage = result.Error.Message, ResponseCode = result.Error.HttpResponseCode, Request = result.ApiRequest });
                    }
                    WriteObject(resultList, true);
                }
                if (Details)
                {
                    var requests = new List<Model.BatchRequest>();
                    foreach (var request in Batch.Requests)
                    {
                        requests.Add(new Model.BatchRequest() { HttpStatusCode = request.Value.ResponseHttpStatusCode, ResponseJson = request.Value.ResponseJson });
                    }
                    WriteObject(requests);
                }
            }
        }
    }

    public class BatchResult
    {
        public string ErrorMessage { get; set; }
        public int ResponseCode { get; set; }
        public string Request { get; set; }
    }
}