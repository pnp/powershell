---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPPageMapping.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPPageMapping
---
  
# Export-PnPPageMapping

## SYNOPSIS
Get's the built-in mapping files or a custom mapping file for your publishing portal page layouts. These mapping files are used to tailor the page transformation experience.

## SYNTAX 

```powershell
Export-PnPPageMapping [-BuiltInWebPartMapping [<SwitchParameter>]]
                                [-BuiltInPageLayoutMapping [<SwitchParameter>]]
                                [-CustomPageLayoutMapping [<SwitchParameter>]]
                                [-PublishingPage <PagePipeBind>]
                                [-AnalyzeOOBPageLayouts [<SwitchParameter>]]
                                [-Folder <String>]
                                [-Overwrite [<SwitchParameter>]]
                                [-Logging [<SwitchParameter>]]
                                
                                [-Connection <PnPConnection>]
```

## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPPageMapping -BuiltInPageLayoutMapping -CustomPageLayoutMapping -Folder c:\\temp -Overwrite
```

Exports the built in page layout mapping and analyzes the current site's page layouts and exports these to files in folder c:\temp

### EXAMPLE 2
```powershell
Export-PnPPageMapping -CustomPageLayoutMapping -PublishingPage mypage.aspx -Folder c:\\temp -Overwrite
```

Analyzes the page layout of page mypage.aspx and exports this to a file in folder c:\temp

### EXAMPLE 3
```powershell
Export-PnPPageMapping -BuiltInWebPartMapping -Folder c:\\temp -Overwrite
```

Exports the built in webpart mapping to a file in folder c:\temp. Use this a starting basis if you want to tailer the web part mapping behavior.

## PARAMETERS

### -AnalyzeOOBPageLayouts
Set this flag if you also want to analyze the OOB page layouts...typically these are covered via the default mapping, but if you've updated these page layouts you might want to analyze them again

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -BuiltInPageLayoutMapping
Exports the builtin pagelayout mapping file (only needed for publishing page transformation)

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -BuiltInWebPartMapping
Exports the builtin web part mapping file

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -CustomPageLayoutMapping
Analyzes the pagelayouts in the current publishing portal and exports them as a pagelayout mapping file

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Folder
The folder to created the mapping file(s) in

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Logging
Outputs analyzer logging to the console

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Overwrite
Overwrites existing mapping files

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -PublishingPage
The name of the publishing page to export a page layout mapping file for

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: True
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


