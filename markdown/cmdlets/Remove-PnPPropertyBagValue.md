---
Module Name: PnP.PowerShell
title: Remove-PnPPropertyBagValue
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPropertyBagValue.html
---
 
# Remove-PnPPropertyBagValue

## SYNOPSIS
Removes a value from the property bag.

## SYNTAX

```powershell
Remove-PnPPropertyBagValue [-Key] <String> [-Folder <String>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION
Removes a value from the property bag. If working with a modern SharePoint Online site or having noscript enabled, you will have to disable this yourself temporarily using `Set-PnPTenantSite -Url <url> -NoScriptSite:$false` to be able to make the change.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPropertyBagValue -Key MyKey
```

This will remove the value with key MyKey from the current web property bag.

### EXAMPLE 2
```powershell
Remove-PnPPropertyBagValue -Key MyKey -Folder /MyFolder
```

This will remove the value with key MyKey from the folder MyFolder which is located in the root folder of the current web.

### EXAMPLE 3
```powershell
Remove-PnPPropertyBagValue -Key MyKey -Folder /
```

This will remove the value with key MyKey from the root folder of the current web.

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

### -Force
If provided, no confirmation will be asked to remove the value from the property bag. It will also temporarily enable scripts on the site and then disable it after removing property bag.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
Key of the property bag value to be removed.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
