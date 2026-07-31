---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Convert-PnPSiteTemplateToMarkdown.html
external help file: PnP.PowerShell.dll-Help.xml
title: Convert-PnPSiteTemplateToMarkdown
---
  
# Convert-PnPSiteTemplateToMarkdown

## SYNOPSIS
Converts an existing PnP Site Template to a markdown report

## SYNTAX

```powershell
Convert-PnPSiteTemplateToMarkdown -TemplatePath <String> [-Out <String>] [-Force <SwitchParameter>]
```

## DESCRIPTION
Converts an existing PnP Site Template to markdown report. Notice that this cmdlet is work in work progress, and the completeness of the report will increase in the future.

## EXAMPLES

### EXAMPLE 1
```powershell
Convert-PnPSiteTemplateToMarkdown -TemplatePath ./mytemplate.xml
```

This will convert the site template to a markdown file and outputs the result to the console.

### EXAMPLE 2
```powershell
Convert-PnPSiteTemplateToMarkdown -TemplatePath ./mytemplate.xml -Out ./myreport.md
```

This will convert the site template to a markdown file and writes the result to the specified myreport.md file.

## PARAMETERS

### -TemplatePath
The path to an existing PnP Site Template

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
The output file name to write the report to in markdown format.

```yaml
Type: String
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


