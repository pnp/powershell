---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFileSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPFileSharingLink
---
  
# Remove-PnPFileSharingLink

## SYNOPSIS
Removes sharing links associated with a file.

## SYNTAX

```powershell
Remove-PnPFileSharingLink -FileUrl <String> -Identity <String> -Force <SwitchParamter> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Removes sharing links associated with a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFileSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
```

This will delete all the sharing links associated with the `Test.docx` file in the `Shared Documents` document library.

### EXAMPLE 2
```powershell
Remove-PnPFileSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Force
```

This will delete all the sharing links associated with the `Test.docx` file in the `Shared Documents` document library. User will not be prompted for confirmation.

### EXAMPLE 3
```powershell
$sharingLinks = Get-PnPFileSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
Remove-PnPFileSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Identity $sharingLinks[0].Id -Force
```

This will delete the first sharing link associated with the `Test.docx` file in the `Shared Documents` document library. User will not be prompted for confirmation.

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

### -FileUrl
The file in the site

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id of the sharing link associated with the file.
You can retrieve the identity using `Get-PnPFileSharingLink` cmdlet.

```yaml
Type: Identity
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be requested and the action will be performed

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
