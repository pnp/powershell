---
Module Name: PnP.PowerShell
title: Move-PnPTerm
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPTerm.html
---

# Move-PnPTerm

## SYNOPSIS

Moves a taxonomy term to another term set or term

## SYNTAX

### Move to term set by Term Id
```
Move-PnPTerm -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTermSet 95e13729-3ccf-4ec8-998c-78e9ef1daa0b -TargetTermGroup b2645144-5757-4cd7-b7f9-e5d24757addf
```
### Move to term set by Term Name
```
Move-PnPTerm -Identity "Test" -TargetTermSet "TestTermSet1" -TermSet "OperationLevel-1 Test" -TermGroup "FromPowerAutomate" -TargetTermGroup "TestingGroup"
```
### Move to term
```
Move-PnPTerm -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTerm 2ad90b20-b5c0-4544-ac64-25e32d51fa3b -MoveToTerm
```

## DESCRIPTION

This cmdlet moves a taxonomy term to another term set or term

## EXAMPLES

### Example 1
```powershell
Move-PnPTerm -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTermSet 95e13729-3ccf-4ec8-998c-78e9ef1daa0b -TargetTermGroup b2645144-5757-4cd7-b7f9-e5d24757addf
```

Moves term by id to term set.

### Example 2
```powershell
Move-PnPTerm -Identity "Test" -TargetTermSet "TestTermSet1" -TermSet "OperationLevel-1 Test" -TermGroup "FromPowerAutomate" -TargetTermGroup "TestingGroup"
```

Moves term by name to term set.

### Example 3
```powershell
Move-PnPTerm -Identity d67966b0-3b60-4331-8dc4-0b5a2ca730fc -TargetTerm 2ad90b20-b5c0-4544-ac64-25e32d51fa3b -MoveToTerm
```

Moves a term to another term by its identifier.

## PARAMETERS

### -Identity
The identifier of the term that needs to be moved, either in the form of its name or its GUID

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

### -TargetTermSet
The identifier of the term set where the term needs to be moved, either in the form of its name or its GUID

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: By Term Id, By Term Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetTermGroup
The identifier of the term group where the term needs to be moved, either in the form of its name or its GUID

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: By Term Id, By Term Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermSet
The identifier of the term set where the term is present, in the form of its name

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: By Term Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermGroup
The identifier of the term set where the term group is present, in the form of its name

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: By Term Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetTerm
The identifier of the term where the term needs to be moved, in the form of its GUID

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

### -MoveToTerm
To be provided if the term needs to be moved to another term

```yaml
Type: SwitchParameter
Parameter Sets: Move To Term

Required: True
Position: Named
Default value: None
Accept pipeline input: False
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