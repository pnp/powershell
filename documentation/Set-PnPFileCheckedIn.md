---
Module Name: PnP.PowerShell
title: Set-PnPFileCheckedIn
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFileCheckedIn.html
---
 
# Set-PnPFileCheckedIn

## SYNOPSIS
Checks in a file.

## SYNTAX

```powershell
Set-PnPFileCheckedIn [-Url] <String> [-CheckInType <CheckInType>] [-Comment <String>] [-Approve]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet checks in a single file, optionally with a comment.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPFileCheckedIn -Url "/Documents/Contract.docx"
```

Checks in the file "Contract.docx" in the "Documents" library

### EXAMPLE 2
```powershell
Set-PnPFileCheckedIn -Url "/Documents/Contract.docx" -CheckInType MinorCheckIn -Comment "Smaller changes"
```

Checks in the file "Contract.docx" in the "Documents" library as a minor version and adds the check in comment "Smaller changes"

## PARAMETERS

### -Approve
Approves the file.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CheckInType
The check in type to use. Defaults to MajorCheckIn.

```yaml
Type: CheckInType
Parameter Sets: (All)
Accepted values: MinorCheckIn, MajorCheckIn, OverwriteCheckIn

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Comment
The check in comment.

```yaml
Type: String
Parameter Sets: (All)

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

### -Url
The server relative url of the file to check in.

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

