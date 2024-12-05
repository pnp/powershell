using PnP.Framework.Sites;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPCommSite")]
    [OutputType(typeof(void))]
    public class EnableCommSite : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string DesignPackageId;

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(DesignPackageId))
            {
                if (Guid.TryParse(DesignPackageId, out Guid designPackageIdGuid))
                {
                    SiteCollection.EnableCommunicationSite(ClientContext, designPackageIdGuid).GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception($"The provided design package id {DesignPackageId} is not a valid guid.");
                }
            }
            else
            {
                SiteCollection.EnableCommunicationSite(ClientContext).GetAwaiter().GetResult();
            }
        }
    }
}