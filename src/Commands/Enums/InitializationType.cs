namespace PnP.PowerShell.Commands.Enums
{
    public enum InitializationType
    {
        Unknown,
        Credentials,
        Token,
        ClientIDSecret,
        ClientIDCertificate,
        AADNativeApp,
        AADAppOnly,
        InteractiveLogin,
        DeviceLogin,
        Graph,
        GraphDeviceLogin,
        SPOManagementShell,
        ManagedIdentity,
        EnvironmentVariable,
        AzureADWorkloadIdentity
    }
}
