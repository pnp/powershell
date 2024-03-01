---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPLibraryFileVersionBatchDeleteJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPLibraryFileVersionBatchDeleteJob
---
  
# Remove-PnPLibraryFileVersionBatchDeleteJob

## SYNOPSIS

Cancels further processing of a file version batch trim job for a document library.

## SYNTAX

```powershell
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity <ListPipeBind> [-Force]
```

## DESCRIPTION

Cancels further processing of a file version batch trim job for a document library.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents"
```

Cancels further processing of the file version batch trim job for the document library.

### EXAMPLE 2
```powershell
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360 -Force
```

Cancels further processing of the file version batch trim job for the document library, without prompting the user for confirmation.

## PARAMETERS

### -Identity
The ID, name or Url (Lists/MyList) of the document library to stop further trimming on.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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
