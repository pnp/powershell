---
Module Name: PnP.PowerShell
title: Grant-PnPHubSiteRights
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPHubSiteRights.html
---
 
# Grant-PnPHubSiteRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Grant additional permissions to the permissions already in place to associate sites to Hub Sites for one or more specific users

## SYNTAX

```powershell
Grant-PnPHubSiteRights [-Identity] <HubSitePipeBind> -Principals <String[]> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to add additional permissions to existing once to associate sites to Hub Sites for specified users.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPHubSiteRights -Identity "https://contoso.sharepoint.com/sites/hubsite" -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

This example shows how to grant rights to myuser and myotheruser to associate their sites with the provided Hub Site

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
Specify hub site url

```yaml
Type: HubSitePipeBind
Parameter Sets: (All)
Aliases: HubSite

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Principals
Specify user(s) login name i.e user@company.com


```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

