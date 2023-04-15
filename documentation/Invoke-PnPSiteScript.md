---
Module Name: PnP.PowerShell
title: Invoke-PnPSiteScript
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteScript.html
---
 
# Invoke-PnPSiteScript

## SYNOPSIS
Applies a Site Script to an existing site

## SYNTAX

### Executing a site script registered as such

```powershell
Invoke-PnPSiteScript -Identity <TenantSiteScriptPipeBind> -WebUrl <String> [-Connection <PnPConnection>] 
```

### Executing a site script by passing in the script directly

```powershell
Invoke-PnPSiteScript -Script <String> [-WebUrl <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Applies the Site Design provided through Identity to an existing site. When providing a site design name and multiple site designs exist with the same name, all of them will be invoked and applied. It is also possible to provide a site script directly without having to add it as a site script first. This could be ideal for making quick one time copies of i.e. lists and sites.

When passing in a site script through -Script, you only need to have permissions to the site you are applying the site script to.
When passing in a site script through -Identity, you need to be connected to the SharePoint Online admin site collection and have rights to access it. Using the -WebUrl you can specify the full URL of the site collection you wish to apply the site script to.

The output provided by this cmdlet are the site script actions that have been applied along with for each of them information on if they were successfully applied.

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPSiteScript -Identity "My awesome script" -WebUrl https://contoso.sharepoint.com/sites/mydemosite
```

Retrieves the site script(s) with the provided name and applies it/them to the provided site. Ensure to connect to the SharePoint Online Admin site for this to work.

### EXAMPLE 2
```powershell
$script = Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/mytemplatesite/lists/Sample"
Invoke-PnPSiteScript -Script $script
```

Creates a site script from an existing list on the fly and uses it to create a new list with the same fields, settings and views in the current site

### EXAMPLE 3
```powershell
Get-PnPSiteScript | ? { $_.Title -like "*Test*"} | Invoke-PnPSiteScript -WebUrl https://contoso.sharepoint.com/sites/mydemosite
```

Applies all of the registered site scripts having the word Title in their name to the site at the provided URL

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

### -Identity
The site script instance, name or Id of the site script to apply

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: SITESCRIPTREFERENCE

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Script
The site script to apply, i.e. retrieved using `Get-PnPSiteScriptFromWeb` or `Get-PnPSiteScriptFromList`

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: SITESCRIPTREFERENCE

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WebUrl
The URL of the web to apply the site script to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

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