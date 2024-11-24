---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Move-PnPTerm.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Move-PnPTerm
---

# Move-PnPTerm

## SYNOPSIS

Moves a taxonomy term to another term set or term

## SYNTAX

### Move to term set by Term Id

```
Move-PnPTerm -Identity -TargetTermSet -TargetTermGroup
```

### Move to term set by Term Name

```
Move-PnPTerm -Identity -TargetTermSet -TermSet -TermGroup -TargetTermGroup
```

### Move to term

```
Move-PnPTerm -Identity -TargetTerm -MoveToTerm
```

## ALIASES

This cmdlet has no aliases.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

