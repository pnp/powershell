---
Module Name: PnP.PowerShell
title: Get-PnPTenantRestrictedSearchAllowedList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRestrictedSearchAllowedList.html
---
 
# Get-PnPTenantRestrictedSearchAllowedList

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site and Copilot for M365 license

Retrieves existing list of URLs in the allowed list.

## SYNTAX

```powershell
Get-PnPTenantRestrictedSearchAllowedList [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command will return all the existing list of URLs in the allowed list

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantRestrictedSearchAllowedList
```

Retrieves existing list of URLs in the allowed list

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