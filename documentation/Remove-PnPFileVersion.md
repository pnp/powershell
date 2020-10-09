---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpfileversion
schema: 2.0.0
title: Remove-PnPFileVersion
---

# Remove-PnPFileVersion

## SYNOPSIS
Removes all or a specific file version.

## SYNTAX

### Return as file object (Default)
```powershell
Remove-PnPFileVersion -Url <String> [-Recycle] [-Force] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### All
```powershell
Remove-PnPFileVersion -Url <String> [-All] [-Recycle] [-Force] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### By Id
```powershell
Remove-PnPFileVersion -Url <String> [-Identity <FileVersionPipeBind>] [-Recycle] [-Force] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -Identity 512
```

Removes the file version with Id 512

### EXAMPLE 2
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -Identity "Version 1.0"
```

Removes the file version with label "Version 1.0"

### EXAMPLE 3
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -All
```

Removes all file versions

## PARAMETERS

### -All

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be requested and the action will be performed

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

Only applicable to: SharePoint Online

```yaml
Type: FileVersionPipeBind
Parameter Sets: By Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recycle

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url

Only applicable to: SharePoint Online

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

Only applicable to: SharePoint Online

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)