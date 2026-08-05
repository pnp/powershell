---
Module Name: PnP.PowerShell
title: Get-PnPTermLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTermLabel.html
---
 

# Get-PnPTermLabel

## SYNOPSIS
Returns all or a specific translation label for a term.

## SYNTAX

### By Term Id
```
Get-PnPTermLabel -Term <Guid> [-Lcid <Int32>] [-TermStore <TaxonomyTermStorePipeBind>] 
```

### By Term Name
```
Get-PnPTermLabel -Term <String> [-Lcid <Int32>] -TermSet <TaxonomyTermSetPipeBind>
 -TermGroup <TaxonomyTermGroupPipeBind> [-TermStore <TaxonomyTermStorePipeBind>] 
```

## DESCRIPTION
This cmdlets allows to retrieve all or a specific translation label for a term

## EXAMPLES

### Example 1
```powershell
Get-PnPTermLabel -Term af8601d6-d925-46dd-af7b-4a58515ffd83
```

Returns all translation labels for a term

### Example 2
```powershell
Get-PnPTermLabel -Term af8601d6-d925-46dd-af7b-4a58515ffd83 -Lcid 1033
```

Returns all translation labels for a term for a specific language

### Example 3
```powershell
Get-PnPTermLabel -Term "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```

Returns all translation labels for a term

## PARAMETERS


### -Lcid
Specify the codepage value for a language, for instance 1033 for English.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Term
The term to retieve the labels for

```yaml
Type: Guid
Parameter Sets: By Term Id
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: By Termset
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
The TermGroup containing the termset, required when referring to the term by name.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: By Termset
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermSet
The TermSet to containing the term, required when referring to the term by name.

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: By Termset
Aliases:

Required: True
Position: Named
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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

