---
Module Name: PnP.PowerShell
title: Remove-PnPTermGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTermGroup.html
---
 

# Remove-PnPTermGroup

## SYNOPSIS
Removes a taxonomy term group and all its term sets.

## SYNTAX

```powershell
Remove-PnPTermGroup -Identity <TaxonomyTermGroupPipeBind> [-TermStore <TaxonomyTermStorePipeBind>] [-Force]
```

## DESCRIPTION
This cmdlet removes a term group and all the contained term sets.

## EXAMPLES

### Example 1
```powershell
Remove-PnPTermGroup -Identity 3d9e60e8-d89c-4cd4-af61-a010cf93b380
```

Removes the specified term group.

### Example 2
```powershell
Remove-PnPTermGroup -Identity "Corporate"
```
Removes the specified term group.

### Example 3
```powershell
Remove-PnPTermGroup -Identity "HR" -Force
```

Removes the specified term group without prompting for confirmation.

## PARAMETERS

### -Identity
The name or GUID of the group to remove.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
Aliases: GroupName

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
Term store to use; if not specified the default term store is used.

```yaml
Type: TaxonomyTermStorePipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

