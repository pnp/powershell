---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteSetVersionPolicyProgress.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteSetVersionPolicyProgress
---
  
# Get-PnPSiteSetVersionPolicyProgress

## SYNOPSIS
Get the progress of setting version policy for existing document libraries on the site.

## SYNTAX

```powershell
Get-PnPSiteSetVersionPolicyProgress [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the progress of setting version policy for existing document libraries on the site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteSetVersionPolicyProgress
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