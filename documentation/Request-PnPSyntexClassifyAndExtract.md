---
Module Name: PnP.PowerShell
title: Request-PnPSyntexClassifyAndExtract
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPage.html
---
 
# Request-PnPSyntexClassifyAndExtract

## SYNOPSIS
Requests for a file or all files in a library to be classified and extracted via the published SharePoint Syntex models on the libraries hosting the files.

## SYNTAX

### File
```powershell
Request-PnPSyntexClassifyAndExtract -FileUrl <string> [-Batch <PnPBatch>]  [-Connection <PnPConnection>] 
[<CommonParameters>]
```

### List
```powershell
Request-PnPSyntexClassifyAndExtract -List <ListPipeBind> [-Force <SwitchParameter>] [-Connection <PnPConnection>] 
[<CommonParameters>]
```

## DESCRIPTION
This command requests for a file or all files in a library to be classified and extracted via the published SharePoint Syntex models on the libraries hosting the files.

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
The name or list holding the files to classify and extract

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
If set, then all files (even if classified and extracted before) are classified and extracted.

```yaml
Type: SwitchParameter
Parameter Sets: List

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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
