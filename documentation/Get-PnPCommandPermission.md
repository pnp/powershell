---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPCommandPermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPCommandPermission
---

# Get-PnPCommandPermission

## SYNOPSIS
Returns declared API permissions and authorization guidance for a PnP PowerShell cmdlet.

## SYNTAX

```powershell
Get-PnPCommandPermission [-CommandName] <String>
```

## DESCRIPTION
Returns the delegated and application API permission sets declared on a PnP PowerShell cmdlet. Permissions within a set are all required, while multiple sets are alternatives.

For SharePoint cmdlets without declared permission metadata, the cmdlet returns conservative inferred guidance. Inferred permissions are suggested maximums and can be broader than the permissions actually required. SharePoint roles and permissions on target resources can also be required.

This cmdlet does not require a connection.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPCommandPermission -CommandName Get-PnPTeamsTeam
```

Returns the permission metadata declared for `Get-PnPTeamsTeam`.

### EXAMPLE 2
```powershell
"Get-PnPTerm" | Get-PnPCommandPermission
```

Returns declared permissions or inferred SharePoint and Term Store guidance for `Get-PnPTerm`.

## PARAMETERS

### -CommandName
The name of the PnP PowerShell cmdlet for which permission information should be returned. Tab completion is available for PnP cmdlet names.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)