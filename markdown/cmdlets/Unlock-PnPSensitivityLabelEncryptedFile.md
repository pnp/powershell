---
Module Name: PnP.PowerShell
title: Unlock-PnPSensitivityLabelEncryptedFile
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Unlock-PnPSensitivityLabelEncryptedFile.html
---
 
# Unlock-PnPSensitivityLabelEncryptedFile

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

```powershell
Unlock-PnPSensitivityLabelEncryptedFile -Url <String> -JustificationText <string> [-Connection <PnPConnection>] 
```

## DESCRIPTION

It removes encryption on a Sensitivity label encrypted file in SharePoint Online.

## EXAMPLES

### EXAMPLE 1
```powershell
Unlock-PnPSensitivityLabelEncryptedFile -Url "https://contoso.com/sites/Marketing/Shared Documents/Doc1.docx" -JustificationText "Need to access file"
```

This example will remove a regular label with admin defined encryption from the file Doc1.docx and also make an entry in audit logs.

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
Full URL for the file

```yaml
Type: string.
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -JustificationText
Text that explains the reason to run this cmdlet on the given file.

```yaml
Type: string.
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

