---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPApplicationCustomizer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPApplicationCustomizer
---
  
# Get-PnPApplicationCustomizer

## SYNOPSIS
Returns all SharePoint Framework client side extension application customizers

## SYNTAX

### Custom Action Id
```powershell
Get-PnPApplicationCustomizer [-Identity <Guid>] [-Scope <CustomActionScope>]
 [-ThrowExceptionIfCustomActionNotFound] [-Connection <PnPConnection>] 
  [-Includes <String[]>] 
```

### Client Side Component Id
```powershell
Get-PnPApplicationCustomizer -ClientSideComponentId <Guid> [-Scope <CustomActionScope>]
 [-ThrowExceptionIfCustomActionNotFound] [-Connection <PnPConnection>] 
  [-Includes <String[]>] 
```

## DESCRIPTION
Returns all SharePoint Framework client side extension application customizers registered on the current web and/or site

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPApplicationCustomizer
```

Returns the custom action representing the SharePoint Framework client side extension registrations registered on the current site collection and web.

### EXAMPLE 2
```powershell
Get-PnPApplicationCustomizer -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Returns the custom action representing the SharePoint Framework client side extension registration with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 3
```powershell
Get-PnPApplicationCustomizer -ClientSideComponentId aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope Web
```

Returns the custom action(s) being registered for a SharePoint Framework solution having the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' in its manifest from the current web.

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
Identity of the SharePoint Framework client side extension application customizer to return. Omit to return all SharePoint Framework client side extension application customizer.

```yaml
Type: Guid
Parameter Sets: Custom Action Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Scope of the SharePoint Framework client side extension application customizer, either Web, Site or All to return both (all is the default)

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

### -ThrowExceptionIfCustomActionNotFound
Switch parameter if an exception should be thrown if the requested SharePoint Framework client side extension application customizer does not exist (true) or if omitted, nothing will be returned in case the SharePoint Framework client side extension application customizer does not exist

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned application customizer which are not included in the response by default

```yaml
Type: String[]
Parameter Sets: (All)

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


