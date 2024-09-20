---
Module Name: PnP.PowerShell
title: Get-PnPManagedAppId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPManagedAppId.html
---
 
# Get-PnPManagedAppId

## SYNOPSIS
Retrieve an App Id associated with a Url from either the Windows Credential Manager, the MacOS Key chain or if you use the Microsoft.PowerShell.SecretManagement module, a default vault.

## SYNTAX

```powershell
Get-PnPManagedAppId -Url <String> 
```

## DESCRIPTION
Returns an associated App Id from the Windows Credential Manager or Mac OS Key Chain Entry.

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

