---
Module Name: PnP.PowerShell
title: Set-PnPHideDefaultThemes
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPHideDefaultThemes.html
---
 
# Set-PnPHideDefaultThemes

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Defines if the default out of the box themes should be visible to users or not.

## SYNTAX

```powershell
Set-PnPHideDefaultThemes -HideDefaultThemes <Boolean> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Use this cmdlet to hide or show the default themes to users as an option to change the look of their site.

You must be a SharePoint Online Administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPHideDefaultThemes -HideDefaultThemes $true
```

The out of the box themes will be hidden.

### EXAMPLE 2
```powershell
Set-PnPHideDefaultThemes -HideDefaultThemes $false
```

The out of the box themes will be shown.

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

### -HideDefaultThemes
Defines if the default themes should be visible or hidden.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

