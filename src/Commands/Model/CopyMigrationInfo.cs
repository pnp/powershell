namespace PnP.PowerShell.Commands.Model
{
    public class CopyMigrationInfo
    {
        public string EncryptionKey { get; set; }
        public string JobId { get; set; }
        public string JobQueueUri { get; set; }
        public string[] SourceListItemUniqueIds { get; set; }
    }
}