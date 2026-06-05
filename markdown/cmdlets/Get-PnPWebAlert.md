---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebAlert.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPWebAlert
---

# Get-PnPWebAlert

## SYNOPSIS
Returns alerts from the current web, optionally filtered by list and user.

## SYNTAX

### All
```powershell
Get-PnPWebAlert [-UserName <String>] [-UserId <Guid>] [-Connection <PnPConnection>]
```

### By List Id
```powershell
Get-PnPWebAlert -ListId <Guid> [-UserName <String>] [-UserId <Guid>] [-Connection <PnPConnection>]
```

### By List Url
```powershell
Get-PnPWebAlert -ListUrl <String> [-UserName <String>] [-UserId <Guid>] [-Connection <PnPConnection>]
```

### By List Title
```powershell
Get-PnPWebAlert -ListTitle <String> [-UserName <String>] [-UserId <Guid>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Retrieves alerts from the current web using the REST API. You can optionally filter by list (ID, URL, or title) and by user. Specify either UserName or UserId, but not both.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWebAlert
```

Returns all alerts for the current web.

### EXAMPLE 2
```powershell
Get-PnPWebAlert -ListTitle "Documents"
```

Returns alerts for the list with title "Documents".

### EXAMPLE 3
```powershell
Get-PnPWebAlert -ListUrl "Lists/Tasks" -UserName "alex.wilber@contoso.com"
```

Returns alerts for the list at the specified URL and for the specified user.

### EXAMPLE 4
```powershell
Get-PnPWebAlert -UserId 12345678-90ab-cdef-1234-567890abcdef
```

Returns alerts for the specified user ID.

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

### -ListId
The ID of the list to filter alerts by.

```yaml
Type: Guid
Parameter Sets: By List Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListUrl
The server-relative URL of the list to filter alerts by.

```yaml
Type: String
Parameter Sets: By List Url

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListTitle
The title of the list to filter alerts by.

```yaml
Type: String
Parameter Sets: By List Title

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The user ID (GUID) to filter alerts by. Do not specify together with UserName.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserName
The user principal name to filter alerts by. Do not specify together with UserId.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### Type
PnP.PowerShell.Commands.Model.SharePoint.WebAlert

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
