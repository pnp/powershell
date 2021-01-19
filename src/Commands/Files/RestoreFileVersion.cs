using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System.Management.Automation;
using File = Microsoft.SharePoint.Client.File;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsData.Restore, "PnPFileVersion", DefaultParameterSetName = "Return as file object")]
    public class RestoreFileVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = false)]
        public FileVersionPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            File file;

            file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));

            ClientContext.Load(file, f => f.Exists, f => f.Versions.IncludeWithDefaultProperties(i => i.CreatedBy));
            ClientContext.ExecuteQueryRetry();

            if (file.Exists)
            {
                var versions = file.Versions;

                if (Force || ShouldContinue("Restoring a previous version will overwrite the current version.", Resources.Confirm))
                {
                    if (!string.IsNullOrEmpty(Identity.Label))
                    {
                        versions.RestoreByLabel(Identity.Label);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject("Version restored");
                    }
                    else if (Identity.Id != -1)
                    {
                        var version = versions.GetById(Identity.Id);
                        ClientContext.Load(version);
                        ClientContext.ExecuteQueryRetry();

                        versions.RestoreByLabel(version.VersionLabel);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject("Version restored");
                    }
                }
            }
            else
            {
                throw new PSArgumentException("File not found", nameof(Url));
            }
        }
    }
}


