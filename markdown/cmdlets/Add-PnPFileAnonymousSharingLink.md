---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFileAnonymousSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFileAnonymousSharingLink
---
  
# Add-PnPFileAnonymousSharingLink

## SYNOPSIS
Creates an anonymous sharing link to share a file.

## SYNTAX

```powershell
Add-PnPFileAnonymousSharingLink -FileUrl <String> -Type <PnP.Core.Model.Security.ShareType> -Password <String> -ExpirationDateTime <DateTime> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates an anonymous sharing link to share a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be viewable.

### EXAMPLE 2
```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Type Edit -Password "PnPRocks!"
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be editable by anonymous users after specifying the password.

### EXAMPLE 3
```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Type View -ExpirationDateTime (Get-Date).AddDays(15)
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be viewable by anonymous users. The link will expire after 15 days.

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

### -ShareType
The type of sharing that you want to, i.e do you want to enable people in your organization to view the shared content or also edit the content?

`CreateOnly` value is not supported.

```yaml
Type: PnP.Core.Model.Security.ShareType
Parameter Sets: (All)

Required: False
Position: Named
Default value: View
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
The password to be set for the file to be shared.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpirationDateTime
The expiration date to be after which the file link will expire.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
