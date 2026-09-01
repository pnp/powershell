---
Module Name: PnP.PowerShell
title: Get-PnPAvailableSiteClassification
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailableSiteClassification.html
---
 
# Get-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All

Returns the available classic Site Classifications for the tenant

## SYNTAX

```powershell
Get-PnPAvailableSiteClassification [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows for retrieving the configuration of the classic site classifications configured within the tenant. For the new Microsoft Purview sensitivity labels, use [Get-PnPAvailableSensitivityLabel](Get-PnPAvailableSensitivityLabel.md) instead.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAvailableSiteClassification
```

Returns the currently set site classifications for the tenant.

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