---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnpProfileCardProperty.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnpProfileCardProperty
---
  
# New-PnpProfileCardProperty

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : PeopleSettings.ReadWrite.All

Adds a property to user profile card

## SYNTAX

```powershell
New-PnpProfileCardProperty -PropertyName <ProfileCardPropertyName> -DisplayName <String> [-Localizations <Hashtable>] [-Verbose] [-Connection <PnPConnection>] 
```
## DESCRIPTION

This cmdlet may be used to add a property to user profile card. Please note that it may take up to 24 hours to reflect the changes.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnpProfileCardProperty -PropertyName CustomAttribute1 -DisplayName "Cost Centre"
```

This cmdlet will add a property with a display name to user profile card.

### EXAMPLE 2
```powershell
$localizations = @{ "pl" = "Centrum kosztów"; "de" = "Kostenstelle" }
New-PnpProfileCardProperty -PropertyName CustomAttribute1 -DisplayName "Cost Centre" -Localizations $localizations
```

This cmdlet will add a property with a display name and specified localizations to user profile card.

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

### -PropertyName
Name of a property to be added

```yaml
Type: Commands.Enums.ProfileCardPropertyName
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name of the property.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Localizations
List of display name localizations

```yaml
Type: Hashtable
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