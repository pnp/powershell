---
Module Name: PnP.PowerShell
title: Get-PnPPropertyBag
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPropertyBag.html
---
 
# Get-PnPPropertyBag

## SYNOPSIS
Returns the property bag values.

## SYNTAX

```powershell
Get-PnPPropertyBag [[-Key] <String>] [-Folder <String>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve all property bag values. It is possible to get property bag values for a folder using `Folder` option or a specific property bag value using `Key` option.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPropertyBag
```

This will return all web property bag values

### EXAMPLE 2
```powershell
Get-PnPPropertyBag -Key MyKey
```

This will return the value of the key MyKey from the web property bag

### EXAMPLE 3
```powershell
Get-PnPPropertyBag -Folder /MyFolder
```

This will return all property bag values for the folder MyFolder which is located in the root of the current web

### EXAMPLE 4
```powershell
Get-PnPPropertyBag -Folder /MyFolder -Key vti_mykey
```

This will return the value of the key vti_mykey from the folder MyFolder which is located in the root of the current web

### EXAMPLE 5
```powershell
Get-PnPPropertyBag -Folder / -Key vti_mykey
```

This will return the value of the key vti_mykey from the root folder of the current web

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

### -Folder
Site relative url of the folder. See examples for use.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
Key that should be looked up

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

