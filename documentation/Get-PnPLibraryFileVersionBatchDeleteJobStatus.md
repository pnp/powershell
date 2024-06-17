---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPLibraryFileVersionBatchDeleteJobStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPLibraryFileVersionBatchDeleteJobStatus
---
  
# Get-PnPLibraryFileVersionBatchDeleteJobStatus

## SYNOPSIS
Get the progress of deleting existing file versions on the document library.

## SYNTAX

```powershell
Get-PnPLibraryFileVersionBatchDeleteJobStatus -Identity <ListPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the progress of deleting existing file versions on the document library.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPLibraryFileVersionBatchDeleteJobStatus -Identity "Documents"
```

Returns the progress of deleting existing file versions on the document library.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
