---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPMicrosoft365GroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPMicrosoft365GroupOwner
---
  
# Clear-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes all current owners from a particular Microsoft 365 Group (aka Unified Group)

## SYNTAX

```powershell
Clear-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind> [<CommonParameters>]
```

## DESCRIPTION

Allows to remove all current owners from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPMicrosoft365GroupOwner -Identity "Project Team"
```

Removes all the current owners from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group to remove all owners from

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-owners)