---
Module Name: PnP.PowerShell
title: Set-PnPApplicationCustomizer
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPApplicationCustomizer.html
---
 
# Set-PnPApplicationCustomizer

## SYNOPSIS
Updates a SharePoint Framework client side extension application customizer

## SYNTAX

### Custom Action Id
```powershell
Set-PnPApplicationCustomizer [[-Identity] <UserCustomActionPipeBind>] [-Scope <CustomActionScope>]
 [-Title <String>] [-Description <String>] [-Sequence <Int32>] [-ClientSideComponentProperties <String>] [-ClientSideHostProperties> <String>]
 [-Connection <PnPConnection>]   
```

### Client Side Component Id
```powershell
Set-PnPApplicationCustomizer [-ClientSideComponentId <Guid>] [-Scope <CustomActionScope>]
 [-Title <String>] [-Description <String>] [-Sequence <Int32>] [-ClientSideComponentProperties <String>] [-ClientSideHostProperties> <String>]
 [-Connection <PnPConnection>]   
```

## DESCRIPTION
Updates a SharePoint Framework client side extension application customizer by updating its custom action. Only the properties that will be provided will be updated. Others will remain as they are.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPApplicationCustomizer -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Updates the custom action representing the client side extension registration with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 2
```powershell
Set-PnPApplicationCustomizer -ClientSideComponentId aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope web -ClientSideComponentProperties "{`"sourceTermSet`":`"PnP-CollabFooter-SharedLinks`",`"personalItemsStorageProperty`":`"PnP-CollabFooter-MyLinks`"}"
```

Updates the custom action(s) properties being registered for a SharePoint Framework solution having the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' in its manifest from the current web.

## PARAMETERS

### -ClientSideComponentId
The Client Side Component Id of the SharePoint Framework client side extension application customizer found in the manifest for which existing custom action(s) should be updated

```yaml
Type: Guid
Parameter Sets: Client Side Component Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties of the application customizer to update. Specify values as a json string : "{Property1 : 'Value1', Property2: 'Value2'}". Omit to not update this property.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideHostProperties
The Client Side Host Properties of the application customizer to update. Omit to not update this property.

```yaml
Type: String
Parameter Sets: (All)

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

### -Description
The description of the application customizer. Omit to not update this property.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The id or name of the CustomAction representing the client side extension registration that needs to be updated or a CustomAction instance itself

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
Define if the CustomAction representing the client side extension registration is to be found at the web or site collection scope. Specify All to update the component on both web and site collection level.

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

### -Sequence
Sequence of this application customizer being injected. Use when you have a specific sequence with which to have multiple application customizers being added to the page. Omit to not update this property.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the application customizer. Omit to not update this property.

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

