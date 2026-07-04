---
title: Remove-PnPGeoAdministrator
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPGeoAdministrator.html
Module Name: PnP.PowerShell
---
 
# Remove-PnPGeoAdministrator

## SYNOPSIS
Removes a SharePoint Online geo administrator.

## SYNTAX

```powershell
Remove-PnPGeoAdministrator [-UserPrincipalName] <String> [-Connection <PnPConnection>]

Remove-PnPGeoAdministrator [-GroupAlias] <String> [-Connection <PnPConnection>]

Remove-PnPGeoAdministrator [-ObjectId] <Guid> [-Connection <PnPConnection>]
```

## DESCRIPTION
Removes a user or group as a SharePoint Online geo administrator.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPGeoAdministrator -UserPrincipalName user@contoso.onmicrosoft.com
```

Removes the specified user as a geo administrator.

### EXAMPLE 2

```powershell
Remove-PnPGeoAdministrator -GroupAlias spo-geo-admins
```

Removes the specified group as a geo administrator.

### EXAMPLE 3

```powershell
Remove-PnPGeoAdministrator -ObjectId 11111111-1111-1111-1111-111111111111
```

Removes the geo administrator with the specified object ID.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupAlias
Specifies the alias of the group to remove as a geo administrator.

```yaml
Type: String
Parameter Sets: Group

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectId
Specifies the object ID of the user or group to remove as a geo administrator.

```yaml
Type: Guid
Parameter Sets: ObjectId

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
Specifies the user principal name of the user to remove as a geo administrator.

```yaml
Type: String
Parameter Sets: User

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### None

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

