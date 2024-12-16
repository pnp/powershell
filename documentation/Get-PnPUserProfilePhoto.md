---
Module Name: PnP.PowerShell
title: Get-PnPUserProfilePhoto
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUserProfilePhoto.html
---
 
# Get-PnPUserProfilePhoto

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of ProfilePhoto.ReadWrite.All, User.ReadWrite or User.ReadWrite.All

Gets the profile picture of a user.

## SYNTAX

```powershell
Get-PnPUserProfilePhoto -Identity <AzureADUserPipeBind> [-Filename <String>]  [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet downloads the user profile photo to the specified path and filename. If no filename has been specified it will default to the Display Name of the user with the either the extension .png or .jpeg depending on the format of the file.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUserProfilePhoto -Identity "john@contoso.onmicrosoft.com"
```
Downloads the photo for the user specified to the current folder.

### EXAMPLE 2
```powershell
Get-PnPUserProfilePhoto -Identity "john@contoso.onmicrosoft.com" -Filename "john.png"
```
Downloads the photo for the user specified to the current folder and will name the file 'john.png'.

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

### -Filename
The path to the image file to save.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the user to remove. This can be the UPN, the GUID or an instance of the user.

```yaml
Type: AzureADUserPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
