using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class AzureADAppPipeBind
    {
        private Guid _id;
        private string _name;

        public AzureADAppPipeBind(Guid id)
        {
            _id = id;
        }

        public AzureADAppPipeBind(string name)
        {
            if (Guid.TryParse(name, out Guid guid))
            {
                _id = guid;
            }
            else
            {
                _name = name;
            }
        }

        public AzureADApp GetApp(BasePSCmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            if (_id != Guid.Empty)
            {
                var results = GraphHelper.Get<RestResultCollection<AzureADApp>>(cmdlet, connection, $"/v1.0/applications?$filter=appId eq '{_id}'", accessToken);
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
                else
                {
                    return GraphHelper.Get<AzureADApp>(cmdlet, connection, $"/v1.0/applications/{_id}", accessToken);
                }

            }
            if (!string.IsNullOrEmpty(_name))
            {
                var results = GraphHelper.Get<RestResultCollection<AzureADApp>>(cmdlet, connection, $"/v1.0/applications?$filter=displayName eq '{_name}'", accessToken);
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
            }
            cmdlet.WriteError(new PSArgumentException("Azure AD App not found"), ErrorCategory.ObjectNotFound);
            return null;
        }
    }
}