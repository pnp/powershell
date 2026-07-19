---
Module Name: PnP.PowerShell
title: Get-PnPTenantRestrictedSearchMode
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRestrictedSearchMode.html
---
 
# Get-PnPTenantRestrictedSearchMode

## SYNOPSIS

**Required Permissions**

  *  Global Administrator or SharePoint Administrator 

Returns Restricted Search mode.

## SYNTAX

```powershell
Get-PnPTenantRestrictedSearchMode [-Connection <PnPConnection>] 
```

## DESCRIPTION

Returns Restricted Search mode. Restricted SharePoint Search is disabled by default.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantRestrictedSearchMode
```

Returns Restricted Search mode.

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
