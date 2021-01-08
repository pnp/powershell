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

```powershell
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
