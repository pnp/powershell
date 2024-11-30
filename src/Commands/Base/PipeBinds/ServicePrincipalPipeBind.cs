using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ServicePrincipalPipeBind
    {
        private readonly Guid? _id;
        private readonly string _displayName;
        private readonly AzureADServicePrincipal _servicePrincipal;

        public ServicePrincipalPipeBind()
        {
        }

        public ServicePrincipalPipeBind(string value)
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
                _displayName = value;
            }
        }

        public ServicePrincipalPipeBind(AzureADServicePrincipal servicePrincipal)
        {
            _servicePrincipal = servicePrincipal;
        }

        public Guid? Id => _id;
        public AzureADServicePrincipal ServicePrincipal => _servicePrincipal;

        internal AzureADServicePrincipal GetServicePrincipal(GraphHelper requestHelper)
        {
            if(_servicePrincipal != null) return _servicePrincipal;
            if(!string.IsNullOrEmpty(_displayName)) return ServicePrincipalUtility.GetServicePrincipalByAppName(requestHelper, _displayName);
            if(_id.HasValue) return ServicePrincipalUtility.GetServicePrincipalByObjectId(requestHelper, _id.Value) ?? ServicePrincipalUtility.GetServicePrincipalByAppId(requestHelper, _id.Value);
            return null;
        }       
    }
}
