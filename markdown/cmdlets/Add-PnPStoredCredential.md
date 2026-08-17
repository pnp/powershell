---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPStoredCredential.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPStoredCredential
Module Name: PnP.PowerShell
---
   
# Add-PnPStoredCredential

## SYNOPSIS
Adds a credential to a secret vault, the Windows Credential Manager, the macOS Keychain or the Linux Secret Service.

## SYNTAX

```powershell
Add-PnPStoredCredential -Name <String> -Username <String> [-Password <SecureString>] [-Overwrite]
 
```

## DESCRIPTION
Adds an entry to the credential store of your platform. If a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the credential is stored in that vault. If not, it is stored in the credential store native to the operating system: the Windows Credential Manager on Windows, the Keychain on macOS and the Secret Service on Linux. Storing a credential on Linux without a default vault requires a Secret Service provider such as GNOME Keyring or KWallet to be installed and unlocked; if none is available, the cmdlet will report that it could not store the credential rather than silently discarding it. See [Credential Management](https://pnp.github.io/powershell/articles/credentialmanagement.html) for the details.

If you add an entry in the form of the URL of your tenant/server PnP PowerShell will check if that entry is available when you connect using Connect-PnPOnline. If it finds a matching URL it will use the associated credentials.

If you add a Credential with a name of "https://yourtenant.sharepoint.com" it will find a match when you connect to "https://yourtenant.sharepoint.com" but also when you connect to "https://yourtenant.sharepoint.com/sites/demo1". Of course you can specify more granular entries, allow you to automatically provide credentials for different URLs.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com
```

You will be prompted to specify the password and a new entry will be added with the specified values

### EXAMPLE 2
```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
```

A new entry will be added with the specified values

### EXAMPLE 3
```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
Connect-PnPOnline -Url "https://tenant.sharepoint.com/sites/mydemosite"
```

A new entry will be added with the specified values, and a subsequent connection to a sitecollection starting with the entry name will be made. Notice that no password prompt will occur.

## PARAMETERS

### -Name
The credential to set

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
Use parameter to overwrite an existing macOS Keychain entry. Not required on Windows or Linux.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
If not specified you will be prompted to enter your password. 
If you want to specify this value use ConvertTo-SecureString -String 'YourPassword' -AsPlainText -Force

```yaml
Type: SecureString
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)



