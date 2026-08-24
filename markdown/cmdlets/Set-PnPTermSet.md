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
Updates an existing term set.

## SYNTAX

```powershell
Set-PnPTermSet -Identity <TaxonomyTermSetPipeBind> [-TermGroup] <TaxonomyTermGroupPipeBind>
 [-TermStore <TaxonomyTermStorePipeBind>] [-Name <String>] [-Description <String>] [-Owner <String>]
 [-Contact <String>] [-CustomProperties <Hashtable>] [-StakeholderToAdd <String>]
 [-StakeholderToDelete <String>] [-IsAvailableForTagging <Boolean>] [-IsOpenForTermCreation <Boolean>]
 [-UseForSiteNavigation <Boolean>] [-UseForFacetedNavigation <Boolean>] [-SetTargetPageForTerms <String>]
 [-RemoveTargetPageForTerms] [-SetCatalogItemPageForCategories <String>] [-RemoveCatalogItemPageForCategories]
 [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet allows you to update an existing term set.

## EXAMPLES

### Example 1
```powershell
Set-PnPTermSet -Identity "Departments" -TermGroup "Corporate" -Name "Business Units"
```

This example changes the name of a the "Departments" term set to "Business Units".

### Example 2
```powershell
Set-PnPTermSet -Identity "Departments" -TermGroup "Corporate" -UseForSiteNavigation $true
```

This example allows the terms in the term set "Departments" to be used for site navigation links.

### Example 3
```powershell
Set-PnPTermSet -Identity "Departments" -TermGroup "Corporate" -IsAvailableForTagging $false
```

This example makes the terms in the term set "Departments" unavailable to end users and content editors.

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

### -Contact
The contact information.

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
The description of the term set.

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
The term set to change.

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
The new name for the term set.

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
Removes catalog item page settings for the term set.

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

### -RemoveTargetPageForTerms
Removes target page settings for the term set.

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
Specifies the page to load when you navigate to a catalog item under a category in this term set. 

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
Specifies page to load when the user navigates to the friendly-URL for a term in this term set.

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
Adds a new stake holder.

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
Removes a stake holder.

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
Specifies whether users can use refiners based on managed metadata from the search index to quickly browse to specific content.

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
Specifies whether the terms in this term set can be used for site navigation links with friendly URLs and dynamic content.

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

