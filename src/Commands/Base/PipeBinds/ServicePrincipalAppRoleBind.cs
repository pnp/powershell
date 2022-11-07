using PnP.PowerShell.Commands.Model.AzureAD;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ServicePrincipalAppRoleBind
    {
        private readonly Guid? _id;
        private readonly string _value;
        private readonly AzureADServicePrincipalAppRole _appRole;

        public ServicePrincipalAppRoleBind()
        {
        }

        public ServicePrincipalAppRoleBind(string value)
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

        public ServicePrincipalAppRoleBind(AzureADServicePrincipalAppRole appRole)
        {
            _appRole = appRole;
        }

        public Guid? Id => _id;
        public AzureADServicePrincipalAppRole AppRole => _appRole;

        internal AzureADServicePrincipalAppRole GetAppRole(PnPConnection connection, string accesstoken, AzureADServicePrincipal servicePrincipal)
        {
            if (_appRole != null) return _appRole;
            if (!string.IsNullOrEmpty(_value)) return servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Value == _value);
            if (_id.HasValue) return servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Id == _id.Value);
            return null;
        }       
    }
}
