---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPRetentionLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPRetentionLabel
---
  
# Get-PnPRetentionLabel

## SYNOPSIS
Gets the Microsoft Purview retention labels that are within the tenant

## SYNTAX

```powershell
Get-PnPRetentionLabel [-Identity <Guid>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the available Microsoft Purview retention labels in the currently connected tenant. You can retrieve all the labels or a specific label.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPRetentionLabel
```

Returns all the Microsoft Purview retention labels that exist on the tenant

### EXAMPLE 3
```powershell
Get-PnPRetentionLabel -Identity 58f77809-9738-5080-90f1-gh7afeba2995
```

Returns a specific Microsoft Purview retention label by its id

## PARAMETERS

### -Identity
The Id of the Microsoft Purview retention label to retrieve

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/informationprotectionpolicy-list-labels)