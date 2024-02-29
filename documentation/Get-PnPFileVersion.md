---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileVersion.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileVersion
---
  
# Get-PnPFileVersion

## SYNOPSIS
Retrieves the previous versions of a file. Does not retrieve the current version of the file. 

## SYNTAX

```powershell
Get-PnPFileVersion -Url <String> [-UseVersionExpirationReport] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Retrieves the version history of a file, not including its current version. To get the current version use the MajorVersion and MinorVersion properties returned from Get-PnPFile.

It can optionally return the version expiration report, which contains the versions' SnapshotDate (or estimated SnapshotDate if it is not available) and estimated ExpirationDate based on the Automatic Version History Limits.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFileVersion -Url Documents/MyDocument.docx
```

Retrieves the file version information for the specified file.

### EXAMPLE 2
```powershell
Get-PnPFileVersion -Url "/sites/blah/Shared Documents/MyDocument.docx"
```

Retrieves the file version information for the specified file by specifying the path to the site and the document library's URL.

### EXAMPLE 3
```powershell
Get-PnPFileVersion -Url "/sites/blah/Shared Documents/MyDocument.docx" -UseVersionExpirationReport
```

Retrieves the version expiration report for the specified file. 

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

### -Url

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseVersionExpirationReport
Returns the file version expiration report. The versions contained in the report has the SnapshotDate (or estimated SnapshotDate if it is not available) and estimated ExpirationDate based on the Automatic Version History Limits.

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


