using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using File = System.IO.File;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Install, "Solution")]
    public class InstallSolution : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public GuidPipeBind PackageId;

        [Parameter(Mandatory = true)]
        public string SourceFilePath;

        [Parameter(Mandatory = false)]
        public int MajorVersion = 1;

        [Parameter(Mandatory = false)]
        public int MinorVersion = 0;

        protected override void ExecuteCmdlet()
        {
            if (File.Exists(SourceFilePath))
            {
                ClientContext.Site.InstallSolution(PackageId.Id, SourceFilePath, MajorVersion, MinorVersion);
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }
    }
}
