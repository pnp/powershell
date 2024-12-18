using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using File = System.IO.File;
using System.Linq;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Import, "PnPTaxonomy")]
    public class ImportTaxonomy : PnPSharePointCmdlet
    {

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "Direct")]
        public string[] Terms;

        [Parameter(Mandatory = true, ParameterSetName = "File")]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int Lcid = 1033;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string TermStoreName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Delimiter = "|";

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter SynchronizeDeletions;

        protected override void ExecuteCmdlet()
        {
            string[] lines;
            if (ParameterSetName == "File")
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }

                lines = File.ReadAllLines(Path);
            }
            else
            {
                lines = Terms;
            }

            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

            if (!string.IsNullOrEmpty(TermStoreName))
            {
                var taxSession = TaxonomySession.GetTaxonomySession(ClientContext);
                var termStore = taxSession.TermStores.GetByName(TermStoreName);
                ClientContext.Site.ImportTerms(lines, Lcid, termStore, Delimiter, SynchronizeDeletions);
            }
            else
            {
                ClientContext.Site.ImportTerms(lines, Lcid, Delimiter, SynchronizeDeletions);
            }
        }

    }
}
