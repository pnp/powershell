---
Module Name: PnP.PowerShell
title: Remove-PnPVivaEngageCommunity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPVivaEngageCommunity.html
---
 
# Remove-PnPVivaEngageCommunity

## SYNOPSIS
Deletes the Viva engage community in the tenant.

## SYNTAX

```powershell
Remove-PnPVivaEngageCommunity [[-Identity] <string>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Deletes Viva engage community.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPVivaEngageCommunity -Identity "eyJfdHlwZSI6Ikdyb3VwIiwiaWQiOiIyMTI0ODA3MTI3MDQifQ"
```

This will delete the Viva Engage community in the tenant with the specified Id.

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
The Id of the Viva engage community.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
