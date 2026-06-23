---
Module Name: PnP.PowerShell
title: Remove-PnPSiteDesign
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteDesign.html
---
 
# Remove-PnPSiteDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site design.

## SYNTAX

```powershell
Remove-PnPSiteDesign [-Identity] <TenantSiteDesignPipeBind> [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes the specified site design.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Removes the specified site design.

### EXAMPLE 2
```powershell
$siteDesign = Get-PnPSiteDesign -Identity c234b254-b51a-4ca8-8ba3-939659a66832
Remove-PnPSiteDesign -Identity $siteDesign
```

Removes the specified site design.

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

### -Force
If specified you will not be asked to confirm removing the specified site design.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the site design to remove.

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

