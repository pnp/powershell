---
Module Name: PnP.PowerShell
title: Set-PnPTermSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTermSet.html
---
 # Set-PnPTermSet

## SYNOPSIS
Updates an existing TermSet

## SYNTAX

```
Set-PnPTermSet -Identity <TaxonomyTermSetPipeBind> [-TermGroup] <TaxonomyTermGroupPipeBind>
 [-TermStore <TaxonomyTermStorePipeBind>] [-Name <String>] [-Description <String>] [-Owner <String>]
 [-Contact <String>] [-CustomProperties <Hashtable>] [-StakeholderToAdd <String>]
 [-StakeholderToDelete <String>] [-IsAvailableForTagging <Boolean>] [-IsOpenForTermCreation <Boolean>]
 [-UseForSiteNavigation <Boolean>] [-UseForFacetedNavigation <Boolean>] [-SetTargetPageForTerms <String>]
 [-RemoveTargetPageforTerms] [-SetCatalogItemPageForCategories <String>] [-RemoveCatalogItemPageForCategories]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlet allows you to update an existing termset.

## EXAMPLES

### Example 1
```powershell
Set-PnPTermSet -Identity "Departments" -TermGroup "Corporate" -Name "Business Units"
```

This changes the name of a the "Departments" termset to "Business Units"

## PARAMETERS


### -Contact
The contact information

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

### -CustomProperties
Any custom properties to set for the term set. e.g. -CustomProperties @{"propA"="valueA"}

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
The description of the term set

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

### -Identity
The term set to change

```yaml
Type: TaxonomyTermSetPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsAvailableForTagging
Sets the term set as available for tagging.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsOpenForTermCreation
Opens the term set for creation of terms by users.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The new name for the term set

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

### -Owner
Sets the owner of the term set.

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

### -RemoveCatalogItemPageForCategories

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveTargetPageforTerms
{{ Fill RemoveTargetPageforTerms Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetCatalogItemPageForCategories

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

### -SetTargetPageForTerms

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

### -StakeholderToAdd
Adds a new stake holder

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

### -StakeholderToDelete
Removes a stake holder

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

### -TermGroup
The term group to find the term set in.

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
The term store to use. If not specified the default term store is used.

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

### -UseForFacetedNavigation

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseForSiteNavigation

```yaml
Type: Boolean
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

