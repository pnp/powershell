namespace PnP.PowerShell.Commands.Base.PipeBinds;

public class ListItemVersionPipeBind
{
    public ListItemVersionPipeBind(string versionLabel)
    {
        if (int.TryParse(versionLabel, out var id))
        {
            Id = id;
        }
        else
        {
            VersionLabel = versionLabel;
        }
    }

    public ListItemVersionPipeBind(int id)
    {
        Id = id;
    }

    public int Id { get; } = -1;

    public string VersionLabel { get; }
}