namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AppIdentity
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return $"{DisplayName}, {Id}";
        }
    }
}