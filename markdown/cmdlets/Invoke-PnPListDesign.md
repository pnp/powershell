---
Module Name: PnP.PowerShell
title: Invoke-PnPListDesign
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPListDesign.html
---
 
# Invoke-PnPListDesign

## SYNOPSIS
Apply a List Design to an existing site. * Requires Tenant Administration Rights *

## SYNTAX

```powershell
Invoke-PnPListDesign [-Identity] <TenantListDesignPipeBind> [-WebUrl <String>] 
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Applies the List Design provided through Identity to an existing site. 

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPListDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Applies the specified list design to the current site.

### EXAMPLE 2
```powershell
Invoke-PnPListDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -WebUrl "https://contoso.sharepoint.com/sites/mydemosite"
```

Applies the specified site design to the specified site.

### EXAMPLE 3
```powershell
Get-PnPListDesign | ?{$_.Title -eq "Demo"} | Invoke-PnPListDesign
```

Applies the specified list design to the specified site.

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
The List Design Id 

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



### -WebUrl
The URL of the web to apply the list design to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

