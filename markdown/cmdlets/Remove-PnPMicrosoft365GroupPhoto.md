---
Module Name: PnP.PowerShell
title: Remove-PnPMicrosoft365GroupPhoto
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupPhoto.html
---
 
# Remove-PnPMicrosoft365GroupPhoto

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes the profile photo from a particular Microsoft 365 Group

## SYNTAX

```powershell
Remove-PnPMicrosoft365GroupPhoto -Identity <Microsoft365GroupPipeBind>
  
```

## DESCRIPTION

Allows to remove profile photo from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMicrosoft365GroupPhoto -Identity "Project Team"
```

Removes profile photo from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group to remove profile photo from

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