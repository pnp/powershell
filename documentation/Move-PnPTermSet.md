---
Module Name: PnP.PowerShell
title: Move-PnPTermSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPTermSet.html
---

# Move-PnPTermSet

## SYNOPSIS

Moves taxonomy term set from one term group to another

## SYNTAX

### By Term Id
```
Move-PnPTermSet -Identity <Guid> -SourceTermGroup <Guid> -DestinationTermGroup <Guid> [-TermStore <TaxonomyTermStorePipeBind>]
```

### By Term Name
```
Move-PnPTermSet -Identity <String> -SourceTermGroup <String> -DestinationTermGroup <String> [-TermStore <TaxonomyTermStorePipeBind>]
```

## DESCRIPTION
This cmdlet moves taxonomy term set from one term group to another.

## EXAMPLES

### Example 1
```powershell
Move-PnPTermSet -Identity 81e0a4b8-701d-459c-ad61-a1c7a81810ff -SourceTermGroup 17e16b98-a8c2-4db6-a860-5c42dbc818f4  -DestinationTermGroup cf33d1cd-42d8-431c-9e43-3d8dab9ea8fd
```

Moves term set by id.

### Example 2
```powershell
Move-PnPTermSet -Identity "OperationLevel-1 Test" -SourceTermGroup "FromPowerAutomate" -DestinationTermGroup "DestinationTermGroup"
```

Moves term set by name.

## PARAMETERS

### -Identity
The identifier of the term set that needs to be moved, either in the form of its name or its GUID

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: (All)
Aliases: TermSet

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SourceTermGroup
The identifier, either in the form of the term group's name or its GUID, where the term set is currently located before being moved.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DestinationTermGroup
The identifier, either in the form of the term group's name or its GUID, indicating the destination where the term set should be relocated.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
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