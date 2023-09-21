using PnP.PowerShell.Commands.Model.EntraID;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ServicePrincipalAvailableAppRoleBind
    {
        private readonly Guid? _id;
        private readonly string _value;
        private readonly ServicePrincipalAppRole _appRole;

        public ServicePrincipalAvailableAppRoleBind()
        {
        }

        public ServicePrincipalAvailableAppRoleBind(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (Guid.TryParse(value, out Guid valueId))
            {
                _id = valueId;
            }
            else
            {
                _value = value;
            }
        }

        public ServicePrincipalAvailableAppRoleBind(ServicePrincipalAppRole appRole)
        {
            _appRole = appRole;
        }

        public Guid? Id => _id;
        public ServicePrincipalAppRole AppRole => _appRole;

        internal ServicePrincipalAppRole GetAvailableAppRole(PnPConnection connection, string accesstoken, ServicePrincipal servicePrincipal)
        {
            ServicePrincipalAppRole appRole = null;

            if (_appRole != null) appRole = _appRole;
            if (!string.IsNullOrEmpty(_value)) appRole = servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Value == _value);
            if (_id.HasValue) appRole = servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Id == _id.Value);

            if (appRole != null)
            {
                appRole.ServicePrincipal = servicePrincipal;
            }

            return appRole;
        }          
    }
}
