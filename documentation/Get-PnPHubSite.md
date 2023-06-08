---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPHubSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPHubSite
---
  
# Get-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve all or a specific hubsite.

## SYNTAX

```powershell
Get-PnPHubSite [[-Identity] <HubSitePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHubSite
```

Returns all hubsite properties

### EXAMPLE 2
```powershell
Get-PnPHubSite -Identity "https://contoso.sharepoint.com/sites/myhubsite"
```

Returns the properties of the specified hubsite by using the hubsite url

### EXAMPLE 3
```powershell
Get-PnPHubSite -Identity "bc07d4b8-1c2f-4184-8cc2-a52dfd6fe0c4"
```

Returns the properties of the specified hubsite by using the hubsite site id

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

### -Identity
Specify hub site url or site id

```yaml
Type: HubSitePipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


