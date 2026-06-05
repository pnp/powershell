---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAppInfo.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAppInfo
---
  
# Get-PnPAppInfo

## SYNOPSIS
Returns information about installed apps.

## SYNTAX

### By Id
```powershell
Get-PnPAppInfo -ProductId <Guid>
```

### By Name
```powershell
Get-PnPAppInfo -Name <String>
```

## DESCRIPTION

The Get-PnPAppInfo cmdlet gets all the installed applications from an external marketplace or from the App Catalog that contain `Name` in their application names or the installed application with mentioned `ProductId`.

The returned collection of installed applications contains Product ID (GUID), Product name and Source.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAppInfo -Name "Excel Service"
```

This will return all installed apps from the external marketplace or from the App Catalog that contain "Excel Service" in the application name.

### EXAMPLE 2
```powershell
Get-PnPAppInfo -ProductId 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will return the installed application info for the app with the given product id.

### EXAMPLE 3
```powershell
Get-PnPAppInfo -Name " " | Sort -Property Name
```

Returns all installed apps that have a space in the name and sorts them by name in ascending order.

## PARAMETERS

### -Name
Specifies the application's name.

```yaml
Type: String
Parameter Sets: By Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProductId
Specifies the id of an application

```yaml
Type: Guid
Parameter Sets: By Id

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


