---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPGeoAdministrator.html
tags: Available in the current Nightly Release only.
title: Add-PnPGeoAdministrator
---
 
# Add-PnPGeoAdministrator

## SYNOPSIS
Adds a SharePoint Online multi-geo administrator.

## SYNTAX

### User
```powershell
Add-PnPGeoAdministrator [-UserPrincipalName] <String> [-Connection <PnPConnection>]
```

### Group
```powershell
Add-PnPGeoAdministrator [-GroupAlias] <String> [-Connection <PnPConnection>]
```

### ObjectId
```powershell
Add-PnPGeoAdministrator [-ObjectId] <Guid> [-Connection <PnPConnection>]
```

## DESCRIPTION
Adds a user or group as a SharePoint Online multi-geo administrator.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPGeoAdministrator -UserPrincipalName user@contoso.com
```

Adds the user as a SharePoint Online multi-geo administrator.

### EXAMPLE 2

```powershell
Add-PnPGeoAdministrator -GroupAlias "Geo Administrators"
```

Adds the group as a SharePoint Online multi-geo administrator.

### EXAMPLE 3

```powershell
Add-PnPGeoAdministrator -ObjectId 00000000-0000-0000-0000-000000000001
```

Adds the principal with the specified object identifier as a SharePoint Online multi-geo administrator.

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
Specifies the alias of the group to add as a SharePoint Online multi-geo administrator.

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
Specifies the object identifier of the principal to add as a SharePoint Online multi-geo administrator.

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
Specifies the user principal name of the user to add as a SharePoint Online multi-geo administrator.

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
This cmdlet does not return output.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

