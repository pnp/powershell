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

```powershell
Get-PnPStoredCredential -Name <String>
Get-PnPStoredCredential -List
```

## DESCRIPTION
Returns a stored credential. If a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the credential is read from that vault. If not, it is read from the credential store native to the operating system: the Windows Credential Manager on Windows, the Keychain on macOS and the Secret Service on Linux. Reading a credential from the Linux Secret Service requires a provider such as GNOME Keyring or KWallet to be installed and unlocked. Use `-List` to enumerate credential names that are currently stored instead of retrieving one by name. See [Credential Management](https://pnp.github.io/powershell/articles/credentialmanagement.html) for the details.

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

Returns the names of all stored PnP PowerShell credentials.

## PARAMETERS

### -List
Returns the names of all stored PnP PowerShell credentials.

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

