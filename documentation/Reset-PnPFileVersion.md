---
Module Name: PnP.PowerShell
title: Reset-PnPFileVersion
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPFileVersion.html
---
 
# Reset-PnPFileVersion

## SYNOPSIS
Resets a file to its previous version

## SYNTAX

```powershell
Reset-PnPFileVersion -ServerRelativeUrl <String> [-CheckinType <CheckinType>] [-CheckInComment <String>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Allows to rollback a file to its previous version.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPFileVersion -ServerRelativeUrl "/sites/test/office365.png"
```

### EXAMPLE 2
```powershell
Reset-PnPFileVersion -ServerRelativeUrl "/sites/test/office365.png" -CheckinType MajorCheckin -Comment "Restored to previous version"
```

## PARAMETERS

### -CheckInComment
The comment added to the check-in. Defaults to 'Restored to previous version'.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CheckinType
The check in type to use. Defaults to Major.

```yaml
Type: CheckinType
Parameter Sets: (All)
Accepted values: MinorCheckIn, MajorCheckIn, OverwriteCheckIn

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

### -ServerRelativeUrl
The server relative URL of the file.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

