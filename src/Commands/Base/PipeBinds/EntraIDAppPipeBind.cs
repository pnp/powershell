using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;
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

        public AzureADApp GetApp(ApiRequestHelper requestHelper)
        {
            if (_id != Guid.Empty)
            {
                var results = requestHelper.Get<RestResultCollection<AzureADApp>>($"/v1.0/applications?$filter=appId eq '{_id}'");
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
                else
                {
                    return requestHelper.Get<AzureADApp>($"/v1.0/applications/{_id}");
                }

            }
            if (!string.IsNullOrEmpty(_name))
            {
                var results = requestHelper.Get<RestResultCollection<AzureADApp>>($"/v1.0/applications?$filter=displayName eq '{_name}'");
                if (results != null && results.Items.Any())
                {
                    return results.Items.First();
                }
            }
            return null;
        }
    }
}