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
This command starts a rename of a site on a SharePoint Online site. You can change the URL, and optionally the site title along with changing the URL.

This will not work for Multi-geo environments.

## SYNTAX

```powershell
Rename-PnPTenantSite [[-Identity] <SPOSitePipeBind>] [[-NewSiteUrl] <String>] [[-NewSiteTitle] <string>]
[[-SuppressMarketplaceAppCheck] [<SwitchParameter>]] [[-SuppressWorkflow2013Check] [<SwitchParameter>]] [-Connection <PnPConnection>]
[<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
$url="https://<tenant>.sharepoint.com/site/samplesite"
$NewSiteUrl="https://<tenant>.sharepoint.com/site/renamed"
Rename-PnPTenantSite -Identity $url -NewSiteUrl $NewSiteUrl
```

Starts the rename of the SPO site with name "samplesite" to "renamed" without modifying the title.

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
Specifies the URL of the SharePoint Site on which SharePoint Spaces should be disabled. Must be provided if Scope is set to Tenant.

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
Specifies the new URL of the SharePoint Site that you want to set

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -NewSiteTitle
Specifies the new title of of the SharePoint Site.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
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
Accept pipeline input: True
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
Accept pipeline input: True
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
Wait till the renaming of the new site collection is successfull. If not specified, a job will be created for SPO and you can check it a few seconds or minutes later.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

