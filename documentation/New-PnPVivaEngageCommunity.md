---
Module Name: PnP.PowerShell
title: New-PnPVivaEngageCommunity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPVivaEngageCommunity.html
---
 
# New-PnPVivaEngageCommunity

## SYNOPSIS
Creates a Viva engage community

## SYNTAX

```powershell
New-PnPVivaEngageCommunity [[-DisplayName] <string> [-Description] <string> [-Privacy] <CommunityPrivacy>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a Viva engage community

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPVivaEngageCommunity -DisplayName "myPnPDemo1" -Description "Viva engage community description" -Privacy Public
```

This will create a new Viva engage community with specified display name, description and the privacy setting.

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

### -DisplayName
The display name of the Viva engage community.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Description
The description of the Viva engage community.

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
The privacy setting of the Viva engage community.

```yaml
Type: CommunityPrivacy
Parameter Sets: (All)

Required: False
Position: Named
Default value: Private
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
