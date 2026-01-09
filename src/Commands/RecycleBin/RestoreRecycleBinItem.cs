using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.RecycleBin;
[Cmdlet(VerbsData.Restore, "PnPRecycleBinItem")]
[OutputType(typeof(void))]
public class RestoreRecycleBinItem : PnPSharePointCmdlethttps://github.com/pnp/powershell/pull/5199/conflict?name=src%252FCommands%252FUtilities%252FRecycleBinUtility.cs&ancestor_oid=464e3967380956cbb32b389afb9657ae6b9b2916&base_oid=09a7f7d9d33cfaea67f77b29022ef9e3f2a2f341&head_oid=8b1b9c9d30f37b07ac1d51cfd1739d24181fb366
{
    private const string ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID = "Restore Multiple Items By Id";
    private const string ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID = "Restore Single Items By Id";

    [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID, Position = 0, ValueFromPipeline = true)]
    public RecycleBinItemPipeBind Identity;

    [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID)]
    public SwitchParameter Force;

    [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID)] 
    public int RowLimit;

    [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID)]
    public string[] IdList;

    protected override void ExecuteCmdlet()
    {
        switch (ParameterSetName)
        {
            case ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID:
                RecycleBinUtility.RestoreRecycleBinItemSingle(ClientContext, this);
                break;

            case ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID:
                RecycleBinUtility.RestoreRecycleBinItemInBulk(HttpClient, ClientContext, IdList, this);
                break;

        }
    }
}
