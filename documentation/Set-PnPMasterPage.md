---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpmasterpage
schema: 2.0.0
title: Set-PnPMasterPage
---

# Set-PnPMasterPage

## SYNOPSIS
Set the masterpage

## SYNTAX

### Server Relative
```powershell
Set-PnPMasterPage [-MasterPageServerRelativeUrl <String>] [-CustomMasterPageServerRelativeUrl <String>]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Site Relative
```powershell
Set-PnPMasterPage [-MasterPageSiteRelativeUrl <String>] [-CustomMasterPageSiteRelativeUrl <String>]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Sets the default master page of the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master
```

Sets the master page based on a server relative URL

### EXAMPLE 2
```powershell
Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master -CustomMasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master
```

Sets the master page and custom master page based on a server relative URL

### EXAMPLE 3
```powershell
Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master
```

Sets the master page based on a site relative URL

### EXAMPLE 4
```powershell
Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master -CustomMasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master
```

Sets the master page and custom master page based on a site relative URL

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

### -CustomMasterPageServerRelativeUrl
Specifies the custom Master page URL based on the server relative URL

```yaml
Type: String
Parameter Sets: Server Relative
Aliases: CustomMasterPageUrl

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CustomMasterPageSiteRelativeUrl
Specifies the custom Master page URL based on the site relative URL

```yaml
Type: String
Parameter Sets: Site Relative

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MasterPageServerRelativeUrl
Specifies the Master page URL based on the server relative URL

```yaml
Type: String
Parameter Sets: Server Relative
Aliases: MasterPageUrl

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MasterPageSiteRelativeUrl
Specifies the Master page URL based on the site relative URL

```yaml
Type: String
Parameter Sets: Site Relative

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)