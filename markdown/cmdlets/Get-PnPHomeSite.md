---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPHomeSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPHomeSite
---
  
# Get-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the SharePoint home sites for your tenant

## SYNTAX

### Basic (Default)
```powershell
Get-PnPHomeSite [-IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled <SwitchParameter>] [-Connection <PnPConnection>]
```

### Detailed
```powershell
Get-PnPHomeSite -Detailed <SwitchParameter> [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet will return the SharePoint Home sites for your tenant. Depending on which parameters you provide, you will get returned either the default first Home Site URL or details on all the Home Sites that have been configured for your tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHomeSite
```

Returns the URL of the first home site for your tenant

### EXAMPLE 2
```powershell
Get-PnPHomeSite -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled
```

Returns whether Viva Connections landing experience is set to the SharePoint home site.

### EXAMPLE 3
```powershell
Get-PnPHomeSite -Detailed
```

Returns detailed information on all the home sites that have been configured for your tenant

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

### -Detailed
When provided, it returns detailed information on all the home sites configured on your tenant 

```yaml
Type: SwitchParameter
Parameter Sets: Detailed
Required: True
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled
When provided, it retrieves whether Viva Connections landing experience is set to the SharePoint home site.

```yaml
Type: SwitchParameter
Parameter Sets: Basic
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)