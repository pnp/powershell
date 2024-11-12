using System.Management.Automation;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Get, "PnPRetentionLabel")]
    public class GetLabel : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        [Alias("ValuesOnly")]
        public SwitchParameter Raw;

        protected override void ExecuteCmdlet()
        {
            if (!ParameterSpecified(nameof(List)))
            {
                var tags = Connection.PnPContext.Site.GetAvailableComplianceTags();
                WriteObject(tags, true);
            }
            else
            {
                var list = List.GetList(Connection.PnPContext);
                if (null != list)
                {
                    var tag = list.GetComplianceTag();
                    if (null == tag)
                    {
                        WriteWarning("No label found for the specified list/library.");
                    }
                    else
                    {
                        if (ParameterSpecified(nameof(Raw)))
                        {
                            WriteObject(tag);
                        }
                        else
                        {
                            WriteObject($"The label '{tag.TagName}' is set to the specified list or library. ");
                            WriteObject($"Block deletion: '{tag.BlockDelete}'");
                            WriteObject($"Block editing: '{tag.BlockEdit}'");
                        }
                    }
                }
                else
                {
                    throw new PSArgumentException("List not found");
                }
            }
        }
    }
}