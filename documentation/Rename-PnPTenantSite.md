---
Module Name: PnP.PowerShell
title: Rename-PnPTenantSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Rename-PnPTenantSite.html
---
 
# Rename-PnPTenantSite

## SYNOPSIS
Starts a rename of a site on a SharePoint Online site.

## SYNTAX

```powershell
Rename-PnPTenantSite [[-Identity] <SPOSitePipeBind>] [[-NewSiteUrl] <String>] [[-NewSiteTitle] <string>]
[[-SuppressMarketplaceAppCheck] [<SwitchParameter>]] [[-SuppressWorkflow2013Check] [<SwitchParameter>]] [[-SuppressBcsCheck] [<SwitchParameter>]] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet starts a rename of a site on a SharePoint Online site. You can change the URL, and optionally the site title along with changing the URL.

This will not work between Multi-geo environments.

## EXAMPLES

### EXAMPLE 1
```powershell
$currentSiteUrl = "https://<tenant>.sharepoint.com/site/samplesite"
$updatedSiteUrl = "https://<tenant>.sharepoint.com/site/renamed"
Rename-PnPTenantSite -Identity $currentSiteUrl -NewSiteUrl $updatedSiteUrl
```

Starts the rename of the SharePoint Online site with name "samplesite" to "renamed" without modifying the title.

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
Specifies the full URL of the SharePoint Online site collection that needs to be renamed.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -NewSiteUrl
Specifies the full URL of the SharePoint Online site collection to which it needs to be renamed.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewSiteTitle
Specifies the new title of the SharePoint Site.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressMarketplaceAppCheck
Suppress checking compatibility of marketplace SharePoint Add-ins deployed to the associated site.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressWorkflow2013Check
Suppress checking compatibility of SharePoint 2013 Workflows deployed to the associated site.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressBcsCheck
Suppress checking compatibility of BCS connections deployed to the associated site.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Wait
Wait till the renaming of the new site collection is successful. If not specified, a job will be created which you can use to check for its status.

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
