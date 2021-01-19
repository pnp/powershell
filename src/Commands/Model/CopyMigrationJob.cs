namespace PnP.PowerShell.Commands.Model
{
    internal class CopyMigrationJob
    {
        public int JobState { get; set; }
        public string[] Logs { get; set; }
    }
}