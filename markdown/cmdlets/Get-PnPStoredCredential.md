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
Get a credential

## SYNTAX

```powershell
Get-PnPStoredCredential -Name <String> 
```

## DESCRIPTION
Returns a stored credential from the Windows Credential Manager or Mac OS Key Chain Entry.

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

