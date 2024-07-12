using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{    
    [Cmdlet(VerbsCommon.Get, "PnPFileSharingLink")]
    [OutputType(typeof(IGraphPermissionCollection))]
    public class GetFileSharingLink : PnPWebCmdlet
    {
        private const string ParameterSet_BYFILEURL = "By file url";
        private const string ParameterSet_BYIDENTITY = "By identity";

        [Obsolete("Use Identity parameter instead")]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYFILEURL)]
        public string FileUrl;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYIDENTITY)]
        public FilePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            IFile file;

            if (ParameterSpecified(nameof(Identity)))
            {
                file = Identity.GetCoreFile(PnPContext, this);
            }
            else
            {

                var serverRelativeUrl = string.Empty;
                var ctx = Connection.PnPContext;

                ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

                if (!FileUrl.ToLower().StartsWith(ctx.Web.ServerRelativeUrl.ToLower()))
                {
                    serverRelativeUrl = UrlUtility.Combine(ctx.Web.ServerRelativeUrl, FileUrl);
                }
                else
                {
                    serverRelativeUrl = FileUrl;
                }

                file = ctx.Web.GetFileByServerRelativeUrl(serverRelativeUrl);
            }

            WriteVerbose("Retrieving file sharing details from Microsoft Graph");
            var sharingLinks = file.GetShareLinks();

            WriteObject(sharingLinks?.RequestedItems, true);
        }
    }
}
