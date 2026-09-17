---
Module Name: PnP.PowerShell
title: Import-PnPTaxonomy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPTaxonomy.html
---
 
# Import-PnPTaxonomy

## SYNOPSIS
Imports a taxonomy from either a string array or a file

## SYNTAX

### Direct
```powershell
Import-PnPTaxonomy [-Terms <String[]>] [-Lcid <Int32>] [-TermStoreName <String>] [-Delimiter <String>]
 [-SynchronizeDeletions] [-Connection <PnPConnection>]   
```

### File
```powershell
Import-PnPTaxonomy -Path <String> [-Lcid <Int32>] [-TermStoreName <String>] [-Delimiter <String>]
 [-SynchronizeDeletions] [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to import taxonomy terms from array or file.

## EXAMPLES

### EXAMPLE 1
```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|Stockholm'
```

Creates a new termgroup, 'Company', a termset 'Locations' and a term 'Stockholm'

### EXAMPLE 2
```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|"Stockholm,Central"'
```

Creates a new termgroup, 'Company', a termset 'Locations', a term 'Stockholm,Central'

### EXAMPLE 3
```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|Stockholm|Central','Company|Locations|Stockholm|North'
```

Creates a new termgroup, 'Company', a termset 'Locations', a term 'Stockholm' and two subterms: 'Central', and 'North'

### EXAMPLE 4
```powershell
Import-PnPTaxonomy -Path ./mytaxonomyterms.txt
```

Imports the taxonomy from the file specified. Each line has to be in the format TERMGROUP|TERMSET|TERM. See example 2 for examples of the format.

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

### -Delimiter
The path delimiter to be used, by default this is '|'

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies a file containing terms per line, in the format as required by the Terms parameter.

```yaml
Type: String
Parameter Sets: File

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SynchronizeDeletions
If specified, terms that exist in the termset, but are not in the imported data, will be removed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermStoreName
Term store to import to; if not specified the default term store is used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Terms
An array of strings describing termgroup, termset, term, subterms using a default delimiter of '|'.

```yaml
Type: String[]
Parameter Sets: Direct

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

