using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.New, "PnPSiteGroup")]
    [OutputType(typeof(SiteGroup))]
    public class NewSiteGroup : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        [Alias("Group")]
        public string Name;

        [Parameter(Mandatory = true)]
        public string[] PermissionLevels;

        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(Site)))
            {
                url = Site.Url;
            }
            var rootWeb = this.Tenant.GetSiteByUrl(url).RootWeb;

            var roleDefinitions = GetRoleDefinitions(rootWeb);

            var roleDefCollection = new RoleDefinitionBindingCollection(AdminContext);
            foreach (var permissionToAdd in PermissionLevels)
            {
                if (!roleDefinitions.Contains(permissionToAdd))
                {
                    throw new PSArgumentException($"Permission level {permissionToAdd} not defined in site");
                }
                var existingRoleDef = rootWeb.RoleDefinitions.GetByName(permissionToAdd);
                roleDefCollection.Add(existingRoleDef);
            }
            var groupCI = new GroupCreationInformation();
            groupCI.Title = Name;
            var group = rootWeb.SiteGroups.Add(groupCI);
            rootWeb.RoleAssignments.Add(group, roleDefCollection);
            AdminContext.Load(group, g => g.Title, g => g.LoginName, g => g.Users, g => g.Owner.LoginName, g => g.OwnerTitle);
            AdminContext.ExecuteQueryRetry();
            var siteGroup = new SiteGroup(AdminContext, Tenant, group, rootWeb);
            WriteObject(siteGroup);
        }

        private HashSet<string> GetRoleDefinitions(Web web)
        {
            RoleDefinitionCollection roleDefinitions = web.RoleDefinitions;
            AdminContext.Load(roleDefinitions);
            AdminContext.ExecuteQueryRetry();
            var hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var roleDef in roleDefinitions)
            {
                hashSet.Add(roleDef.Name);
            }
            return hashSet;
        }
    }
}
