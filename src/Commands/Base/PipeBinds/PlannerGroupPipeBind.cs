using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PlannerGroupPipeBind
    {
        private readonly string _id;
        private readonly string _stringValue;

        public PlannerGroupPipeBind()

        {
            _id = string.Empty;
            _stringValue = string.Empty;
        }

        public PlannerGroupPipeBind(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
            if (Guid.TryParse(input, out Guid tabId))
            {
                _id = input;
            }
            else
            {
                _stringValue = input;
            }
        }

        public PlannerGroupPipeBind(Model.Graph.Group group)
        {
            _id = group.Id;
        }

        public string GetGroupId(ApiRequestHelper requestHelper)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                return _id.ToString();
            }
            else
            {
                var collection = requestHelper.Get<RestResultCollection<Model.Graph.Group>>( $"v1.0/groups?$filter=mailNickname eq '{_stringValue}'&$select=Id");
                if (collection != null && collection.Items.Any())
                {
                    return collection.Items.First().Id;
                }
                else
                {
                    // find the team by displayName
                    var byDisplayNamecollection = requestHelper.Get<RestResultCollection<Model.Graph.Group>>( $"v1.0/groups?$filter=displayName eq '{_stringValue}'&$select=Id");
                    if (byDisplayNamecollection != null && byDisplayNamecollection.Items.Any())
                    {
                        if (byDisplayNamecollection.Items.Count() == 1)
                        {
                            return byDisplayNamecollection.Items.First().Id;
                        }
                        else
                        {
                            throw new PSArgumentException("We found more matches based on the identity value you entered. Use Get-PnPMicrosoft365Group to find the correct instance and use the id value to select the correct group instead.");
                        }
                    }
                    return null;
                }
            }
        }
    }
}
