---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Move-PnPTermSet.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Move-PnPTermSet
---

# Move-PnPTermSet

## SYNOPSIS

Moves taxonomy term set from one term group to another

## SYNTAX

### By Term Id

```
Move-PnPTermSet -Identity <Guid> -TermGroup <Guid> -TargetTermGroup <Guid>
 [-TermStore <TaxonomyTermStorePipeBind>]
```

### By Term Name

```
Move-PnPTermSet -Identity <String> -TermGroup <String> -TargetTermGroup <String>
 [-TermStore <TaxonomyTermStorePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet moves taxonomy term set from one term group to another.

## EXAMPLES

### Example 1

```powershell
Move-PnPTermSet -Identity 81e0a4b8-701d-459c-ad61-a1c7a81810ff -TermGroup 17e16b98-a8c2-4db6-a860-5c42dbc818f4  -TargetTermGroup cf33d1cd-42d8-431c-9e43-3d8dab9ea8fd
```

Moves term set by id.

### Example 2

```powershell
Move-PnPTermSet -Identity "OperationLevel-1 Test" -TermGroup "FromPowerAutomate" -TargetTermGroup "TargetTermGroup"
```

Moves term set by name.

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

