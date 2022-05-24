namespace PnP.PowerShell.Commands.Model
{
    public sealed class SubscribeSharePointNewsDigestStatus
    {
        internal SubscribeSharePointNewsDigestStatus(string account, bool enabled)
        {
            Account = account ?? throw new System.ArgumentNullException(nameof(account));
            Enabled = enabled;
        }

        public string Account { get; }
        public bool Enabled { get; }
    }
}
