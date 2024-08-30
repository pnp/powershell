---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPLibraryFileVersionBatchDeleteJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPLibraryFileVersionBatchDeleteJob
---
  
# New-PnPLibraryFileVersionBatchDeleteJob

## SYNOPSIS

Starts a file version batch trim job for a document library.

## SYNTAX

```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity <ListPipeBind> -DeleteBeforeDays <int> [-Force]
```

## DESCRIPTION

Starts a file version batch trim job for a document library.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360
```

Starts a file version batch trim job that will delete all file versions that are over 360 days old in the document library.

### EXAMPLE 2
```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360 -Force
```

Starts a file version batch trim job that will delete all file versions that are over 360 days old in the document library, without prompting the user for confirmation.

### EXAMPLE 3
```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -Automatic
```

Starts a file version batch trim job that will delete file versions that expired and set version expiration time for the ones not expired in the document library based on the backend algorithm.

### EXAMPLE 4
```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -MajorVersionLimit 30 -MajorWithMinorVersionsLimit 10
```

Starts a file version batch trim job that will delete file versions in the document library based on the version count limits.

## PARAMETERS

### -Identity
The ID, name or Url (Lists/MyList) of the document library to perform the trimming on.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DeleteBeforeDays
The minimum age of file versions to trim. In other words, all file versions that are older than this number of days will be deleted.

```yaml
Type: int
Parameter Sets: DeleteOlderThanDays

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Automatic
Trim file version using automatic trim.

```yaml
Type: SwitchParameter
Parameter Sets: AutomaticTrim

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorVersionLimit
Trim file version using version count limits. Need to specify MajorWithMinorVersionsLimit as well.

```yaml
Type: int
Parameter Sets: CountLimits

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorWithMinorVersionsLimit
Trim file version using version count limits. Need to specify MajorVersionLimit as well.

```yaml
Type: int
Parameter Sets: CountLimits

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
When provided, no confirmation prompts will be shown to the user.

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
