using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Export, "PnPTaxonomy", SupportsShouldProcess = true)]
    public class ExportTaxonomy : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = "TermSet")]
        public GuidPipeBind TermSetId = new GuidPipeBind();

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeID = false;

        [Parameter(Mandatory = false)]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = "TermSet")]
        public string TermStoreName;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public string Delimiter = "|";

        [Parameter(Mandatory = false, ParameterSetName = "TermSet")]
        public int Lcid = 0;

        [Parameter(Mandatory = false)]
        public Encoding Encoding = Encoding.Unicode;


        protected override void ExecuteCmdlet()
        {
            List<string> exportedTerms;
            if (ParameterSetName == "TermSet")
            {
                if (Delimiter != "|" && Delimiter == ";#")
                {
                    throw new Exception("Restricted delimiter specified");
                }
                if (!string.IsNullOrEmpty(TermStoreName))
                {
                    var taxSession = TaxonomySession.GetTaxonomySession(ClientContext);
                    var termStore = taxSession.TermStores.GetByName(TermStoreName);
                    exportedTerms = ClientContext.Site.ExportTermSet(TermSetId.Id, IncludeID, termStore, Delimiter, Lcid);
                }
                else
                {
                    exportedTerms = ClientContext.Site.ExportTermSet(TermSetId.Id, IncludeID, Delimiter, Lcid);
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
                    case Encoding.UTF7:
                        {
                            textEncoding = System.Text.Encoding.UTF7;
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

    }
}
