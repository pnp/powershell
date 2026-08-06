---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPManagedAppId.html
title: Get-PnPManagedAppId
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
  
# Get-PnPManagedAppId

## SYNOPSIS
Retrieve an App Id associated with a URL from the Windows Credential Manager, macOS Keychain, Linux Secret Service, or a default vault configured through Microsoft.PowerShell.SecretManagement.

## SYNTAX

```powershell
Get-PnPManagedAppId -Url <String> 
```

## DESCRIPTION
Returns an associated App Id from the Windows Credential Manager, macOS Keychain, Linux Secret Service, or a default vault configured through Microsoft.PowerShell.SecretManagement.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPManagedAppId -Url https://yourtenant.sharepoint.com
```

Returns the App Id associated with the specified tenant Url.

## PARAMETERS

### -Url
The Url for which to retrieve the associated App Id

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


