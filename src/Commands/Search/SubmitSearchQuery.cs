using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsLifecycle.Submit, "PnPSearchQuery", DefaultParameterSetName = "Limit")]
    [Alias("Invoke-PnPSearchQuery")]
    public class SubmitSearchQuery : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Query = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = "Limit")]
        public int StartRow = 0;

        [Parameter(Mandatory = false, ParameterSetName = "Limit")]
        [ValidateRange(0, 500)]
        public int MaxResults = 500;

        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool TrimDuplicates = false;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Hashtable Properties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Refiners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int Culture;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string QueryTemplate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string[] SelectProperties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string[] RefinementFilters;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Hashtable SortList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string RankingModelId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ClientType = "PnP";

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string CollapseSpecification;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string HiddenConstraints;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int TimeZoneId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool EnablePhonetic;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool EnableStemming;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool EnableQueryRules;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Guid SourceId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool ProcessBestBets;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool ProcessPersonalFavorites;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter RelevantResults;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int RetryCount = 0;

        internal IEnumerable<object> Run()
        {
            int startRow = StartRow;
            int rowLimit = MaxResults;
            // Used for internal Microsoft telemetry
            string clientFunction = "Search";
            if (All.IsPresent)
            {
                clientFunction = "Scanner";
                startRow = 0;
                rowLimit = 500;
                SortList = new Hashtable()
                    {
                        {"[DocId]", "ascending"}    // Special optimized sorting when iterating all items
                    };
            }

            var currentCount = 0;
            string lastDocId = "0";
            PnPResultTableCollection finalResults = null;
            do
            {
                KeywordQuery keywordQuery = CreateKeywordQuery(clientFunction);
                keywordQuery.StartRow = startRow;
                keywordQuery.RowLimit = rowLimit;

                if (All.IsPresent)
                {
                    if (currentCount != 0)
                    {
                        keywordQuery.Refiners = null; // Only need to set on first page for auto paging
                    }
                    keywordQuery.StartRow = 0;
                    keywordQuery.QueryText += " IndexDocId>" + lastDocId;
                }

                // We'll always try at least once, even if RetryCount is 0 (default)
                for (var iterator = 0; iterator <= RetryCount; iterator++)
                {
                    try
                    {
                        var searchExec = new SearchExecutor(ClientContext);
                        var results = searchExec.ExecuteQuery(keywordQuery);
                        ClientContext.ExecuteQueryRetry();

                        if (results.Value != null)
                        {
                            if (finalResults == null)
                            {
                                finalResults = (PnPResultTableCollection)results.Value;
                                foreach (ResultTable resultTable in results.Value)
                                {
                                    if (resultTable.TableType == "RelevantResults")
                                    {
                                        currentCount = resultTable.RowCount;
                                        if (currentCount > 0)
                                        {
                                            lastDocId = resultTable.ResultRows.Last()["DocId"].ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // we're in paging mode
                                foreach (ResultTable resultTable in results.Value)
                                {
                                    PnPResultTable pnpResultTable = (PnPResultTable)resultTable;
                                    var existingTable = finalResults.SingleOrDefault(t => t.TableType == resultTable.TableType);
                                    if (existingTable != null)
                                    {
                                        existingTable.ResultRows.AddRange(pnpResultTable.ResultRows);
                                    }
                                    else
                                    {
                                        finalResults.Add(pnpResultTable);
                                    }
                                    if (pnpResultTable.TableType == "RelevantResults")
                                    {
                                        currentCount = resultTable.RowCount;
                                        if (currentCount > 0)
                                        {
                                            lastDocId = resultTable.ResultRows.Last()["DocId"].ToString();
                                        }
                                    }
                                }
                            }
                        }

                        // If we were successful (and didn't end in the catch block), we don't want to retry -> break out of retry loop
                        break;
                    }
                    // If we're not retrying, or if we're on the last retry, don't catch the exception
                    catch (Exception ex) when (RetryCount > 0 && iterator < RetryCount)
                    {   
                        // Swallow the exception and retry (with incremental backoff)
                        Thread.Sleep(4000 * (iterator+1));

                        continue;
                    }
                }
                startRow += rowLimit;
            } while (currentCount == rowLimit && All.IsPresent);

            if (!RelevantResults.IsPresent)
            {
                return finalResults;
            }
            else
            {
                var results = finalResults.FirstOrDefault(t => t.TableType == "RelevantResults")?
                    .ResultRows.Select(r => ConvertToPSObject(r));
                return results;
            }
        }

        protected override void ExecuteCmdlet()
        {
            var results = Run();
            WriteObject(results, true);
        }

        private object ConvertToPSObject(IDictionary<string, object> r)
        {
            PSObject res = new PSObject();
            if (r != null)
            {
                foreach (var kvp in r)
                {
                    res.Properties.Add(new PSNoteProperty(kvp.Key, kvp.Value));
                }
            }
            return res;
        }

        private KeywordQuery CreateKeywordQuery(string clientFunction)
        {
            var keywordQuery = new KeywordQuery(ClientContext);

            // Construct query to execute
            var query = "";
            if (!string.IsNullOrEmpty(Query))
            {
                query = Query;
            }

            keywordQuery.QueryText = query;
            keywordQuery.ClientType = ClientType;
            keywordQuery.TrimDuplicates = TrimDuplicates;
            if (ParameterSpecified(nameof(Refiners))) keywordQuery.Refiners = Refiners;
            if (ParameterSpecified(nameof(Culture))) keywordQuery.Culture = Culture;
            if (ParameterSpecified(nameof(QueryTemplate))) keywordQuery.QueryTemplate = QueryTemplate;
            if (ParameterSpecified(nameof(RankingModelId))) keywordQuery.RankingModelId = RankingModelId;
            if (ParameterSpecified(nameof(HiddenConstraints))) keywordQuery.HiddenConstraints = HiddenConstraints;
            if (ParameterSpecified(nameof(TimeZoneId))) keywordQuery.TimeZoneId = TimeZoneId;
            if (ParameterSpecified(nameof(EnablePhonetic))) keywordQuery.EnablePhonetic = EnablePhonetic;
            if (ParameterSpecified(nameof(EnableStemming))) keywordQuery.EnableStemming = EnableStemming;
            if (ParameterSpecified(nameof(EnableQueryRules))) keywordQuery.EnableQueryRules = EnableQueryRules;
            if (ParameterSpecified(nameof(SourceId))) keywordQuery.SourceId = SourceId;
            if (ParameterSpecified(nameof(ProcessBestBets))) keywordQuery.ProcessBestBets = ProcessBestBets;
            if (ParameterSpecified(nameof(ProcessPersonalFavorites))) keywordQuery.ProcessPersonalFavorites = ProcessPersonalFavorites;
            if (ParameterSpecified(nameof(CollapseSpecification))) keywordQuery.CollapseSpecification = CollapseSpecification;

            if (SortList != null)
            {
                var sortList = keywordQuery.SortList;
                sortList.Clear();
                foreach (string key in SortList.Keys)
                {
                    SortDirection sort = (SortDirection)Enum.Parse(typeof(SortDirection), SortList[key] as string, true);
                    sortList.Add(key, sort);
                }
            }
            if (SelectProperties != null)
            {
                keywordQuery.SelectProperties.Clear();
                var selectProperties = keywordQuery.SelectProperties;
                if (SelectProperties.Length == 1 && SelectProperties[0].Contains(","))
                {
                    SelectProperties = SelectProperties[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }

                foreach (string property in SelectProperties)
                {
                    selectProperties.Add(property.Trim());
                }
            }
            if (RefinementFilters != null)
            {
                var refinementFilters = keywordQuery.RefinementFilters;
                refinementFilters.Clear();
                foreach (string property in RefinementFilters)
                {
                    refinementFilters.Add(property);
                }
            }
            if (Properties != null)
            {
                foreach (string key in Properties.Keys)
                {
                    QueryPropertyValue propVal = new QueryPropertyValue();
                    var value = Properties[key];
                    if (value is string)
                    {
                        propVal.StrVal = (string)value;
                        propVal.QueryPropertyValueTypeIndex = 1;
                    }
                    else if (value is int)
                    {
                        propVal.IntVal = (int)value;
                        propVal.QueryPropertyValueTypeIndex = 2;
                    }
                    else if (value is bool)
                    {
                        propVal.BoolVal = (bool)value;
                        propVal.QueryPropertyValueTypeIndex = 3;
                    }
                    else if (value is string[])
                    {
                        propVal.StrArray = (string[])value;
                        propVal.QueryPropertyValueTypeIndex = 4;
                    }
                    keywordQuery.Properties.SetQueryPropertyValue(key, propVal);
                }
            }

            QueryPropertyValue clientFuncPropVal = new QueryPropertyValue();
            clientFuncPropVal.StrVal = clientFunction;
            clientFuncPropVal.QueryPropertyValueTypeIndex = 1;

            keywordQuery.Properties.SetQueryPropertyValue("ClientFunction", clientFuncPropVal);
            return keywordQuery;
        }
    }
}