using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteGroup")]
    [OutputType(typeof(SiteGroup))]
    public class SetSiteGroup : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string[] PermissionLevelsToAdd;

        [Parameter(Mandatory = false)]
        public string[] PermissionLevelsToRemove;
        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(Site)))
            {
                url = Site.Url;
            }
            var rootWeb = this.Tenant.GetSiteByUrl(url).RootWeb;

            var roleDefinitions = GetRoleDefinitions(rootWeb);

            var group = rootWeb.SiteGroups.GetByName(Identity);
            if (ParameterSpecified(nameof(Name)))
            {
                group.Title = Name;
            }
            if (ParameterSpecified(nameof(Owner)))
            {
                group.Owner = rootWeb.EnsureUser(Owner);
            }
            group.Update();
            if (ParameterSpecified(nameof(PermissionLevelsToAdd)))
            {
                var roleDefCollection = new RoleDefinitionBindingCollection(AdminContext);
                foreach (var permissionToAdd in PermissionLevelsToAdd)
                {
                    if (!roleDefinitions.Contains(permissionToAdd))
                    {
                        throw new PSArgumentException($"Permission level {permissionToAdd} not defined in site");
                    }
                    var existingRoleDef = rootWeb.RoleDefinitions.GetByName(permissionToAdd);
                    roleDefCollection.Add(existingRoleDef);
                }
                rootWeb.RoleAssignments.Add(group, roleDefCollection);
            }
            if (ParameterSpecified(nameof(PermissionLevelsToRemove)))
            {
                var roleAssignment = rootWeb.RoleAssignments.GetByPrincipal(group);
                foreach (var permissionToRemove in PermissionLevelsToRemove)
                {
                    if (!roleDefinitions.Contains(permissionToRemove))
                    {
                        throw new PSArgumentException($"Permission level {permissionToRemove} not defined in site");
                    }
                    var existingRoleDef = rootWeb.RoleDefinitions.GetByName(permissionToRemove);
                    roleAssignment.RoleDefinitionBindings.Remove(existingRoleDef);
                }
                roleAssignment.Update();
            }
            rootWeb.Update();
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
