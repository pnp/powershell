using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class SensitivityLabelPipeBind
    {
        private readonly Guid? _labelId;
        private readonly string _labelName;
        private readonly Model.Graph.Purview.InformationProtectionLabel _label;

        public SensitivityLabelPipeBind()
        {
            _labelId = null;
            _labelName = string.Empty;
        }

        public SensitivityLabelPipeBind(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(nameof(id));
            }

            if (Guid.TryParse(id, out Guid labelId))
            {
                _labelId = labelId;
            }
            else
            {
                _labelName = id;
            }

            _label = null;
        }

        public SensitivityLabelPipeBind(Guid id)
        {
            _labelId = id;
            _labelName = string.Empty;
            _label = null;
        }

        public SensitivityLabelPipeBind(Model.Graph.Purview.InformationProtectionLabel label)
        {
            _labelId = label.Id;
            _labelName = label.Name;
            _label = label;
        }

        public string LabelName => _labelName;

        public Guid? LabelId => _labelId;

        public Model.Graph.Purview.InformationProtectionLabel Label => _label;

        /// <summary>
        /// Tries to look up the Label by the Label Name
        /// </summary>
        /// <param name="connection">Connection that can be used to query Microsoft Graph for the available sensitivity labels</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <returns>The the sensitivity label that matches the name set in this pipebind or NULL if no match found</returns>
        public Model.Graph.Purview.InformationProtectionLabel GetLabelByNameThroughGraph(PnPConnection connection, ApiRequestHelper requestHelper)
        {
            if (string.IsNullOrEmpty(_labelName)) return null;

            string url;
            if (connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly)
            {
                url = "/beta/security/informationProtection/sensitivityLabels";
            }
            else
            {
                url = "/beta/me/security/informationProtection/sensitivityLabels";
            }

            var availableLabels = requestHelper.GetResultCollection<Model.Graph.Purview.InformationProtectionLabel>($"https://{connection.GraphEndPoint}/{url}");
            return availableLabels.FirstOrDefault(l => l.Name == _labelName);
        }
    }
}