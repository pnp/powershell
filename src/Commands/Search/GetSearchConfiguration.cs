using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using System.Dynamic;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Administration;
using Microsoft.SharePoint.Client.Search.Portability;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.REST;
using Resources = PnP.PowerShell.Commands.Properties.Resources;


namespace PnP.PowerShell.Commands.Search
{
    public enum OutputFormat
    {
        CompleteXml = 0,
        ManagedPropertyMappings = 1
    }

    public enum BookmarkStatus
    {
        Suggested = 0,
        Published = 1
    }

    [Cmdlet(VerbsCommon.Get, "PnPSearchConfiguration", DefaultParameterSetName = "Xml")]
    public class GetSearchConfiguration : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SearchConfigurationScope Scope = SearchConfigurationScope.Web;

        [Parameter(Mandatory = false, ParameterSetName = "Xml")]
        [Parameter(Mandatory = false, ParameterSetName = "CSV")]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = "OutputFormat")]
        public OutputFormat OutputFormat = OutputFormat.CompleteXml;

        [Parameter(Mandatory = false, ParameterSetName = "CSV")]
        public SwitchParameter PromotedResultsToBookmarkCSV;

        [Parameter(Mandatory = false, ParameterSetName = "CSV")]
        public BookmarkStatus BookmarkStatus = BookmarkStatus.Suggested;

        [Parameter(Mandatory = false, ParameterSetName = "CSV")]
        public bool ExcludeVisualPromotedResults = true;

        protected override void ExecuteCmdlet()
        {
            string output = string.Empty;

            if (!PromotedResultsToBookmarkCSV.IsPresent)
            {
                switch (Scope)
                {
                    case SearchConfigurationScope.Web:
                        {

                            output = CurrentWeb.GetSearchConfiguration();

                            break;
                        }
                    case SearchConfigurationScope.Site:
                        {

                            output = ClientContext.Site.GetSearchConfiguration();

                            break;
                        }
                    case SearchConfigurationScope.Subscription:
                        {
                            if (!ClientContext.Url.ToLower().Contains("-admin"))
                            {
                                throw new InvalidOperationException(Resources.CurrentSiteIsNoTenantAdminSite);
                            }

                            SearchObjectOwner owningScope = new SearchObjectOwner(ClientContext, SearchObjectLevel.SPSiteSubscription);
                            var config = new SearchConfigurationPortability(ClientContext);
                            ClientResult<string> configuration = config.ExportSearchConfiguration(owningScope);
                            ClientContext.ExecuteQueryRetry();
                            output = configuration.Value;
                        }
                        break;
                }
            }
            else
            {
                string promotedResultsBaseUrl = "searchsetting/getpromotedresultqueryrules?";
                if (Scope == SearchConfigurationScope.Site)
                {
                    promotedResultsBaseUrl += "sitecollectionlevel=true&";
                }

                int offset = 0;
                const int numberOfRules = 50;
                bool hasData;
                List<string> queryRuleResponses = new List<string>();
                do
                {
                    string runUrl = string.Format("{0}offset={1}&numberOfRules={2}", promotedResultsBaseUrl, offset, numberOfRules);
                    string response = RestHelper.ExecuteGetRequest(ClientContext, runUrl);
                    offset += numberOfRules;
                    var config = JsonSerializer.Deserialize<Root>(response);
                    hasData = config.Result != null && config.Result.Count > 0;
                    if(hasData) queryRuleResponses.Add(response);
                } while (hasData);

                List<ExpandoObject> bookmarks = new List<ExpandoObject>(200);
                foreach (var response in queryRuleResponses)
                {
                    var result = PromotedResultsToBookmarks(response);
                    if (result != null && result.Count > 0)
                    {
                        bookmarks.AddRange(result);
                    }
                }
                output = BookmarksToString(bookmarks);
            }

            if (Path != null)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                System.IO.File.WriteAllText(Path, output);
            }
            else
            {
                if (OutputFormat == OutputFormat.CompleteXml)
                {
                    WriteObject(output);
                }
                else if (OutputFormat == OutputFormat.ManagedPropertyMappings)
                {
                    StringReader sr = new StringReader(output);
                    var doc = XDocument.Load(sr);
                    var mps = GetCustomManagedProperties(doc);

                    foreach (var mp in mps)
                    {
                        mp.Aliases = new List<string>();
                        mp.Mappings = new List<string>();

                        var mappings = GetCpMappingsFromPid(doc, mp.Pid);
                        mp.Mappings = mappings;
                        var aliases = GetAliasesFromPid(doc, mp.Pid);
                        mp.Aliases = aliases;
                    }
                    WriteObject(mps, true);
                }
            }
        }

        private List<ExpandoObject> PromotedResultsToBookmarks(string json)
        {
            var bookmarks = new List<ExpandoObject>();
            var config = JsonSerializer.Deserialize<Root>(json);
            if (config.Result != null)
            {
                foreach (var rule in config.Result)
                {
                    if (rule.QueryConditions == null || rule.PromotedResults == null)
                    {
                        continue;
                    }
                    foreach (var promoResult in rule.PromotedResults)
                    {
                        dynamic bookmark = new ExpandoObject();
                        bookmark.Title = promoResult.Title.Contains(" ") ? '"' + promoResult.Title + '"' : promoResult.Title;
                        bookmark.Url = promoResult.Url;
                        
                        if (promoResult.IsVisual && ExcludeVisualPromotedResults)
                        {
                            LogWarning($"Skipping visual promoted result {bookmark.Title} ({bookmark.Url})");
                            continue;
                        }
                        
                        List<string> triggerTerms = new List<string>();
                        bool matchSimilar = false;
                        foreach (var condition in rule.QueryConditions)
                        {
                            if (condition.Terms == null || condition.QueryConditionType != "Keyword")
                            {
                                LogWarning($"Skipping {bookmark.Title} due to no trigger conditions");
                                continue;
                            }

                            if (condition.MatchingOptions.Contains("ProperPrefix") || condition.MatchingOptions.Contains("ProperSuffix"))
                            {
                                matchSimilar = true;
                            }

                            foreach (string term in condition.Terms)
                            {
                                triggerTerms.AddRange(term.Split(';').Select(s => s.Replace("Keywords:", "").Trim()).ToList());
                            }
                        }
                        if (triggerTerms.Count == 0)
                        {
                            LogWarning($"Skipping {bookmark.Title} due to no trigger terms");
                            continue;
                        }

                        var dict = bookmark as IDictionary<string, object>;

                        bookmark.Keywords = string.Join(";", triggerTerms.Distinct());
                        dict["Match Similar Keywords"] = matchSimilar.ToString().ToLowerInvariant();
                        bookmark.State = BookmarkStatus == BookmarkStatus.Suggested ? "suggested" : "published";
                        bookmark.Description = promoResult.Description.Contains(" ") ? '"' + promoResult.Description + '"' : promoResult.Description;
                        dict["Reserved Keywords"] = string.Empty;
                        bookmark.Categories = string.Empty;

                        dict["Start Date"] = rule.StartDate != DateTime.MinValue ? rule.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ") : string.Empty;
                        dict["End Date"] = rule.EndDate != DateTime.MinValue ? rule.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ") : string.Empty;
                        dict["Country/Region"] = string.Empty;
                        bookmark.Groups = string.Empty;
                        dict["Device & OS"] = string.Empty;
                        dict["Targeted Variations"] = string.Empty;
                        dict["Last Modified"] = string.Empty;
                        dict["Last Modified By"] = string.Empty;
                        bookmark.Id = string.Empty;
                        bookmarks.Add(bookmark);
                    }
                }
            }
            return bookmarks;
        }

        private static string BookmarksToString(List<ExpandoObject> bookmarks)
        {
            StringBuilder sb = new StringBuilder();
            bool firstLine = true;
            foreach (var bookmark in bookmarks)
            {
                var dict = bookmark as IDictionary<string, object>;
                if (firstLine)
                {
                    sb.AppendLine(string.Join(",", dict.Keys));
                    firstLine = false;
                }
                sb.AppendLine(string.Join(",", dict.Values));
            }

            return sb.ToString();
        }

        #region Queryrule / Bookmark classes
        public class ContextCondition
        {
            [JsonPropertyName("ContextConditionType")]
            public string ContextConditionType { get; set; }

            [JsonPropertyName("SourceId")]
            public string SourceId { get; set; }
        }

        public class PromotedResult
        {
            [JsonPropertyName("Description")]
            public string Description { get; set; }

            [JsonPropertyName("IsVisual")]
            public bool IsVisual { get; set; }

            [JsonPropertyName("Title")]
            public string Title { get; set; }

            [JsonPropertyName("Url")]
            public string Url { get; set; }
        }

        public class QueryCondition
        {
            [JsonPropertyName("LCID")]
            public int LCID { get; set; }

            [JsonPropertyName("MatchingOptions")]
            public string MatchingOptions { get; set; }

            [JsonPropertyName("QueryConditionType")]
            public string QueryConditionType { get; set; }

            [JsonPropertyName("SubjectTermsOrigin")]
            public string SubjectTermsOrigin { get; set; }

            [JsonPropertyName("Terms")]
            public List<string> Terms { get; set; }
        }

        public class Result
        {
            [JsonPropertyName("EndDate")]
            public DateTime EndDate { get; set; }

            [JsonPropertyName("IsPromotedResultsOnly")]
            public bool IsPromotedResultsOnly { get; set; }

            [JsonPropertyName("PromotedResults")]
            public List<PromotedResult> PromotedResults { get; set; }

            [JsonPropertyName("QueryConditions")]
            public List<QueryCondition> QueryConditions { get; set; }

            [JsonPropertyName("StartDate")]
            public DateTime StartDate { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("Result")]
            public List<Result> Result { get; set; }
        }


        #endregion

        #region Helper functions
        internal class ManagedProperty
        {
            public string Name { get; set; }
            public List<string> Aliases { get; set; }

            public List<string> Mappings { get; set; }

            public string Type { get; set; }

            internal string Pid { get; set; }
        }

        private static string PidToName(string pid)
        {
            /*
    RefinableString00 1000000000
    Int00             1000000100
    Date00            1000000200
    Decimal00         1000000300
    Double00          1000000400
    RefinableInt00    1000000500
    RefinableDate00   1000000600
    RefinableDateSingle00 1000000650
    RefinableDateInvariant00  1000000660
    RefinableDecimal00    1000000700
    RefinableDouble00     1000000800
    RefinableString100  1000000900                
 */
            int p;
            if (!int.TryParse(pid, out p)) return pid;
            if (p < 1000000000) return pid;

            var autoMpNum = pid.Substring(pid.Length - 2);
            var mpName = pid;

            if (p < 1000000100) mpName = "RefinableString";
            else if (p < 1000000200) mpName = "Int";
            else if (p < 1000000300) mpName = "Date";
            else if (p < 1000000400) mpName = "Decimal";
            else if (p < 1000000500) mpName = "Double";
            else if (p < 1000000600) mpName = "RefinableInt";
            else if (p < 1000000650) mpName = "RefinableDate";
            else if (p < 1000000660) mpName = "RefinableDateSingle";
            else if (p < 1000000700) mpName = "RefinableDateInvariant";
            else if (p < 1000000800) mpName = "RefinableDecimal";
            else if (p < 1000000900) mpName = "RefinableDouble";
            else if (p < 1000001000) mpName = "RefinableString1";
            return mpName + autoMpNum;
        }

        private static List<ManagedProperty> GetCustomManagedProperties(XDocument doc)
        {
            var mpList = new List<ManagedProperty>();
            var mps =
                doc.Descendants()
                    .Where(n => n.Name.LocalName.StartsWith("KeyValueOfstringManagedPropertyInfo"));
            foreach (var mpNode in mps)
            {
                var name = mpNode.Descendants().Single(n => n.Name.LocalName == "Name").Value;
                var pid = mpNode.Descendants().Single(n => n.Name.LocalName == "Pid").Value;
                var type = mpNode.Descendants().Single(n => n.Name.LocalName == "ManagedType").Value;
                var mp = new ManagedProperty
                {
                    Name = PidToName(name),
                    Pid = pid,
                    Type = type
                };
                mpList.Add(mp);
            }

            var overrides = doc.Descendants()
                .Where(n => n.Name.LocalName.StartsWith("KeyValueOfstringOverrideInfo"));
            foreach (var o in overrides)
            {
                var name = o.Descendants().Single(n => n.Name.LocalName == "Name").Value;
                var pid = o.Descendants().Single(n => n.Name.LocalName == "ManagedPid").Value;
                var mp = new ManagedProperty
                {
                    Name = PidToName(name),
                    Pid = pid
                };
                if (mp.Name.Contains("String")) mp.Type = "Text";
                else if (mp.Name.Contains("Date")) mp.Type = "Date";
                else if (mp.Name.Contains("Int")) mp.Type = "Integer";
                else if (mp.Name.Contains("Double")) mp.Type = "Double";
                else if (mp.Name.Contains("Decimal")) mp.Type = "Decimal";
                mpList.Add(mp);
            }
            return mpList;
        }

        private static List<string> GetAliasesFromPid(XDocument doc, string pid)
        {
            var aliasList = new List<string>();
            var aliases = doc.Descendants().Where(n => n.Name.LocalName.StartsWith("KeyValueOfstringAliasInfo"));
            foreach (var alias in aliases)
                if (alias.Descendants().Single(n => n.Name.LocalName == "ManagedPid").Value == pid)
                {
                    var aliasName = alias.Descendants().Single(n => n.Name.LocalName == "Name").Value;
                    aliasList.Add(aliasName);
                }
            return aliasList;
        }

        private static List<string> GetCpMappingsFromPid(XDocument doc, string pid)
        {
            var mappingList = new List<string>();
            var cps = doc.Descendants().Where(n => n.Name.LocalName.StartsWith("KeyValueOfstringMappingInfo"));
            foreach (var cp in cps)
                if (cp.Descendants().Single(n => n.Name.LocalName == "ManagedPid").Value == pid)
                {
                    var cpName = cp.Descendants().Single(n => n.Name.LocalName == "CrawledPropertyName").Value;
                    mappingList.Add(cpName);
                }
            return mappingList;
        }
        #endregion
    }
}
