---
Module Name: PnP.PowerShell
title: Request-PnPReIndexWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPReIndexWeb.html
---
 
# Request-PnPReIndexWeb

## SYNOPSIS
Marks the web for full indexing during the next incremental crawl.

## SYNTAX

```powershell
Request-PnPReIndexWeb [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet marks the web for full indexing during the next incremental crawl.

## EXAMPLES

### EXAMPLE 1
```powershell
Request-PnPReIndexWeb
```

This example requests that the site be reindexed during the next crawl.

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

