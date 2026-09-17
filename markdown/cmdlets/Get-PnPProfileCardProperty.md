---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPProfileCardProperty.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPProfileCardProperty
---
  
# Get-PnPProfileCardProperty

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of PeopleSettings.Read.All, PeopleSettings.ReadWrite.All
  
Retrieves custom properties added to user profile cards

## SYNTAX

```powershell
Get-PnPProfileCardProperty [-PropertyName <ProfileCardPropertyName>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet may be used to retrieve custom properties added to user profile card.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPProfileCardProperty
```

This will retrieve all custom properties added to user profile card.

### EXAMPLE 2
```powershell
Get-PnPProfileCardProperty -PropertyName "pnppowershell"
```

This will return information about the specified property added to a profile card.

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

### -Identity
Name of the property to be retrieved. If not provided, all properties will be returned.

```yaml
Type: Commands.Enums.ProfileCardPropertyName
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/en-us/graph/add-properties-profilecard)