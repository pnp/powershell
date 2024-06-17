---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPListToSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPListToSiteTemplate
---
  
# Export-PnPListToSiteTemplate

## SYNOPSIS
Exports one or more lists to provisioning template

## SYNTAX

```powershell
Export-PnPListToSiteTemplate -List <System.Collections.Generic.List`1[System.String]> [[-Out] <String>]
 [[-Schema] <XMLPnPSchemaVersion>] [-Force] [-OutputInstance] 
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to export one or more lists to provisioning template.

## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPListToSiteTemplate -Out template.xml -List "Documents"
```

Extracts a list to a new provisioning template including the list specified by title or ID.

### EXAMPLE 2
```powershell
Export-PnPListToSiteTemplate -Out template.pnp -List "Documents","Events"
```

Extracts a list to a new provisioning template Office Open XML file, including the lists specified by title or ID.

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

### -List
Specify the lists to extract, either providing their ID or their Title.

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputInstance
Returns the template as an in-memory object, which is an instance of the SiteTemplate type of the PnP Core Component. It cannot be used together with the -Out parameter.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema
The schema of the output to use, defaults to the latest schema

```yaml
Type: XMLPnPSchemaVersion
Parameter Sets: (All)
Accepted values: LATEST, V201503, V201505, V201508, V201512, V201605, V201705, V201801, V201805, V201807, V201903, V201909, V202002, V202103, V202209

Required: False
Position: 1
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


