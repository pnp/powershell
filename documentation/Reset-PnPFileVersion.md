---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/reset-pnpfileversion
schema: 2.0.0
title: Reset-PnPFileVersion
---

# Reset-PnPFileVersion

## SYNOPSIS
Resets a file to its previous version

## SYNTAX

```powershell
Reset-PnPFileVersion -ServerRelativeUrl <String> [-CheckinType <CheckinType>] [-CheckInComment <String>]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

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
The comment added to the checkin. Defaults to 'Restored to previous version'.

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)