---
Module Name: PnP.PowerShell
title: Revoke-PnPHubSiteRights
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPHubSiteRights.html
---
 
# Revoke-PnPHubSiteRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Revoke permissions to the permissions already in place to associate sites to Hub Sites for one or more specific users

## SYNTAX

```powershell
Revoke-PnPHubSiteRights [-Identity] <HubSitePipeBind> -Principals <String[]> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to revoke permissions from existing once to associate sites to Hub Sites for specified users.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPHubSiteRights -Identity "https://contoso.sharepoint.com/sites/hubsite" -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

This example shows how to revoke the rights of myuser and myotheruser to associate their sites with the provided Hub Site

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
Specify hub site url to revoke rights from

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
Specify user(s) login name i.e user@company.com to revoke rights for

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

