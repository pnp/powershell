---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantPronounsSettings.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPTenantPronounsSettings
---
  
# Get-PnPTenantPronounsSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of PeopleSettings.Read.All, PeopleSettings.ReadWrite.All
  
Retrieve the current setting for the availability of using pronouns in the organization

## SYNTAX

```powershell
Get-PnPTenantPronounsSettings [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to retrieve tenant wide pronounsSettings properties.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantPronounsSettings
```

Retrieves the tenant-wide pronouns settings

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/peopleadminsettings-list-pronouns)