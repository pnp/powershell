---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteVersionPolicyProgress.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteVersionPolicyProgress
---
  
# Get-PnPSiteVersionPolicyProgress

## SYNOPSIS
Get the progress of setting version policy for existing document libraries on the site.

## SYNTAX

```powershell
Get-PnPSiteVersionPolicyProgress [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the progress of setting version policy for existing document libraries on the site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteVersionPolicyProgress
```

Returns the progress of setting version policy for existing document libraries on the site.

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