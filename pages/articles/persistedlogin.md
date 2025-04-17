# Persisted Login

Starting with PnP PowerShell 3.0, the `Connect-PnPOnline` cmdlet has been updated to allow `-PersistLogin` to be provided. Documentation for it can be [found here](../cmdlets/Connect-PnPOnline.md#-persistlogin). This parameter allows you to persist the delegated authentication token retrieved through an interactive login in a local file on your machine, which can be used for subsequent connections without requiring re-authentication.  

This feature is particularly useful for scenarios where you need to run scripts or tasks that require authentication but do not want to enter your credentials every time. The risk obviously will be that anyone with access to your machine can use the token to authenticate against your tenant.

## Where is the token stored
The token is stored in a file in the `%LOCALAPPDATA%\.m365pnppowershell` folder on Windows or `$HOME/.m365pnppowershell` on Linux and MacOS. The file is encrypted using the Data Protection API (DPAPI) on Windows or the Keychain on MacOS and Linux.  

This means that the token is securely stored and cannot be easily accessed by unauthorized users nor can it be copied to another machine as the encryption is tied to the machine on which it has been generated. However, it is important to note that if you share your machine with others, they may be able to access the token if they have access to your user profile.

## How does it work
When you use the `-PersistLogin` parameter with the `Connect-PnPOnline` cmdlet, PnP PowerShell will authenticate you as normal but will also store the refresh token in a local file. The next time you run `Connect-PnPOnline`, PnP PowerShell will check if a valid token already exists in the local file for the tenant or site you are trying to connect to. If a valid token is found, it will be used to authenticate without prompting for credentials. If no valid token is found, PnP PowerShell will prompt for credentials as normal.

You do not need to specify the `-PersistLogin` parameter again for subsequent connections unless you want to change the behavior.

## Clearing the persisted login
If you want to clear the persisted login and remove the stored token, you can connect to the tenant for which you would like to remove the stored token first and then use the `Disconnect-PnPOnline` cmdlet with the `-ClearPersistedLogin` option. Documentation for it can be [found here](../cmdlets/Disconnect-PnPOnline.md#-clearpersistedlogin). This will delete the token from the local file and require you to authenticate again the next time you run `Connect-PnPOnline`.

## FAQ

### Can I use `-PersistLogin` in Azure?

No you cannot, as there are no profiles folders in Azure.

### Can I use `-PersistLogin` with an app only context?

No, it is meant to be used for an interactive delegated authentication context only. If you want to use an app only context, you can just use the parameters with the `Connect-PnPOnline` cmdlet that support app only authentication as normal. Documentation for it can be [found here](../cmdlets/Connect-PnPOnline.md#app-only-with-azure-active-directory).

### Do I still need my own application registration in Entra ID when using `-PersistLogin`?

Yes, this is still required.

### Can I use a different application registration for `-PersistLogin` for different tenants or even site collections on the same tenant?

Yes, that is supported. Just use it as described above and it will store the token for the tenant or site collection you are connecting to.