using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;
using File = Microsoft.SharePoint.Client.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFileVersion", DefaultParameterSetName = "Return as file object")]
    public class RemoveFileVersion : PnPWebCmdlet
    {
        private const string ParameterSetName_BYID = "By Id";
        private const string ParameterSetName_ALL = "All";

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ALL)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_BYID)]
        public FileVersionPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_BYID)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                // We can't deal with absolute URLs
                Url = UrlUtility.MakeRelativeUrl(Url);
            }

            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

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

                switch (ParameterSetName)
                {
                    case ParameterSetName_ALL:
                        {
                            if (Force || ShouldContinue("Remove all versions?", Resources.Confirm))
                            {
                                versions.DeleteAll();
                                ClientContext.ExecuteQueryRetry();
                            }
                            break;
                        }
                    case ParameterSetName_BYID:
                        {
                            if (Force || ShouldContinue("Remove a version?", Resources.Confirm))
                            {
                                if (!string.IsNullOrEmpty(Identity.Label))
                                {
                                    if (Recycle.IsPresent)
                                    {
                                        versions.RecycleByLabel(Identity.Label);
                                    }
                                    else
                                    {
                                        versions.DeleteByLabel(Identity.Label);
                                    }
                                    ClientContext.ExecuteQueryRetry();
                                }
                                else if (Identity.Id != -1)
                                {
                                    if (Recycle.IsPresent)
                                    {
                                        versions.RecycleByID(Identity.Id);
                                    }
                                    else
                                    {
                                        versions.DeleteByID(Identity.Id);
                                    }
                                    ClientContext.ExecuteQueryRetry();
                                }
                            }
                            break;
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


