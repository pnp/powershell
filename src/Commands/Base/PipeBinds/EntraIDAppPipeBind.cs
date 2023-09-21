using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.EntraID;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class EntraIDAppPipeBind
    {
        private Guid _id;
        private string _name;

        public EntraIDAppPipeBind(Guid id)
        {
            _id = id;
        }

        public EntraIDAppPipeBind(string name)
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

        public App GetApp(BasePSCmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            if (_id != Guid.Empty)
            {
                var results = Utilities.REST.GraphHelper.GetAsync<RestResultCollection<App>>(connection, $"/v1.0/applications?$filter=appId eq '{_id}'", accessToken).GetAwaiter().GetResult();
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
                else
                {
                    return GraphHelper.GetAsync<App>(connection, $"/v1.0/applications/{_id}", accessToken).GetAwaiter().GetResult();
                }

            }
            if (!string.IsNullOrEmpty(_name))
            {
                var results = GraphHelper.GetAsync<RestResultCollection<App>>(connection, $"/v1.0/applications?$filter=displayName eq '{_name}'", accessToken).GetAwaiter().GetResult();
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
            }
            cmdlet.WriteError(new PSArgumentException("Entra ID App not found"), ErrorCategory.ObjectNotFound);
            return null;
        }
    }
}