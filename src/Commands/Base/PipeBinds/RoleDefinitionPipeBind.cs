using Microsoft.SharePoint.Client;

using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class RoleDefinitionPipeBind
    {
        private readonly object _roleDefinition;

        public RoleDefinitionPipeBind(RoleDefinition definition)
        {
            _roleDefinition = definition;
        }

        public RoleDefinitionPipeBind(int id)
        {
            _roleDefinition = id;
        }

        public RoleDefinitionPipeBind(string name)
        {
            _roleDefinition = name;
        }

        public RoleDefinition GetRoleDefinition(Microsoft.SharePoint.Client.Site site)
            => GetRoleDefinition(site.RootWeb);

        public RoleDefinition GetRoleDefinition(Web web)
        {
            switch (_roleDefinition)
            {
                case RoleDefinition rd:
                    return rd;

                case int id:
                    var rdById = web.RoleDefinitions.GetById(id);
                    web.Context.Load(rdById);
                    web.Context.ExecuteQueryRetry();
                    return rdById;

                case string name:
                    var rdByName = web.RoleDefinitions.GetByName(name);
                    web.Context.Load(rdByName);
                    web.Context.ExecuteQueryRetry();
                    return rdByName;

                default:
                    throw new ArgumentNullException();
            }
        }
    }
}
