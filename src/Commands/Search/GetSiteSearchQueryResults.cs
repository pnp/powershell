using System.Management.Automation;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteSearchQueryResults", DefaultParameterSetName = "Limit")]
    public class GetSiteSearchQueryResults : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Query = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = "Limit")]
        public int StartRow = 0;

        [Parameter(Mandatory = false, ParameterSetName = "Limit")]
        [ValidateRange(0, 500)]
        public int MaxResults = 500;

        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public SwitchParameter All;

        protected override void ExecuteCmdlet()
        {
            var queryCmdLet = new SubmitSearchQuery
            {
                Connection = Connection,
                StartRow = StartRow,
                MaxResults = MaxResults,
                All = All
            };
            
            var query = "contentclass:STS_Site";
            if (!string.IsNullOrEmpty(Query))
            {
                query = $"{query} AND {Query}";
            }
            queryCmdLet.Query = query;

            queryCmdLet.SelectProperties = new[] {"Title","SPSiteUrl","Description","WebTemplate"};
            queryCmdLet.SortList = new System.Collections.Hashtable
            {
                { "SPSiteUrl", "ascending" }
            };
            queryCmdLet.RelevantResults = true;

            var res = queryCmdLet.Run();

            var dynamicList = new List<dynamic>();
            foreach (var row in res)
            {
                var obj = row as PSObject;                
                dynamicList.Add(
                    new
                    {
                        Title = obj.Properties["Title"]?.Value ?? "",
                        Url = obj.Properties["SPSiteUrl"]?.Value ?? "",
                        Description = obj.Properties["Description"]?.Value ?? "",
                        WebTemplate = obj.Properties["WebTemplate"]?.Value ?? ""
                    });
            }

            WriteObject(dynamicList, true);
        }
    }
}