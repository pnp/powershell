using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Export, "PnPTaxonomy")]
    public class ExportTaxonomy : PnPSharePointCmdlet
    {
        private const string ParameterSet_TermSet = "TermSet";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TermSet)]
        public Guid TermSetId;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeID = false;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TermSet)]
        public SwitchParameter ExcludeDeprecated = false;

        [Parameter(Mandatory = false)]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TermSet)]
        public string TermStoreName;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public string Delimiter = "|";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TermSet)]
        public int Lcid = 0;

        [Parameter(Mandatory = false)]
        public Encoding Encoding = Encoding.Unicode;

        protected override void ExecuteCmdlet()
        {
            List<string> exportedTerms;
            if (ParameterSetName == ParameterSet_TermSet)
            {
                if (Delimiter != "|" && Delimiter == ";#")
                {
                    throw new Exception("Restricted delimiter specified");
                }

                if (ExcludeDeprecated && Delimiter != "|")
                {
                    throw new PSArgumentException($"{nameof(ExcludeDeprecated)} works only on the default delimiter", nameof(ExcludeDeprecated));
                }

                if (!string.IsNullOrEmpty(TermStoreName))
                {
                    var taxSession = TaxonomySession.GetTaxonomySession(ClientContext);
                    var termStore = taxSession.TermStores.GetByName(TermStoreName);
                    exportedTerms = ClientContext.Site.ExportTermSet(TermSetId, (IncludeID || ExcludeDeprecated), termStore, Delimiter, Lcid);
                }
                else
                {
                    exportedTerms = ClientContext.Site.ExportTermSet(TermSetId, (IncludeID || ExcludeDeprecated), Delimiter, Lcid);
                }

                if (ExcludeDeprecated)
                {
                    exportedTerms = RemoveDeprecatedTerms(exportedTerms);
                }
            }
            else
            {
                exportedTerms = ClientContext.Site.ExportAllTerms(IncludeID, Delimiter);
            }

            if (Path == null)
            {
                WriteObject(exportedTerms);
            }
            else
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }

                System.Text.Encoding textEncoding = System.Text.Encoding.Unicode;
                if (Encoding == Encoding.UTF7)
                {
                    LogWarning("UTF-7 Encoding is no longer supported. Defaulting to UTF-8");
                    Encoding = Encoding.UTF8;
                }
                switch (Encoding)
                {
                    case Encoding.ASCII:
                        {
                            textEncoding = System.Text.Encoding.ASCII;
                            break;
                        }

                    case Encoding.BigEndianUnicode:
                        {
                            textEncoding = System.Text.Encoding.BigEndianUnicode;
                            break;
                        }
                    case Encoding.UTF32:
                        {
                            textEncoding = System.Text.Encoding.UTF32;
                            break;
                        }
                    case Encoding.UTF8:
                        {
                            textEncoding = System.Text.Encoding.UTF8;
                            break;
                        }
                    case Encoding.Unicode:
                        {
                            textEncoding = System.Text.Encoding.Unicode;
                            break;
                        }
                }

                if (File.Exists(Path))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Path), Resources.Confirm))
                    {
                        File.WriteAllLines(Path, exportedTerms, textEncoding);
                    }
                }
                else
                {
                    File.WriteAllLines(Path, exportedTerms, textEncoding);
                }
            }
        }

        private List<string> RemoveDeprecatedTerms(List<string> exportedTerms)
        {
            var termIds = exportedTerms.Select(t => t.Split(";#").Last().ToGuid());
            var taxSession = TaxonomySession.GetTaxonomySession(ClientContext);
            if (termIds.Any())
            {
                //refetch all the terms (500 per call) again just to check the term is deprecated or not
                var termGroups = termIds.Select((termId, index) => new { termId, index })
                                        .GroupBy(x => x.index / 500, g => g.termId);
                foreach (var termGroup in termGroups)
                {
                    var terms = taxSession.GetTermsById(termGroup.ToArray());
                    ClientContext.Load(terms);
                    ClientContext.ExecuteQueryRetry();
                    var deprecatedTerms = terms.Where(t => t.IsDeprecated);
                    //remove all deprecated terms
                    foreach (var deprecatedTerm in deprecatedTerms)
                    {
                        var index = exportedTerms.FindIndex(s => s.EndsWith(deprecatedTerm.Id.ToString()));
                        if (index > -1)
                        {
                            exportedTerms.RemoveAt(index);
                        }
                    }
                }
            }

            if (!IncludeID)
            {
                //remove the ids from the term string.
                var exportedTermsWithoutId = new List<string>();
                foreach (var term in exportedTerms)
                {
                    exportedTermsWithoutId.Add(string.Join(Delimiter, term.Split(Delimiter).Select(t => t.Split(";#").First())));
                }
                return exportedTermsWithoutId;
            }
            return exportedTerms;
        }
    }
}
