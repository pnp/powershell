---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/invoke-pnpsitedesign
schema: 2.0.0
title: Invoke-PnPSiteDesign
---

# Invoke-PnPSiteDesign

## SYNOPSIS
Apply a Site Design to an existing site. * Requires Tenant Administration Rights *

## SYNTAX

```powershell
Invoke-PnPSiteDesign [-Identity] <TenantSiteDesignPipeBind> [-WebUrl <String>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Applies the specified site design to the current site.

### EXAMPLE 2
```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -WebUrl https://contoso.sharepoint.com/sites/mydemosite
```

Applies the specified site design to the specified site.

### EXAMPLE 3
```powershell
Get-PnPSiteDesign | ?{$_.Title -eq "Demo"} | Invoke-PnPSiteDesign
```

Applies the specified site design to the specified site.

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

### -Identity
The Site Design Id or an actual Site Design object to apply

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### -WebUrl
The URL of the web to apply the site design to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)