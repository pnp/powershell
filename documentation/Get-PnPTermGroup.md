---
Module Name: PnP.PowerShell
title: Get-PnPTermGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTermGroup.html
---
 
# Get-PnPTermGroup

## SYNOPSIS
Returns a taxonomy term group

## SYNTAX

```powershell
Get-PnPTermGroup
 [-Identity <TaxonomyTermGroupPipeBind>]
 [-TermStore <TaxonomyTermStorePipeBind>]
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION

Allows to retrieve a taxonomy term group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTermGroup
```

Returns all Term Groups in the site collection termstore

### EXAMPLE 2
```powershell
Get-PnPTermGroup -Identity "Departments"
```

Returns the termgroup named "Departments" from the site collection termstore

### EXAMPLE 3
```powershell
Get-PnPTermGroup -Identity ab2af486-e097-4b4a-9444-527b251f1f8d
```

Returns the termgroup with the given ID from the site collection termstore

## PARAMETERS

### -Identity
Name of the taxonomy term group to retrieve.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
Aliases: GroupName

Required: False
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
Aliases: TermStoreName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

