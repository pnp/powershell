# Credential Management

PnP PowerShell is the ultimate library to execute cmdlets unattended in scripts, Azure Functions or Azure Automation.

However, in order to automate authentication you need to safely store these credentials. You should -never- store them in your scripts.

We currently recommend the Microsoft provided Secret Management and Secret Store modules to set up a vault which PnP PowerShell can use to store and retrieve credentials.

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

PnP PowerShell will check the vault if a secret is present with the label matching the URL and it will use those credentials. Notice that a URL like `https://yourtenant.sharepoint.com/sites/demo` will also match the secret. You can create multiple secrets too. PnP PowerShell will then try to match the most complete label first, e.g. a secret ending on /sites/demo1 will proceed the one without that ending.

## Removing a secret 

### Secret Management
```powershell
Remove-Secret -Name [yourlabel] -Vaultname [VaultName]
```

### PnP PowerShell
```powershell
Remove-PnPStoredCredential -Name [yourlabel]
```
