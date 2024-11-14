---
Module Name: PnP.PowerShell
title: Remove-PnPTerm
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTerm.html
---
 # Remove-PnPTerm

## SYNOPSIS
Removes a taxonomy term from the term store.

## SYNTAX

### By Term Id
```
Remove-PnPTerm -Identity <Guid> [-TermStore <TaxonomyTermStorePipeBind>]
  [-Confirm] 
```

### By Term Name
```
Remove-PnPTerm -Identity <String> -TermSet <TaxonomyTermSetPipeBind>
 -TermGroup <TaxonomyTermGroupPipeBind> [-TermStore <TaxonomyTermStorePipeBind>]
 [-Connection <PnPConnection>] [-Confirm] 
```

## DESCRIPTION
This cmdlet removes a term from the term store.

## EXAMPLES

### Example 1
```powershell
Remove-PnPTerm -Identity 3d9e60e8-d89c-4cd4-af61-a010cf93b380
```

Removes a term by id.

### Example 2
```powershell
Remove-PnPTerm -Identity "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```

Removes a term by name.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specifies which term to remove.

```yaml
Type: TaxonomyTermPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermGroup
Specifies which termgroup to find the termset in.

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

### -TermSet
Specifies which termset to find the term in if the identity of the term is specified as a string.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

