using System;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using File = System.IO.File;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Set, "PnPWikiPageContent")]
    public class SetWikiPageContent : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "STRING")]
        public string Content = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "FILE")]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "FILE")]
        [Parameter(Mandatory = true, ParameterSetName = "STRING")]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "FILE")
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                if (File.Exists(Path))
                {
                    var fileStream = new StreamReader(Path);
                    var contentString = fileStream.ReadToEnd();
                    fileStream.Close();
                    CurrentWeb.AddHtmlToWikiPage(ServerRelativePageUrl, contentString);
                }
                else
                {
                    throw new Exception($"File {Path} does not exist");
                }
            }
            else
            {
                CurrentWeb.AddHtmlToWikiPage(ServerRelativePageUrl, Content);
            }
        }
    }
}
