---
Module Name: PnP.PowerShell
title: Set-PnPPropertyBagValue
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPropertyBagValue.html
---
 
# Set-PnPPropertyBagValue

## SYNOPSIS
Adds a new or updates an existing property bag value.

## SYNTAX

### Web
```powershell
Set-PnPPropertyBagValue -Key <String> -Value <String> [-Indexed] 
 [-Connection <PnPConnection>] 
```

### Folder
```powershell
Set-PnPPropertyBagValue -Key <String> -Value <String> [-Folder <String>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Updates an existing property bag value or adds it as a new key\value pair if it doesn't exist yet. If working with a modern SharePoint Online site or having noscript enabled, you will have to disable this yourself temporarily using `Set-PnPTenantSite -Url <url> -NoScriptSite:$false` to be able to make the change.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue
```

This sets or adds a value to the current web property bag.

### EXAMPLE 2
```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /
```

This sets or adds a value to the root folder of the current web.

### EXAMPLE 3
```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /MyFolder
```

This sets or adds a value to the folder MyFolder which is located in the root folder of the current web.

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
Parameter Sets: Folder

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Indexed
Sets the key to be indexed, which makes the property bag value searchable.

```yaml
Type: SwitchParameter
Parameter Sets: Web

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
Key of the property to set.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
Value to set.

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

