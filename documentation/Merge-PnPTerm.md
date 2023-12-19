---
Module Name: PnP.PowerShell
title: Merge-PnPTerms
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Merge-PnPTerm.html
---

# Merge-PnPTerms

## SYNOPSIS

Merges a taxonomy term into another term.

## SYNTAX

### Merge term set into term by Term Ids

```
Merge-PnPTerms -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTerm 95e13729-3ccf-4ec8-998c-78e9ef1daa0b 
```

## DESCRIPTION

This cmdlet merges a taxonomy term into another term.


## EXAMPLES

### Example 1
```powershell
Merge-PnPTerms -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTerm 95e13729-3ccf-4ec8-998c-78e9ef1daa0b 
```



## PARAMETERS

### -Identity
The identifier of the term that will be merged away, in the form of its GUID

```yaml
Type: TaxonomyTermPipeBind
Parameter Sets: (All)
Aliases: Term

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetTerm
The identifier of the term where the term will be merged into, in the form of its GUID

```yaml
Type: TaxonomyTermPipeBind
Parameter Sets: Move To Term
Aliases:

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