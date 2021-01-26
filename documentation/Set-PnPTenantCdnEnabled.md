---
Module Name: PnP.PowerShell
title: Set-PnPTenantCdnEnabled
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantCdnEnabled.html
---
 
# Set-PnPTenantCdnEnabled

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Enables or disabled the public or private Office 365 Content Delivery Network (CDN).

## SYNTAX

```powershell
Set-PnPTenantCdnEnabled [-NoDefaultOrigins] -Enable <Boolean> -CdnType <CdnType> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Enables or disabled the public or private Office 365 Content Delivery Network (CDN).

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantCdnEnabled -CdnType Public -Enable $true
```

This example sets the Public CDN enabled.

## PARAMETERS

### -CdnType
The type of cdn to enable or disable

```yaml
Type: CdnType
Parameter Sets: (All)
Accepted values: Public, Private, Both

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

### -Enable
Specify to enable or disable

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoDefaultOrigins
{{ Fill NoDefaultOrigins Description }}

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

