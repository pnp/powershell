---
Module Name: PnP.PowerShell
title: Request-PnPSyntexClassifyAndExtract
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPSyntexClassifyAndExtract.html
---
 
# Request-PnPSyntexClassifyAndExtract

## SYNOPSIS

Requests for a file, folder or all files in a library to be classified and extracted via the published Microsoft Syntex models on the libraries hosting the files.

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### File

```powershell
Request-PnPSyntexClassifyAndExtract -FileUrl <string> [-Batch <PnPBatch>]  [-Connection <PnPConnection>] 

```

### Folder

```powershell
Request-PnPSyntexClassifyAndExtract -Folder <FolderPipeBind> [-Connection <PnPConnection>] 

```

### List

```powershell
Request-PnPSyntexClassifyAndExtract -List <ListPipeBind> [-OffPeak <SwitchParameter>] [-Force <SwitchParameter>] [-Connection <PnPConnection>] 

```

## DESCRIPTION

This command requests for all files in a library, folder or individual files to be classified and extracted via the published Syntex models on the libraries hosting the files. When using with the `OffPeak` switch then the files are send to the off peak Syntex document processing queue, this way there's no need to enumerate all files in the library and submit them to the regular queue. When using the `Force` switch without setting OffPeak then all files are enumerated and sent to the regular queue, regardless of whether they were processed in the past.

When the list contains more than 5000 files or when using the folder parameter the cmdlet will use the off peak Syntex queue.

## EXAMPLES

### EXAMPLE 1

```powershell
Request-PnPSyntexClassifyAndExtract -FileUrl "/sites/finance/invoices/invoice1.docx" 
```

Requests the classification and extraction of invoice1.docx in library "Invoices".

### EXAMPLE 2

```powershell
Request-PnPSyntexClassifyAndExtract -List "Invoices"
```

Requests the classification and extraction of all files in library "Invoices" that never were classified and extracted before.

### EXAMPLE 3

```powershell
Request-PnPSyntexClassifyAndExtract -Folder (Get-PnPFolder -Url "invoices/Q1/jan")
```

Requests the classification and extraction of all files in the folder "jan" in library "invoices" that never were classified and extracted before.

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

### -List

The name or list holding the files to classify and extract.

```yaml
Type: ListPipeBind
Parameter Sets: List

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Force

If set, then all files (even if classified and extracted before) are classified and extracted. If the list contains more than 5000 items this option will not apply and off-peak processing is used.

```yaml
Type: SwitchParameter
Parameter Sets: List

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OffPeak

If set, then the files to classify are sent to the off peak queue without enumerating them. If the list contains more than 5000 items then off-peak processing is always used.

```yaml
Type: SwitchParameter
Parameter Sets: List

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder

The folder holding the files to classify and extract. When using this parameter, files will be send to the off peak queue.

```yaml
Type: FolderPipeBind
Parameter Sets: Folder

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -FileUrl

The server relative URL of the file to be classified and extracted.

```yaml
Type: String
Parameter Sets: File
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Batch

The batch to add this file classification and extraction request to.

```yaml
Type: PnPBatch
Parameter Sets: File
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
