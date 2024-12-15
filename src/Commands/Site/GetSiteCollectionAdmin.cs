using Microsoft.SharePoint.Client;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteCollectionAdmin")]
    [OutputType(typeof(User))]
    public class GetSiteCollectionAdmin : PnPWebRetrievalsCmdlet<User>
    {
        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<User, object>>[]
            {
                u => u.Id,
                u => u.Title,
                u => u.LoginName,
                u => u.Email,
                u => u.IsShareByEmailGuestUser,
                u => u.IsSiteAdmin,
                u => u.UserId,
                u => u.IsHiddenInUI,
                u => u.PrincipalType,
                u => u.Alerts.Include(
                    a => a.Title,
                    a => a.Status),
                u => u.Groups.Include(
                    g => g.Id,
                    g => g.Title,
                    g => g.LoginName)
            };

            var siteCollectionAdminUsersQuery = CurrentWeb.SiteUsers.Where(u => u.IsSiteAdmin);
            var siteCollectionAdminUsers = ClientContext.LoadQuery(siteCollectionAdminUsersQuery.Include(RetrievalExpressions));
            ClientContext.ExecuteQueryRetry();

            WriteObject(siteCollectionAdminUsers, true);
        }
    }
}
