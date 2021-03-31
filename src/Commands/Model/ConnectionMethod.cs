namespace PnP.PowerShell.Commands.Model
{
    public enum ConnectionMethod
    {
        Unspecified,
        WebLogin,
        Credentials,
        AccessToken,
        AzureADAppOnly,
        AzureADNativeApplication,
        ADFS,
        GraphDeviceLogin,
        DeviceLogin,
        ManagedIdentity,
        ACSAppOnly
    }
}
