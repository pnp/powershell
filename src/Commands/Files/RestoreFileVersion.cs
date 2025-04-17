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

        [Parameter(Mandatory = true)]
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

            LogDebug($"Looking up file at {serverRelativeUrl}");
            File file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));

            ClientContext.Load(file, f => f.Exists, f => f.Versions.IncludeWithDefaultProperties(i => i.CreatedBy));
            ClientContext.ExecuteQueryRetry();

            if (file.Exists)
            {
                LogDebug($"File has been found and has {file.Versions.Count} versions");

                var versions = file.Versions;

                if (Force || ShouldContinue("Restoring a previous version will overwrite the current version. Are you sure you wish to continue?", Resources.Confirm))
                {
                    if (!string.IsNullOrEmpty(Identity.Label))
                    {
                        LogDebug($"Trying to restore to version with label '{Identity.Label}'");

                        try
                        {
                            versions.RestoreByLabel(Identity.Label);
                            ClientContext.ExecuteQueryRetry();

                            WriteObject("Version restored");
                        }
                        catch (ServerException e) when (e.ServerErrorTypeName.Equals("System.IO.DirectoryNotFoundException"))
                        {
                            throw new PSArgumentException($"Version with label '{Identity.Label}' does not exist", e);
                        }                        
                    }
                    else if (Identity.Id != -1)
                    {
                        LogDebug($"Looking up version with id '{Identity.Id}'");

                        FileVersion version = versions.GetById(Identity.Id);
                        ClientContext.Load(version);
                        ClientContext.ExecuteQueryRetry();

                        if(version == null || !version.IsPropertyAvailable("VersionLabel"))
                        {
                            throw new PSArgumentException($"Version with id '{Identity.Id}' does not exist", nameof(Identity));
                        }

                        LogDebug($"Trying to restore to version with label '{version.VersionLabel}'");

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