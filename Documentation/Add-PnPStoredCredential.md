---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpstoredcredential
schema: 2.0.0
title: Add-PnPStoredCredential
---

# Add-PnPStoredCredential

## SYNOPSIS
Adds a credential to the Windows Credential Manager

## SYNTAX

```
Add-PnPStoredCredential -Name <String> -Username <String> [-Password <SecureString>] [-Overwrite]
 [<CommonParameters>]
```

## DESCRIPTION
Adds an entry to the Windows Credential Manager. If you add an entry in the form of the URL of your tenant/server PnP PowerShell will check if that entry is available when you connect using Connect-PnPOnline. If it finds a matching URL it will use the associated credentials.

If you add a Credential with a name of "https://yourtenant.sharepoint.com" it will find a match when you connect to "https://yourtenant.sharepoint.com" but also when you connect to "https://yourtenant.sharepoint.com/sites/demo1". Of course you can specify more granular entries, allow you to automatically provide credentials for different URLs.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPStoredCredential -Name https://tenant.sharepoint.com -Username yourname@tenant.onmicrosoft.com
```

You will be prompted to specify the password and a new entry will be added with the specified values

### EXAMPLE 2
```powershell
Add-PnPStoredCredential -Name https://tenant.sharepoint.com -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
```

A new entry will be added with the specified values

### EXAMPLE 3
```powershell
Add-PnPStoredCredential -Name https://tenant.sharepoint.com -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
Connect-PnPOnline -Url https://tenant.sharepoint.com/sites/mydemosite
```

A new entry will be added with the specified values, and a subsequent connection to a sitecollection starting with the entry name will be made. Notice that no password prompt will occur.

## PARAMETERS

### -Name
The credential to set

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
{{ Fill Overwrite Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
If not specified you will be prompted to enter your password. 
If you want to specify this value use ConvertTo-SecureString -String 'YourPassword' -AsPlainText -Force

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)