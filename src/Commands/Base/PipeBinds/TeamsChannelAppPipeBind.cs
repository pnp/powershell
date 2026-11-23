using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TeamsChannelAppPipeBind
    {
        private readonly string _id;
        private readonly string _displayName;
        private readonly TeamApp _app;

        public TeamsChannelAppPipeBind()
        {
        }

        public TeamsChannelAppPipeBind(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
            if (Guid.TryParse(input, out Guid appId))
            {
                _id = input;
            }
            else
            {
                _displayName = input;
            }
        }

        public TeamsChannelAppPipeBind(TeamApp app)
        {
            _app = app;
        }

        public string Id => _id;

        public TeamApp Get(ApiRequestHelper requestHelper, string groupId, string channelId)
        {
            if (_app != null)
            {
                return _app;
            }
            else
            {
                var app = TeamsUtility.GetChannelApp(requestHelper, groupId, channelId, _id);
                if (string.IsNullOrEmpty(app.Id))
                {
                    var apps = TeamsUtility.GetChannelApps(requestHelper, groupId, channelId);
                    if (apps != null)
                    {
                        // find the app by id
                        app = apps.FirstOrDefault(t => t.DisplayName.Equals(_displayName, System.StringComparison.OrdinalIgnoreCase));
                    }
                }
                return app;
            }
        }
    }
}

