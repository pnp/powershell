using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteCollectionAdmin")]
    public class AddSiteCollectionAdmin : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public List<UserPipeBind> Owners;

        protected override void ExecuteCmdlet()
        {
            foreach (var owner in Owners)
            {
                User user = null;
                if (owner.Id > 0)
                {
                    WriteVerbose($"Adding user with Id \"{owner.Id}\" as site collection administrator");
                    user = ClientContext.Web.GetUserById(owner.Id);
                }
                else if (owner.User != null && owner.User.Id > 0)
                {
                    WriteVerbose($"Adding user provided in pipeline as site collection administrator");
                    user = owner.User;

                }
                else if (!string.IsNullOrWhiteSpace(owner.Login))
                {
                    WriteVerbose($"Adding user with loginname \"{owner.Login}\" as site collection administrator");
                    if (owner.Login.StartsWith("i:"))
                    {
                        user = ClientContext.Web.SiteUsers.GetByLoginName(owner.Login);
                    }
                    else
                    {
                        user = ClientContext.Web.EnsureUser(owner.Login);
                    }
                }
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