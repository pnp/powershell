using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteCollectionAdmin")]
    [OutputType(typeof(void))]
    public class RemoveSiteCollectionAdmin : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public List<UserPipeBind> Owners;

        protected override void ExecuteCmdlet()
        {
            foreach (var owner in Owners)
            {
                User user = owner.GetUser(ClientContext, true);

                if (user != null)
                {
                    user.IsSiteAdmin = false;
                    user.Update();

                    try
                    {
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException e)
                    {
                        LogWarning($"Exception occurred while trying to remove the user: \"{e.Message}\". User will be skipped.");
                    }
                }
                else
                {
                    LogWarning($"Unable to remove user as it wasn't found. User will be skipped.");
                }
            }
        }
    }
}