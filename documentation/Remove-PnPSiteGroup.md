---
Module Name: PnP.PowerShell
title: Remove-PnPSiteGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteGroup.html
---
 
# Remove-PnPSiteGroup

## SYNOPSIS
Removes a group from a web.

## SYNTAX

```powershell
Remove-PnPSiteGroup -Identity <String> [-Site <SitePipeBind>]    
    [-Confirm]
```

## DESCRIPTION

Allows to remove a group from specified site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteGroup -Identity GroupToRemove -Site "https://contoso.sharepoint.com/sites/marketing"
```

This example removes a group named GroupToRemove from the site collection https://contoso.sharepoint.com/sites/marketing.

### EXAMPLE 2
```powershell
Remove-PnPSiteGroup -Identity GroupToRemove
```

This example removes a group named GroupToRemove from the current site collection that has been connected to with Connect-PnPOnline.

## PARAMETERS

### -Identity
Specifies the name of the group to remove.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Specifies the site collection to remove the group from.

```yaml
Type: SitePipeBind
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

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

