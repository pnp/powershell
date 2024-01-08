---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteVersionPolicyForNewLibs.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteVersionPolicyForNewLibs
---
  
# Get-PnPSiteVersionPolicyForNewLibs

## SYNOPSIS
Get version policy setting of the site.

## SYNTAX

```powershell
Get-PnPSiteVersionPolicyForNewLibs [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of version policy setting on the site. When the new document libraries are created, they will be set as the version policy of the site.
If the version policy is not set on the site, the setting of the tenant will be used.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteVersionPolicyForNewLibs
```

Returns the version policy setting of the site.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Docs documentation](https://learn.microsoft.com/sharepoint/dev/solution-guidance/modern-experience-site-classification#programmatically-read-the-classification-of-a-site)