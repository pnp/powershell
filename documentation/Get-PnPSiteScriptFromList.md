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

```powershell
Get-PnPSiteScriptFromList -Url <String> [-Connection <PnPConnection>]   [<CommonParameters>]
```

## DESCRIPTION
This command allows a Site Script to be generated off of an existing list on your tenant. Connect to your SharePoint Online Admin site before executing this command.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/teamsite/lists/MyList"
```

Returns the generated Site Script JSON from the list "MyList" at the provided Url

### EXAMPLE 2
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

### -Url
Specifies the URL of the list to generate a Site Script from

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

