---
Module Name: PnP.PowerShell
title: Restore-PnPDeletedMicrosoft365Group
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPDeletedMicrosoft365Group.html
---
 
# Restore-PnPDeletedMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Restores one deleted Microsoft 365 Group

## SYNTAX

```powershell
Restore-PnPDeletedMicrosoft365Group -Identity <Microsoft365GroupPipeBind> 
 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Restores a deleted Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
$group = Get-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
Restore-PnPDeletedMicrosoft365Group -Identity $group
```

Restores the provided deleted Microsoft 365 Group

## PARAMETERS

### -Identity
The Identity of the deleted Microsoft 365 Group

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/directory-deleteditems-restore)