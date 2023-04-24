---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPTaxonomy.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPTaxonomy
---
  
# Export-PnPTaxonomy

## SYNOPSIS
Exports a taxonomy to either the output or to a file.

## SYNTAX

```powershell
Export-PnPTaxonomy [-TermSetId <Guid>] [-IncludeID] [-Path <String>] [-TermStoreName <String>] [-Force]
 [-Delimiter <String>] [-Lcid <Int32>] [-Encoding <Encoding>] [-Connection <PnPConnection>] 
  
```

## DESCRIPTION

Allows to export a taxonomy to either the output or to a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPTaxonomy
```

Exports the full taxonomy to the standard output

### EXAMPLE 2
```powershell
Export-PnPTaxonomy -Path c:\output.txt
```

Exports the full taxonomy the file output.txt

### EXAMPLE 3
```powershell
Export-PnPTaxonomy -Path c:\output.txt -TermSetId f6f43025-7242-4f7a-b739-41fa32847254
```

Exports the term set with the specified id

### EXAMPLE 4
```powershell
Export-PnPTaxonomy -Path c:\output.txt -TermSetId f6f43025-7242-4f7a-b739-41fa32847254 -Lcid 1044
```

Exports the term set with the specified id using Norwegian labels

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

### -Encoding
Defaults to Unicode

```yaml
Type: Encoding
Parameter Sets: (All)
Accepted values: Unicode, ASCII, BigEndianUnicode, UTF32, UTF7, UTF8

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeID
If specified will include the ids of the taxonomy items in the output. Format: &lt;label&gt;;#&lt;guid&gt;

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
Specify the language code for the exported terms

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
File to export the data to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermSetId
If specified, will export the specified termset only

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermStoreName
Term store to export; if not specified the default term store is used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


