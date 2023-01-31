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

Enables or disables the public or private Office 365 Content Delivery Network (CDN).

## SYNTAX

```powershell
Set-PnPTenantCdnEnabled [-NoDefaultOrigins] -Enable <Boolean> -CdnType <CdnType> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Enables or disables the public or private Office 365 Content Delivery Network (CDN) for the tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantCdnEnabled -CdnType Public -Enable $true
```

This example sets the Public CDN enabled.

### EXAMPLE 2
```powershell
Set-PnPTenantCdnEnabled -CdnType Private -Enable $false
```

This example disables the Private CDN for the tenant.

### EXAMPLE 3
```powershell
Set-PnPTenantCdnEnabled -CdnType Public -Enable $true -NoDefaultOrigins
```

This example enables the Public CDN for the tenant, but skips the provisioning of the default origins.

## PARAMETERS

### -CdnType
The type of CDN to enable or disable

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
If specified, the default origins for the specified CDN type will not be provisioned. See [Default CDN origins](https://learn.microsoft.com/microsoft-365/enterprise/use-microsoft-365-cdn-with-spo?view=o365-worldwide#default-cdn-origins) for information about the origins that are provisioned by default when you enable the Office 365 CDN, and the potential impact of skipping the setup of default origins.

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

