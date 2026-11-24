---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTermToTerm.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTermToTerm
---
  
# Add-PnPTermToTerm

## SYNOPSIS
Adds a new term to an existing term.

## SYNTAX

```powershell
Add-PnPTermToTerm -ParentTermId <Guid> -Name <String> [-Id <Guid>] [-Lcid <Int32>]
  [-LocalCustomProperties <Hashtable>]
 [-TermStore <Guid>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet adds a new taxonomy term as a child term to an existing term.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTermToTerm -ParentTermId 2d1f298b-804a-4a05-96dc-29b667adec62 -Name SubTerm -CustomProperties @{"Department"="Marketing"}
```

Creates a new taxonomy child term named "SubTerm" in the specified term by id 2d1f298b-804a-4a05-96dc-29b667adec62.

### EXAMPLE 2
```powershell
$parentTerm = Get-PnPTerm -Name Marketing -TermSet Departments -TermGroup Corporate
Add-PnPTermToTerm -ParentTermId $parentTerm.Id -Name "Conference Team"
```

Creates a new taxonomy child term named "Conference Team" in the specified term called Marketing which is located in the Departments term set.

## PARAMETERS

### -ParentTermId
The Id of the parent term.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```


### -Name
The name of the term.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```


### -Description
Descriptive text to help users understand the intended use of this term.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The Id to use for the term; if not specified, or the empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
The locale id to use for the term. Defaults to the current locale id.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CustomProperties
Custom Properties

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LocalCustomProperties
Custom Properties

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


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


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


