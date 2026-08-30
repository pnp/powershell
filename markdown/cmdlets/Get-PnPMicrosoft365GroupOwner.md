---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMicrosoft365GroupOwner
---
  
# Get-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : at least Group.Read.All
  * Microsoft Graph API : Directory.Read.All

Gets owners of a particular Microsoft 365 Group

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind> 
```

## DESCRIPTION

Allows to retrieve owners of Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupOwner -Identity $groupId
```

Retrieves all the owners of a specific Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupOwner -Identity $group
```

Retrieves all the owners of a specific Microsoft 365 Group based on the group's object instance

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group.

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


