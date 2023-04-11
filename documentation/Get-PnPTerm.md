---
Module Name: PnP.PowerShell
title: Get-PnPTerm
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTerm.html
---
 
# Get-PnPTerm

## SYNOPSIS
Returns a Term Store Term.

## SYNTAX

### By Term Id
```powershell
Get-PnPTerm
    -Identity <Guid>
    [-TermStore <Guid>]
    [-IncludeChildTerms] [-Connection <PnPConnection>] [-Includes <String[]>] 
```

### By Term Name
```powershell
Get-PnPTerm
    -Identity <Name>
    -TermSet <Guid|Name>
    -TermGroup <Guid|Name>
    [-TermStore <Guid>]
    [-Recursive] 
    [-IncludeChildTerms][-IncludeDeprecated] [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION

Retries a Term Store Term.
## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTerm -TermSet "Departments" -TermGroup "Corporate"
```

Returns all term in the termset "Departments" which is in the group "Corporate" from the site collection termstore

### EXAMPLE 2
```powershell
Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate"
```

Returns the term named "Finance" in the termset "Departments" from the termgroup called "Corporate" from the site collection termstore

### EXAMPLE 3
```powershell
Get-PnPTerm -Identity ab2af486-e097-4b4a-9444-527b251f1f8d -TermSet "Departments" -TermGroup "Corporate"
```

Returns the term named with the given id, from the "Departments" termset in a term group called "Corporate" from the site collection termstore

### EXAMPLE 4
```powershell
Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Recursive
```

Returns the term named "Small Finance", from the "Departments" termset in a term group called "Corporate" from the site collection termstore even if it is a subterm below "Finance"

### EXAMPLE 5
```powershell
$term = Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Includes Labels
$term.Labels
```

Returns all the localized labels for the term named "Small Finance", from the "Departments" termset in a term group called "Corporate"

### EXAMPLE 6
```powershell
Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Recursive -IncludeDeprecated
```

Returns the deprecated term named "Small Finance", from the "Departments" termset in a term group called "Corporate" from the site collection termstore even if it is a subterm below "Finance"

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id or Name of a Term

```yaml
Type: GenericObjectNameIdPipeBind<Microsoft.SharePoint.Client.Taxonomy.Term>
Parameter Sets: All

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeChildTerms
Includes the hierarchy of child terms if available

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recursive
Find the first term recursively matching the label in a term hierarchy.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
Name of the termgroup to check.

```yaml
Type: TermGroupPipeBind
Parameter Sets: By Termset

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermSet
Name of the termset to check.

```yaml
Type: TaxonomyItemPipeBind<TermSet>
Parameter Sets: By Termset

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
Term store to use; if not specified the default term store is used.

```yaml
Type: GenericObjectNameIdPipeBind<TermStore>
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeDeprecated
Includes the deprecated terms if available.

```yaml
Type: SwitchParameter
Parameter Sets: By Term name

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

