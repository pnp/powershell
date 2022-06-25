using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteSensitivityLabel")]
    [OutputType(typeof(string))]
    public class GetSiteSensitivityLabel : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var label = GraphHelper.GetAsync<Model.Graph.Purview.InformationProtectionLabel>(Connection, $"{Connection.Url}/_api/site/classification", AccessToken).GetAwaiter().GetResult();
            WriteObject(label, false);
        }
    }
}