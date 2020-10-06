---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpstoredcredential
schema: 2.0.0
title: Get-PnPStoredCredential
---

# Get-PnPStoredCredential

## SYNOPSIS
Get a credential

## SYNTAX

```
Get-PnPStoredCredential -Name <String> [<CommonParameters>]
```

## DESCRIPTION
Returns a stored credential from the Windows Credential Manager

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPStoredCredential -Name O365
```

Returns the credential associated with the specified identifier

### EXAMPLE 2
```powershell
Get-PnPStoredCredential -Name testEnvironment -Type OnPrem
```

Gets the credential associated with the specified identifier from the credential manager and then will return a credential that can be used for on-premises authentication

## PARAMETERS

### -Name
The credential to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)