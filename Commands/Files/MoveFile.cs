using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Move, "PnPFile", DefaultParameterSetName = ParameterSet_SITE)]
    public class MoveFile : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER = "Server Relative";
        private const string ParameterSet_SITE = "Site Relative";
        private const string ParameterSet_OTHERSITE = "Other Site Collection";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SERVER)]
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_OTHERSITE)]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SITE)]
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_OTHERSITE)]
        public string SiteRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SITE, Position = 1)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SERVER, Position = 1)]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_OTHERSITE)]
        public string TargetServerRelativeLibrary = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OTHERSITE)]
        public SwitchParameter OverwriteIfAlreadyExists;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OTHERSITE)]
        public SwitchParameter AllowSchemaMismatch;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OTHERSITE)]
        public SwitchParameter AllowSmallerVersionLimitOnDestination;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OTHERSITE)]
        public SwitchParameter IgnoreVersionHistory;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // Ensure that with ParameterSet_OTHERSITE we either receive a ServerRelativeUrl or SiteRelativeUrl
            if (ParameterSetName == ParameterSet_OTHERSITE && !ParameterSpecified(nameof(ServerRelativeUrl)) && !ParameterSpecified(nameof(SiteRelativeUrl)))
            {
                throw new PSArgumentException($"Either provide {nameof(ServerRelativeUrl)} or {nameof(SiteRelativeUrl)}");
            }

            if (ParameterSpecified(nameof(SiteRelativeUrl)))
            {
                var webUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
                ServerRelativeUrl = UrlUtility.Combine(webUrl, SiteRelativeUrl);
            }

            var file = SelectedWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(ServerRelativeUrl));

            ClientContext.Load(file, f => f.Name, f => f.ServerRelativeUrl);
            ClientContext.ExecuteQueryRetry();

            if (Force || ShouldContinue(string.Format(Resources.MoveFile0To1, ServerRelativeUrl, TargetUrl), Resources.Confirm))
            {
                switch(ParameterSetName)
                {
                    case ParameterSet_SITE:
                    case ParameterSet_SERVER:
                        file.MoveToUsingPath(ResourcePath.FromDecodedUrl(TargetUrl), OverwriteIfAlreadyExists.ToBool() ? MoveOperations.Overwrite : MoveOperations.None);
                        break;

                    case ParameterSet_OTHERSITE:
                        SelectedWeb.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);

                        // Create full URLs including the SharePoint domain to the source and destination
                        var source = UrlUtility.Combine(SelectedWeb.Url.Remove(SelectedWeb.Url.Length - SelectedWeb.ServerRelativeUrl.Length + 1, SelectedWeb.ServerRelativeUrl.Length - 1), file.ServerRelativeUrl);
                        var destination = UrlUtility.Combine(SelectedWeb.Url.Remove(SelectedWeb.Url.Length - SelectedWeb.ServerRelativeUrl.Length + 1, SelectedWeb.ServerRelativeUrl.Length - 1), TargetServerRelativeLibrary);

                        ClientContext.Site.CreateCopyJobs(new[] { source }, destination, new CopyMigrationOptions { IsMoveMode = true, 
                                                                                                                    AllowSchemaMismatch = AllowSchemaMismatch.ToBool(), 
                                                                                                                    AllowSmallerVersionLimitOnDestination = AllowSmallerVersionLimitOnDestination.ToBool(), 
                                                                                                                    IgnoreVersionHistory = IgnoreVersionHistory.ToBool(), 
                                                                                                                    NameConflictBehavior = OverwriteIfAlreadyExists.ToBool() ? MigrationNameConflictBehavior.Replace : MigrationNameConflictBehavior.Fail });
                        break;

                    default:
                        throw new PSInvalidOperationException(string.Format(Resources.ParameterSetNotImplemented, ParameterSetName));
                }

                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
 