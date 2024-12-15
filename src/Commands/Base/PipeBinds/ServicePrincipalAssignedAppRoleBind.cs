using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ServicePrincipalAssignedAppRoleBind
    {
        private readonly string _id;
        private readonly AzureADServicePrincipalAppRoleAssignment _appRoleAssignment;

        public ServicePrincipalAssignedAppRoleBind()
        {
        }

        public ServicePrincipalAssignedAppRoleBind(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            _id = value;
        }

        public ServicePrincipalAssignedAppRoleBind(AzureADServicePrincipalAppRoleAssignment appRoleAssignment)
        {
            _appRoleAssignment = appRoleAssignment;
        }

        public string Id => _id;
        public AzureADServicePrincipalAppRoleAssignment AppRoleAssignment => _appRoleAssignment;

        internal AzureADServicePrincipalAppRoleAssignment GetAssignedAppRole(ApiRequestHelper requestHelper, string servicePrincipalObjectId = null)
        {            
            AzureADServicePrincipalAppRoleAssignment appRoleAssignment = null;

            if (_appRoleAssignment != null) appRoleAssignment = _appRoleAssignment;
            if (!string.IsNullOrEmpty(_id))
            {
                if (string.IsNullOrEmpty(servicePrincipalObjectId))
                {
                    throw new ArgumentNullException(nameof(servicePrincipalObjectId), $"{nameof(servicePrincipalObjectId)} is required when the {GetType()} is created based on an Id");
                }
                appRoleAssignment = ServicePrincipalUtility.GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(requestHelper, servicePrincipalObjectId, _id).FirstOrDefault();
            } 

            return appRoleAssignment;
        }          
    }
}
