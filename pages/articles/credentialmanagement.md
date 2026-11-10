# Credential Management

PnP PowerShell is the ultimate library to execute cmdlets unattended in scripts, Azure Functions or Azure Automation.

However, in order to automate authentication you need to safely store these credentials. You should -never- store them in your scripts.

We currently recommend the Microsoft provided Secret Management and Secret Store modules to set up a vault which PnP PowerShell can use to store and retrieve credentials. This works the same way on every platform PnP PowerShell runs on, which is why it is the recommended option.

If you do not register a default vault, `Add-PnPStoredCredential`, `Get-PnPStoredCredential` and `Remove-PnPStoredCredential` fall back to the credential store built into your operating system, described under [Storing credentials without a vault](#storing-credentials-without-a-vault) below.

## Install the required modules

```powershell
Install-Module -Name "Microsoft.PowerShell.SecretManagement"
Install-Module -Name "Microsoft.PowerShell.SecretStore"
```

## Configuring the vault

After installing the module, create and register a vault:

```powershell
Register-SecretVault -Name "SecretStore" -ModuleName "Microsoft.PowerShell.SecretStore" -DefaultVault
Set-SecretStoreConfiguration -Authentication None
```

The last cmdlet, where you set the authentication to `None` means that you will allow access to the secret store without requiring you to enter a password to unlock the vault. This is optional. Notice that you will be asked several times to provide a password the moment you create a new vault. If you intend to use the native out of the box functionality of PnP PowerShell with the Secret Management modules, notice that the authentication is required to set to `None`.

## Adding a secret
### Secret Management Module

```powershell
Set-Secret -Name [yourlabel] -Secret (Get-Credential)
```

### PnP PowerShell
```powershell
Add-PnPStoredCredential -Name [yourlabel] -Username [username]
```

You will be prompted to provide a password.

## Retrieving a secret 
### Secret Management Module

```powershell
Connect-PnPOnline -Url https://yourtenant.sharepoint.com -Credentials (Get-Secret -Name [yourlabel])
```

### PnP PowerShell

```powershell
Connect-PnPOnline -Url https://yourtenant.sharepoint.com -Credentials [yourlabel]
```

### Advanced usage of secrets

You can add a secret with a label that reflects your tenant url, e.g. 

```powershell
Set-Secret -Name "https://yourtenant.sharepoint.com" -Secret (Get-Credential)
```

Now you can simply do this:

```powershell
Connect-PnPOnline -Url "https://yourtenant.sharepoint.com"
```

PnP PowerShell will check the vault if a secret is present with the label matching the URL and it will use those credentials. When this happens, `Connect-PnPOnline -Verbose` writes a one-line verbose message naming the stored credential it selected. Notice that a URL like `https://yourtenant.sharepoint.com/sites/demo` will also match the secret. You can create multiple secrets too. PnP PowerShell will then try to match the most complete label first, e.g. a secret ending on /sites/demo1 will precede the one without that ending.

## Removing a secret 

### Secret Management
```powershell
Remove-Secret -Name [yourlabel] -Vaultname [VaultName]
```

### PnP PowerShell
```powershell
Remove-PnPStoredCredential -Name [yourlabel]
```

## Storing credentials without a vault

When no default vault is registered, the PnP PowerShell cmdlets above use the credential store that comes with your operating system. Nothing needs to be installed for this on Windows and macOS, and the credentials are stored under a name prefixed with `PnPPS:`.

| Platform | Credential store | Prerequisites |
| -------- | ---------------- | ------------- |
| Windows | Windows Credential Manager | None |
| macOS | Keychain | None |
| Linux | Secret Service | A Secret Service provider such as GNOME Keyring or KWallet must be installed, running and unlocked |

On Linux this means a headless machine, a container or an SSH session without a running keyring daemon typically has no Secret Service available. `Add-PnPStoredCredential` will then fail with an error explaining that the credential could not be stored, and `Get-PnPStoredCredential` will return nothing. On such machines, register a default vault with the Secret Management modules as described above, or pass credentials to `Connect-PnPOnline` in another way.

Note that a credential stored in the operating system credential store is not visible to the Secret Management module, and the other way around. If you register a default vault after having stored credentials natively, PnP PowerShell will look in the vault only and you will need to add the credentials there again.
