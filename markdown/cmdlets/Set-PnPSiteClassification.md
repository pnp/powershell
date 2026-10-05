---
Module Name: PnP.PowerShell
title: Set-PnPSiteClassification
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteClassification.html
---
 
# Set-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All (see description below)

Allows placing a classic site classification on the current site.

## SYNTAX

```powershell
Set-PnPSiteClassification -Identity <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows for setting a classic site classification on the currently connected to site. If the site has a Microsoft 365 Group behind it, the classification will be placed on the Microsoft 365 Group and will require either Directory.Read.All or Directory.ReadWrite.All application permissions on Microsoft Graph. If it does not have a Microsoft 365 Group behind it, it will set the site classification on the SharePoint Online site and will not require Microsoft Graph permissions. Use [Get-PnPAvailableSiteClassification](Get-PnPAvailableSiteClassification.md) to get an overview of the available site classifications on the tenant. For the new Microsoft Purview sensitivity labels, use [Set-PnPSiteSensitivityLabel](Set-PnPSiteSensitivityLabel.md) instead.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteClassification -Identity "LBI"
```

Sets the "LBI" site classification on the current site.

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

### -Identity
Specifies the name of the classification tag.

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
