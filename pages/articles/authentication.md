# Authentication

## Setting up Access

PnP PowerShell allows you to authenticate with credentials to your tenant. However, due to changes in the underlying SDKs we require you first to register a Azure AD Application which will allow you to authenticate.

The easiest way to do this by using a built-in cmdlet:

```powershell
Register-PnPManagementShellAccess
```

You'll notice that the cmdlet is not called `Register-PnPPowerShellAccess`. This is due that both PnP PowerShell and the CLI for Microsoft 365 make use of this Azure AD application. 

> [!Important]
> You need to run this cmdlet with an identity that has write access to the Azure AD.
> You are not creating a new application in the sense of something that runs in your Azure AD tenant. You're only adding a registration to your Azure AD, a so called 'consent' for people in your tenant to use that application. The access rights the application requires are delegate only, so you will always have to provide credentials or another way of identifying the user actually using that application.

During execution of the cmdlet you will be talked through the consent flow. This means that a browser window will open, you will be asked to authenticate, and you will be asked to consent to a number of permissions. After this permissions has been granted a new entry will show up if you navigate to `Enterprise Applications` in your Azure AD. If you want to revoke the consent you can simply remove the entry from the Enterprise Applications. 

## Authenticating with Credentials

Enter

```powershell
Connect-PnPOnline -Url https://contoso.sharepoint.com -Credentials (Get-Credential)
```

and you will be prompted for credentials. 

## Authenticating with pre-stored credentials using the Windows Credential Manager (Windows only)

```powershell
Add-PnPStoreCredential -Name "yourlabel" -Username youruser@domain.com
```

You will be prompted to provide a password. After that you can login using:

```powershell
Connect-PnPOnline -Url https://contoso.sharepoint.com -Credentials "yourlabel"
```

## Authenticating with pre-stored credentials using the Secrets Management Module from Microsoft (Multi-Platform)

```powershell
Install-Module -Name Microsoft.PowerShell.SecretManagement -AllowPrerelease
Install-Module -Name Microsoft.PowerShell.SecretStore -AllowPrelease
Set-SecretStoreConfiguration
Set-Secret -Name "yourlabel" -Secret (Get-Credential)
```

This creates a new secret vault on your computer. You will be asked to provide a password to access the vault. If you access the vault you will be prompted for that password. In case you want to want to write automated scripts you will have to turn off this password prompt as follows:

```powershell
Set-SecretStoreConfiguration -Authentication None
```

For more information about these cmdlets, check out the github repositories: https://github.com/powershell/secretmanagement and https://github.com/powershell/secretstore.

After you set up the vault and you added a credential

```powershell
Connect-PnPOnline -Url https://contoso.sharepoint.com -Credentials (Get-Secret -Name "yourlabel")
```



