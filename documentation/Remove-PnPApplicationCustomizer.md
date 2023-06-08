---
Module Name: PnP.PowerShell
title: Remove-PnPApplicationCustomizer
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPApplicationCustomizer.html
---
 
# Remove-PnPApplicationCustomizer

## SYNOPSIS
Removes a SharePoint Framework client side extension application customizer

## SYNTAX

### Custom Action Id
```powershell
Remove-PnPApplicationCustomizer [[-Identity] <UserCustomActionPipeBind>] [-Scope <CustomActionScope>] [-Force]
 [-Connection <PnPConnection>]   
```

### Client Side Component Id
```powershell
Remove-PnPApplicationCustomizer -ClientSideComponentId <Guid> [-Scope <CustomActionScope>] [-Force]
 [-Connection <PnPConnection>]   
```

## DESCRIPTION
Removes a SharePoint Framework client side extension application customizer by removing a user custom action from a web or sitecollection

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPApplicationCustomizer -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Removes the custom action representing the client side extension registration with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 2
```powershell
Remove-PnPApplicationCustomizer -ClientSideComponentId aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope web
```

Removes the custom action(s) being registered for a SharePoint Framework solution having the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' in its manifest from the current web.

## PARAMETERS

### -ClientSideComponentId
The Client Side Component Id of the SharePoint Framework client side extension application customizer found in the manifest for which existing custom action(s) should be removed

```yaml
Type: Guid
Parameter Sets: Client Side Component Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
The id or name of the CustomAction representing the client side extension registration that needs to be removed or a CustomAction instance itself

```yaml
Type: UserCustomActionPipeBind
Parameter Sets: Custom Action Id

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
Define if the CustomAction representing the client side extension registration is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection (default).

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



### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

