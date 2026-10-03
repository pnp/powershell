---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMicrosoft365GroupMember
---
  
# Get-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : at least Group.Read.All
  * Microsoft Graph API : Directory.Read.All

Returns the members of a particular Microsoft 365 Group

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind> [-UserType <String>] 
```

## DESCRIPTION
Returns the members of a particular Microsoft 365 Group

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupMember -Identity $groupId
```

Retrieves all the members of a specific Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupMember -Identity $group
```

Retrieves all the members of a specific Microsoft 365 Group based on the group's object instance

### EXAMPLE 3
```powershell
Get-PnPMicrosoft365GroupMember -Identity "Sales" | Where-Object UserType -eq Guest
```

Returns all the guest users of the Microsoft 365 Group named "Sales"

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group

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