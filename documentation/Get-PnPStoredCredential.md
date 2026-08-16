---
Module Name: PnP.PowerShell
title: Get-PnPStoredCredential
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPStoredCredential.html
---
 
# Get-PnPStoredCredential

## SYNOPSIS
Get a credential or list stored credential names

## SYNTAX

### Name (Default)
```powershell
Get-PnPStoredCredential -Name <String> 
```

### List
```powershell
Get-PnPStoredCredential -List 
```

## DESCRIPTION
Returns a stored credential. If a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the credential is read from that vault. If not, it is read from the credential store native to the operating system: the Windows Credential Manager on Windows, the Keychain on macOS and the Secret Service on Linux. Reading a credential from the Linux Secret Service requires a provider such as GNOME Keyring or KWallet to be installed and unlocked. Use `-List` to enumerate the names credentials are stored under instead of retrieving one by name; each name it returns can be passed straight back in through `-Name`. See [Credential Management](https://pnp.github.io/powershell/articles/credentialmanagement.html) for the details.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPStoredCredential -Name O365
```

Returns the credential associated with the specified identifier.

### EXAMPLE 2
```powershell
Get-PnPStoredCredential -List
```

Returns the names credentials are stored under.

## PARAMETERS

### -List
Returns the names credentials are stored under, in the same credential store `-Name` reads from.

When the credentials are held in the credential store native to the operating system, only the entries written by PnP PowerShell are returned. When a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the vault is a general purpose secret store and nothing marks the secrets that PnP PowerShell wrote, so the result may also include credentials in that vault that were stored by something else.

Listing on Linux requires `secret-tool` (the `libsecret-tools` package). If the credential store cannot be read, a warning is written explaining why, so that an empty result is never mistaken for an empty credential store.

```yaml
Type: SwitchParameter
Parameter Sets: List

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The credential to retrieve.

```yaml
Type: String
Parameter Sets: Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

