---
Module Name: PnP.PowerShell
title: Set-PnPVivaEngageCommunity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPVivaEngageCommunity.html
---
 
# Set-PnPVivaEngageCommunity

## SYNOPSIS
Updates the Viva engage community in the tenant.

## SYNTAX

```powershell
Set-PnPVivaEngageCommunity [[-Identity] <string>] [[-DisplayName] <string>] [[-Description] <string>][[-Privacy] <CommunityPrivacy>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Updates the Viva engage community.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPVivaEngageCommunity -Identity "eyJfdHlwZSI6Ikdyb3VwIiwiaWQiOiIyMTI0ODA3MTI3MDQifQ" -DisplayName "New Viva Community"
```

This will update the display name of the Viva Engage community in the tenant with the specified Id.

### EXAMPLE 2
```powershell
Set-PnPVivaEngageCommunity -Identity "eyJfdHlwZSI6Ikdyb3VwIiwiaWQiOiIyMTI0ODA3MTI3MDQifQ" -DisplayName "New Viva Community" -Description "Updated description" -Privacy Private
```

This will update the display name, description and privacy setting of the Viva Engage community in the tenant with the specified Id.

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

### -DisplayName
The updated display name of the Viva engage community.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Description
The updated description of the Viva engage community.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Privacy
The updated privacy setting of the Viva engage community. Available values are `Public` and `Private`.

```yaml
Type: CommunityPrivacy
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
