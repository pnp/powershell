---
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPMicrosoft365GroupMember.html
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
applicable: SharePoint Online
title: Clear-PnPMicrosoft365GroupMember
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
   
# Clear-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes all current members from a particular Microsoft 365 Group

## SYNTAX

```powershell
Clear-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind> 
```

## DESCRIPTION

Allows to remove all current members from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPMicrosoft365GroupMember -Identity "Project Team"
```

Removes all the current members from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group to remove all members from

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)

