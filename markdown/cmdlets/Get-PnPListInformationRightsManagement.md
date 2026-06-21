---
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListInformationRightsManagement.html
schema: 2.0.0
title: Get-PnPListInformationRightsManagement
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
---
   
# Get-PnPListInformationRightsManagement

## SYNOPSIS
Get the site closure status of the site which has a site policy applied

## SYNTAX

```powershell
Get-PnPListInformationRightsManagement -List <ListPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to get the site closure status of the current site or list which has a site policy applied.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListInformationRightsManagement -List "Documents"
```

Returns Information Rights Management (IRM) settings for the list. See 'Get-Help Set-PnPListInformationRightsManagement -Detailed' for more information about the various values.

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

### -List

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)



