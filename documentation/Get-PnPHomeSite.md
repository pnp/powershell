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

Returns the home site url for your tenant

## SYNTAX

```powershell
Get-PnPHomeSite [IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled <SwitchParameter>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHomeSite
```

Returns the home site url for your tenant

### EXAMPLE 2
```powershell
Get-PnPHomeSite -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled
```

Returns whether Viva Connections landing experience is set to the SharePoint home site.

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

### -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled
When specified, it retrieves whether Viva Connections landing experience is set to the SharePoint home site.

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


