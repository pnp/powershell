using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteCollectionAdmin")]
    [OutputType(typeof(void))]
    public class AddSiteCollectionAdmin : PnPSharePointCmdlet
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
                    user.IsSiteAdmin = true;
                    user.Update();

                    try
                    {
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException e)
                    {
                        WriteWarning($"Exception occurred while trying to add the user: \"{e.Message}\". User will be skipped.");
                    }
                }
                else
                {
                    WriteWarning($"Unable to add user as it wasn't found. User will be skipped.");
                }
            }
        }
    }
}