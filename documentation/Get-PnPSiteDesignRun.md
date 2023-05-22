---
Module Name: PnP.PowerShell
title: Get-PnPSiteDesignRun
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteDesignRun.html
---
 
# Get-PnPSiteDesignRun

## SYNOPSIS
Retrieves a list of site designs applied to a specified site collection. If the WebUrl parameter is not specified we show the list of designs applied to the current site. The returned output includes the ID of the scheduled job, the web and site IDs, and the site design ID, version, and title.

## SYNTAX

```powershell
Get-PnPSiteDesignRun [-SiteDesignId <Guid>] [-WebUrl <String>] 
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to retrieve a list of site designs applied to a specified site collection. By default the command will retrieve list of designs applied to the current site but it is also possible to get this information from a different site collection using `WebUrl` option.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteDesignRun
```

This example returns a list of the site designs applied to the current site. Providing a specific site design ID will return the details for just that applied site design.

### EXAMPLE 2
```powershell
Get-PnPSiteDesignRun -WebUrl "https://mytenant.sharepoint.com/sites/project"
```

This example returns a list of the site designs applied to the specified site. Providing a specific site design ID will return the details for just that applied site design.

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

### -SiteDesignId
The ID of the site design to apply.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebUrl
The URL of the site collection where the site design will be applied. If not specified the design will be applied to the site you connected to with Connect-PnPOnline.

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