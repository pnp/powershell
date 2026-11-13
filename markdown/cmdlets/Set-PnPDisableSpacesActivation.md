---
Module Name: PnP.PowerShell
title: Set-PnPDisableSpacesActivation
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPDisableSpacesActivation.html
---
 
# Set-PnPDisableSpacesActivation

## SYNOPSIS
Sets if SharePoint Spaces should be disabled.

## SYNTAX

```powershell
Set-PnPDisableSpacesActivation -Disable <SwitchParameter> [-Scope <String>] [-Identity <SitePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet disables or enables SharePoint Spaces for a specific site collection or entire SharePoint tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPDisableSpacesActivation -Disable:$true -Scope Tenant
```

Disables SharePoint Spaces on the entire tenant.

### EXAMPLE 2
```powershell
Set-PnPDisableSpacesActivation -Disable -Scope Site -Identity "https://contoso.sharepoint.com"
```

Disables SharePoint Spaces on https://contoso.sharepoint.com

### EXAMPLE 3
```powershell
Set-PnPDisableSpacesActivation -Disable:$false -Scope Site -Identity "https://contoso.sharepoint.com"
```

Enables SharePoint Spaces on https://contoso.sharepoint.com

## PARAMETERS

### -Disable
Sets if SharePoint Spaces should be enabled or disabled.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: True
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
Specifies the URL of the SharePoint Site on which SharePoint Spaces should be disabled. Must be provided if Scope is set to Site.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Scope
Defines if SharePoint Spaces should be disabled for the entire tenant or for a specific site collection.

```yaml
Type: DisableSpacesScope
Parameter Sets: (All)
Accepted values: Tenant, Site

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

