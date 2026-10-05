---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPDisableSpacesActivation.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPDisableSpacesActivation
---
  
# Get-PnPDisableSpacesActivation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves if SharePoint Spaces is disabled on the entire tenant

## SYNTAX

```powershell
Get-PnPDisableSpacesActivation [-Connection <PnPConnection>] 
```

## DESCRIPTION

Retrieves if SharePoint Spaces is disabled on the entire tenant. At this point there is no API yet for retrieving the setting for a specific site, although you can set it for a specific site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPDisableSpacesActivation
```

Returns if SharePoint Spaces is disabled on the tenant

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


