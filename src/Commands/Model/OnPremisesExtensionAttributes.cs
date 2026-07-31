namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Extension attributes 1-15 of one Microsoft 365 Group. Microsoft Graph only populates these for groups that are synchronized from an on-premises Active Directory,
    /// so they are empty for a cloud only group, also when the equivalent CustomAttribute1-15 properties do hold a value in Exchange Online.
    /// </summary>
    public class OnPremisesExtensionAttributes
    {
        public string ExtensionAttribute1 { get; set; }
        public string ExtensionAttribute2 { get; set; }
        public string ExtensionAttribute3 { get; set; }
        public string ExtensionAttribute4 { get; set; }
        public string ExtensionAttribute5 { get; set; }
        public string ExtensionAttribute6 { get; set; }
        public string ExtensionAttribute7 { get; set; }
        public string ExtensionAttribute8 { get; set; }
        public string ExtensionAttribute9 { get; set; }
        public string ExtensionAttribute10 { get; set; }
        public string ExtensionAttribute11 { get; set; }
        public string ExtensionAttribute12 { get; set; }
        public string ExtensionAttribute13 { get; set; }
        public string ExtensionAttribute14 { get; set; }
        public string ExtensionAttribute15 { get; set; }
    }
}
