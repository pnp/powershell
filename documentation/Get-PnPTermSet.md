---
Module Name: PnP.PowerShell
title: Get-PnPTermSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTermSet.html
---
 # Get-PnPTermSet

## SYNOPSIS
Returns a taxonomy term set


## SYNTAX

```powershell
Get-PnPTermSet [-Identity <TaxonomyTermSetPipeBind>] [-TermGroup] <TaxonomyTermGroupPipeBind>
 [-TermStore <TaxonomyTermStorePipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlet returns a termset from the taxonomy store.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTermSet -TermGroup "Corporate"
```

Returns all termsets in the group "Corporate" from the site collection termstore

### EXAMPLE 2
```powershell
Get-PnPTermSet -Identity "Departments" -TermGroup "Corporate"
```

Returns the termset named "Departments" from the termgroup called "Corporate" from the site collection termstore

### EXAMPLE 3
```powershell
Get-PnPTermSet -Identity ab2af486-e097-4b4a-9444-527b251f1f8d -TermGroup "Corporate
```

Returns the termset with the given id from the termgroup called "Corporate" from the site collection termstore

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id or Name of a termset

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
Name of the term group to check.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

