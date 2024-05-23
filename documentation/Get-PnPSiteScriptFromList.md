---
Module Name: PnP.PowerShell
title: Get-PnPSiteScriptFromList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScriptFromList.html
---
 
# Get-PnPSiteScriptFromList

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a Site Script from an existing list

## SYNTAX

### By List (Default)

```powershell
Get-PnPSiteScriptFromList -List <ListPipeBind> [-Connection <PnPConnection>] [-Verbose]
```

### By Url

```powershell
Get-PnPSiteScriptFromList -Url <String> [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This command allows a Site Script to be generated off of an existing list on your tenant. The script will return the JSON syntax with the definition of the list, including fields, views, content types, and some of the list settings. The script can then be used with [Add-PnPSiteScript](Add-PnPSiteScript.md) and [Add-PnPListDesign](Add-PnPListDesign.md) to allow lists with the same configuration as the original list to be created by end users.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteScriptFromList -List "MyList"
```

Returns the generated Site Script JSON from the list "MyList" in the currently connected to site

### EXAMPLE 2
```powershell
Get-PnPList -Identity "MyList" | Get-PnPSiteScriptFromList | Add-PnPSiteScript -Title "MyListScript" | Add-PnPListDesign -Title "MyListDesign"
```

Returns the generated Site Script JSON from the list "MyList" in the currently connected to site and registers it as a new Site Script with the title "MyListScript" and uses that Site Script to register a new List Design with the title "MyListDesign"

### EXAMPLE 3
```powershell
Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/teamsite/lists/MyList"
```

Returns the generated Site Script JSON from the list "MyList" at the provided Url

### EXAMPLE 4
```powershell
Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/teamsite/Shared Documents"
```

Returns the generated Site Script JSON from the default document library at the provided Url

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

### -List
Specifies an instance, Id or, title of the list to generate a Site Script from

```yaml
Type: ListPipeBind
Parameter Sets: By List

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Url
Specifies the full URL of the list to generate a Site Script from. I.e. https://contoso.sharepoint.com/sites/teamsite/lists/MyList

```yaml
Type: String
Parameter Sets: By Url

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
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