# Persisted Login

Starting with PnP PowerShell 3.0, the `Connect-PnPOnline` cmdlet allows `-PersistLogin` to be provided. Documentation for it can be [found here](../cmdlets/Connect-PnPOnline.md#-persistlogin). This parameter persists tokens acquired through delegated authentication or certificate-based app-only authentication in a local cache, which can be reused by subsequent connections.

This feature is particularly useful for scenarios where you need to run scripts or tasks that require authentication but do not want to enter your credentials every time. The risk obviously will be that anyone with access to your machine can use the token to authenticate against your tenant.

## Where is the token stored
The token is stored in a file in the `%LOCALAPPDATA%\.m365pnppowershell` folder on Windows or `$HOME/.m365pnppowershell` on Linux and macOS. The file is encrypted using the Data Protection API (DPAPI) on Windows, Keychain on macOS or Secret Service on Linux.

This means that the token is securely stored and cannot be easily accessed by unauthorized users nor can it be copied to another machine as the encryption is tied to the machine on which it has been generated. However, it is important to note that if you share your machine with others, they may be able to access the token if they have access to your user profile.

## How does it work
When you use the `-PersistLogin` parameter with the `Connect-PnPOnline` cmdlet, PnP PowerShell authenticates as normal and stores the resulting token data in the local cache. The next time you run `Connect-PnPOnline`, PnP PowerShell checks whether a valid token exists for the tenant and client ID. If one is found, it is reused. Delegated authentication can therefore avoid prompting while its refresh token remains valid.

For certificate-based app-only authentication, the certificate, tenant and client ID must still be supplied on each connection because the cache does not store the certificate or its password. A cached access token is reused while valid; afterwards the supplied certificate is used to acquire a new token.

You do not need to specify the `-PersistLogin` parameter again for subsequent connections unless you want to change the behavior.

## Listing persisted logins

Use `Get-PnPPersistedLogin` to list the tenant URLs, client IDs and authentication types registered to use the persisted token cache:

```powershell
Get-PnPPersistedLogin
```

## Clearing the persisted login
If you want to clear the persisted login and remove the stored token, you can connect to the tenant for which you would like to remove the stored token first and then use the `Disconnect-PnPOnline` cmdlet with the `-ClearPersistedLogin` option. Documentation for it can be [found here](../cmdlets/Disconnect-PnPOnline.md#-clearpersistedlogin). This will delete the token from the local file and require you to authenticate again the next time you run `Connect-PnPOnline`.

## FAQ

### Can I use `-PersistLogin` in Azure Automation, Azure Functions or a container?

No. These environments are ephemeral: the file system does not survive the run, and the work may be spread over instances which do not share a user profile, so there is nothing to reuse a token from. The cache is written for a workstation you come back to.

Authenticate as you normally would on each run instead, using a certificate, a managed identity or a workload identity. Leaving `-PersistLogin` off costs nothing there, as the first connection of a run has to authenticate regardless.

### Can I use `-PersistLogin` with an app only context?

Yes, certificate-based app-only authentication supports `-PersistLogin`. You must continue to provide the certificate, tenant and client ID on each connection because the certificate and its password are not stored. Legacy SharePoint ACS client-secret authentication does not support `-PersistLogin`.

### Do I still need my own application registration in Entra ID when using `-PersistLogin`?

Yes, this is still required.

### Can I use a different application registration for `-PersistLogin` for different tenants or even site collections on the same tenant?

Yes, that is supported. Just use it as described above and it will store the token for the tenant or site collection you are connecting to.