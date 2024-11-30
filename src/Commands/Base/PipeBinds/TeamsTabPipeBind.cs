using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TeamsTabPipeBind
    {
        private readonly string _id;
        private readonly string _displayName;
        private readonly TeamTab _tab;

        public TeamsTabPipeBind()
        {
        }

        public TeamsTabPipeBind(string input)
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
                _displayName = input;
            }
        }

        public TeamsTabPipeBind(TeamTab tab)
        {
            _tab = tab;
        }

        public string Id => _id;

        public TeamTab GetTab(ApiRequestHelper requestHelper, string groupId, string channelId)
        {
            if (_tab != null)
            {
                return _tab;
            }
            else
            {
                var tab = TeamsUtility.GetTab(requestHelper, groupId, channelId, _id);
                if (string.IsNullOrEmpty(tab.Id))
                {
                    var tabs = TeamsUtility.GetTabs(requestHelper, groupId, channelId);
                    if (tabs != null)
                    {
                        // find the tab by id
                        tab = tabs.FirstOrDefault(t => t.DisplayName.Equals(_displayName, System.StringComparison.OrdinalIgnoreCase));
                    }
                }
                return tab;
            }
        }
    }
}

