---
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPStoredCredential
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPStoredCredential.html
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
schema: 2.0.0
Module Name: PnP.PowerShell
---
  
# Get-PnPStoredCredential

## SYNOPSIS
Get a credential

## SYNTAX

```powershell
Get-PnPStoredCredential -Name <String> 
```

## DESCRIPTION
Returns a stored credential. If a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the credential is read from that vault. If not, it is read from the credential store native to the operating system: the Windows Credential Manager on Windows, the Keychain on macOS and the Secret Service on Linux. Reading a credential from the Linux Secret Service requires a provider such as GNOME Keyring or KWallet to be installed and unlocked. See [Credential Management](https://pnp.github.io/powershell/articles/credentialmanagement.html) for the details.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPStoredCredential -Name O365
```

Returns the credential associated with the specified identifier

## PARAMETERS

### -Name
The credential to retrieve.

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


