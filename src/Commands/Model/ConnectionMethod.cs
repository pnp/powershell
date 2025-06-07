namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// The authentication types that can be used to create a connection
    /// </summary>
    public enum ConnectionMethod
    {
        Unspecified,
        WebLogin,

        /// <summary>
        /// Using interactive logon or by passing in credentials
        /// </summary>
        Credentials,

        /// <summary>
        /// By passing in an access token to the connect
        /// </summary>
        AccessToken,

        AzureADAppOnly,
        AzureADNativeApplication,
        ADFS,
        GraphDeviceLogin,

        /// <summary>
        /// Using a Device Login
        /// </summary>
        DeviceLogin,

        /// <summary>
        /// Using a System Assigned or User Assigned Managed Identity
        /// </summary>
        ManagedIdentity,

        AzureADWorkloadIdentity,
        FederatedIdentity
    }
}
