---
Module Name: PnP.PowerShell
title: Remove-PnPJavaScriptLink
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPJavaScriptLink.html
---
 
# Remove-PnPJavaScriptLink

## SYNOPSIS
Removes a JavaScript link or block from a web or sitecollection

## SYNTAX

```powershell
Remove-PnPJavaScriptLink [[-Identity] <UserCustomActionPipeBind>] [-Force] [-Scope <CustomActionScope>]
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to remove JavaScript link or block from a web or sitecollection.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPJavaScriptLink -Identity jQuery
```

Removes the injected JavaScript file with the name jQuery from the current web after confirmation

### EXAMPLE 2
```powershell
Remove-PnPJavaScriptLink -Identity jQuery -Scope Site
```

Removes the injected JavaScript file with the name jQuery from the current site collection after confirmation

### EXAMPLE 3
```powershell
Remove-PnPJavaScriptLink -Identity jQuery -Scope Site -Confirm:$false
```

Removes the injected JavaScript file with the name jQuery from the current site collection and will not ask for confirmation

### EXAMPLE 4
```powershell
Remove-PnPJavaScriptLink -Scope Site
```

Removes all the injected JavaScript files from the current site collection after confirmation for each of them

### EXAMPLE 5
```powershell
Remove-PnPJavaScriptLink -Identity faea0ce2-f0c2-4d45-a4dc-73898f3c2f2e -Scope All
```

Removes the injected JavaScript file with id faea0ce2-f0c2-4d45-a4dc-73898f3c2f2e from both the Web and Site scopes

### EXAMPLE 6
```powershell
Get-PnPJavaScriptLink -Scope All | ? Sequence -gt 1000 | Remove-PnPJavaScriptLink
```

Removes all the injected JavaScript files from both the Web and Site scope that have a sequence number higher than 1000

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
Use the -Force flag to bypass the confirmation question

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
Name or id of the JavaScriptLink to remove. Omit if you want to remove all JavaScript Links.

```yaml
Type: UserCustomActionPipeBind
Parameter Sets: (All)
Aliases: Key, Name

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
Define if the JavaScriptLink is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection.

```yaml
Type: CustomActionScope
Parameter Sets: (All)
Accepted values: Web, Site, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

