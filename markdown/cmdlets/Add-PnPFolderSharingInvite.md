---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFolderSharingInvite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFolderSharingInvite
---
  
# Add-PnPFolderSharingInvite

## SYNOPSIS
Creates an invitation for users to a folder.

## SYNTAX

```powershell
Add-PnPFolderSharingInvite -Folder <FolderPipeBind> -Users <String[]> -Message <String> -RequireSignIn <SwitchParameter> -SendInvitation <SwitchParameter> -Role <PermissionRole> -ExpirationDateTime <DateTime> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates an invitation for user to a folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFolderSharingInvite -Folder "/sites/demo/Shared Documents/Test" -Users "john@contoso.onmicrosoft.com" -RequireSignIn
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test` folder with Read permissions and will require that user to sign in before accessing the folder.

### EXAMPLE 2
```powershell
Add-PnPFolderSharingInvite -Folder "/sites/demo/Shared Documents/Test" -Users "john@contoso.onmicrosoft.com" -RequireSignIn -SendInvitation -Role Owner
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test` folder with Read permissions and will require that user to sign in before accessing the folder. The invitation will be sent and the user will have `Owner` permissions

### EXAMPLE 3
```powershell
Add-PnPFolderSharingInvite -Folder "/sites/demo/Shared Documents/Test" -Users "john@contoso.onmicrosoft.com" -RequireSignIn -ExpirationDate (Get-Date).AddDays(15)
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test` folder with Read permissions and will require that user to sign in before accessing the folder. The link will expire after 15 days.

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

### -Users
A collection of recipients who will receive access and the sharing invitation.
**Currently, only one user at a time is supported**. We are planning to add support for multiple users a bit later.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
A plain text formatted message that is included in the sharing invitation. Maximum length 2000 characters.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequireSignIn
Specifies where the recipient of the invitation is required to sign-in to view the shared item

```yaml
Type: SwitchParamter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SendInvitation
Specifies if an email or post is generated (false) or if the permission is just created (true).

```yaml
Type: SwitchParamter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
Specify the role that is to be granted to the recipients of the sharing invitation.

Supported values are `Owner`, `Write` and `Read`.

```yaml
Type: PnP.Core.Model.Security.PermissionRole
Parameter Sets: (All)

Required: False
Position: Named
Default value: Read
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpirationDateTime
The expiration date for the folder after which the permission expires.

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
