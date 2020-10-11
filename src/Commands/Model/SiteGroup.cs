using System.Collections.Generic;
using System.Linq;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Model
{
    public class SiteGroup
    {
        public string LoginName { get; internal set; }
        public string Title { get; internal set; }
        public string OwnerLoginName { get; internal set; }
        public string OwnerTitle { get; internal set; }
        public List<string> Users { get; internal set; } = new List<string>();
        public List<string> Roles { get; internal set; } = new List<string>();

        internal SiteGroup(ClientContext clientContext, Tenant tenant, Group group, Web rootWeb)
        {
            var roleAssignment = rootWeb.RoleAssignments.GetByPrincipal(group);
            clientContext.Load(roleAssignment, r => r.RoleDefinitionBindings.Include(b => b.Name));
            try
            {
                clientContext.ExecuteQueryRetry();
            }
            catch
            {
                roleAssignment = null;
            }

            var decodedLoginName = tenant.DecodeClaim(group.LoginName);
            var decodedOwnerLoginName = tenant.DecodeClaim(group.Owner.LoginName);

            var userList = tenant.DecodeClaims(group.Users.Select(u => u.LoginName).ToList());
            clientContext.ExecuteQueryRetry();

            OwnerLoginName = decodedOwnerLoginName.Value;
            OwnerTitle = group.OwnerTitle;
            Title = group.Title;
            LoginName = group.LoginName;
            Users.AddRange(userList);

            if (roleAssignment != null && roleAssignment.RoleDefinitionBindings != null)
            {
                Roles.AddRange(roleAssignment.RoleDefinitionBindings.Select(r => r.Name));
            }
        }
    }
}