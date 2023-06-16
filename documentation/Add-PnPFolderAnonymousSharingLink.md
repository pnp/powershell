---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFolderAnonymousSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFolderAnonymousSharingLink
---
  
# Add-PnPFolderAnonymousSharingLink

## SYNOPSIS
Creates an anonymous sharing link to share a folder.

## SYNTAX

```powershell
Add-PnPFolderAnonymousSharingLink -Folder <FolderPipeBind> -Type <PnP.Core.Model.Security.ShareType> -Password <String> -ExpirationDateTime <DateTime> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a new anonymous sharing link for a folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be viewable to anonymous users.

### EXAMPLE 2
```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit -Password "PnPRocks!"
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be editable by anonymous users with the specified password.

### EXAMPLE 2
```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit -Password "PnPRocks!" -ExpirationDateTime (Get-Date).AddDays(15)
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be editable by anonymous users with the specified password. The link will expire after 15 days.

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

### -Folder
The folder in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShareType
The type of sharing that you want to, i.e do you want to enable anonymous users to view the shared content or also edit the content?

`Review` and `BlocksDownload` values are not supported.

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
The password for the folder which will be shared anonymously.

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
The expiration date for the folder after which the shared link will stop working.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
