---
Module Name: PnP.PowerShell
title: New-PnPTerm
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTerm.html
---
 
# New-PnPTerm

## SYNOPSIS
Creates a taxonomy term

## SYNTAX

```powershell
New-PnPTerm -Name <String> [-Id <Guid>] [-Lcid <Int32>] [-TermSet] <TaxonomyTermSetPipeBind>
 -TermGroup <TaxonomyTermGroupPipeBind> [-Description <String>] [-CustomProperties <Hashtable>]
 [-LocalCustomProperties <Hashtable>] [-TermStore <TaxonomyTermStorePipeBind>]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlet adds a new taxonomy term to a given termset.

## EXAMPLES

### Example 1
```powershell
New-PnPTerm -TermSet "Departments" -TermGroup "Corporate" -Name "Finance"
```

Creates a new taxonomy term named "Finance" in the termset Departments which is located in the "Corporate" termgroup

### Example 2
```powershell
New-PnPTerm -TermSet "Departments" -TermGroup "Corporate" -Name "Finance" -CustomProperties @{"IsCorporate"="True"}
```

Creates a new taxonomy term named "Finance" in the termset Departments which is located in the "Corporate" termgroup and sets a custom property on the termset.

## PARAMETERS

### -CustomProperties
Sets custom properties. 

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Descriptive text to help users understand the intended use of this term.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The Id to use for the term; if not specified, or provided with an empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
The locale id to use for the term. Defaults to the default language of the termstore.
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

### -LocalCustomProperties
Sets local custom properties

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the term.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermGroup
The termgroup to create the term in.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermSet
The termset to add the term to.

```yaml
Type: TaxonomyTermSetPipeBind
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
Aliases: TermStoreName

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

