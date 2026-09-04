---
Module Name: PnP.PowerShell
title: Remove-PnPMicrosoft365Group
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365Group.html
---
 
# Remove-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes one Microsoft 365 Group

## SYNTAX

```powershell
Remove-PnPMicrosoft365Group -Identity <Microsoft365GroupPipeBind>  
```

## DESCRIPTION

Allows to remove Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPMicrosoft365Group -Identity $groupId
```

Removes an Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
Remove-PnPMicrosoft365Group -Identity $group
```

Removes the provided Microsoft 365 Group

### EXAMPLE 3
```powershell
Get-PnPMicrosoft365Group | ? Visibility -eq "Public" | Remove-PnPMicrosoft365Group
```

Removes all the public Microsoft 365 Groups

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

