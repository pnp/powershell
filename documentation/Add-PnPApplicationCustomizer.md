---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPApplicationCustomizer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPApplicationCustomizer
---
  
# Add-PnPApplicationCustomizer

## SYNOPSIS
Adds a SharePoint Framework client side extension application customizer to a specific site collection or web

## SYNTAX

```powershell
Add-PnPApplicationCustomizer [-Title <String>] [-Description <String>] [-Sequence <Int32>]
 [-Scope <CustomActionScope>] -ClientSideComponentId <Guid> [-ClientSideComponentProperties <String>]
 [-ClientSideHostProperties <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a SharePoint Framework client side extension application customizer by registering a user custom action to a web or sitecollection. This can be useful in the scenario where you have a SPFx Application Customizer whcih you decide to deploy to the global app catalog, checking the box to deploy it to the entire tenant. If you then go to the Tenant Wide Extensions list inside the tenant app catalog and set the SPFx Application Customizer its Disabled property to be Yes, you can use this cmdlet to add the functionality of that SPFx component to specific site collections manually. This voids having to add it as an app to every site collection and it being visible in the site contents, yet you having full control over where it should be ran and where not.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPApplicationCustomizer -Title "CollabFooter" -ClientSideComponentId c0ab3b94-8609-40cf-861e-2a1759170b43 -ClientSideComponentProperties "{`"sourceTermSet`":`"PnP-CollabFooter-SharedLinks`",`"personalItemsStorageProperty`":`"PnP-CollabFooter-MyLinks`"}
```

Adds a new application customizer to the current web. This requires that a SharePoint Framework solution has been deployed containing the application customizer specified in its manifest. Be sure to run Install-PnPApp before trying this cmdlet on a site.

## PARAMETERS

### -ClientSideComponentId
The Client Side Component Id of the SharePoint Framework client side extension application customizer found in the manifest

```yaml
Type: Guid
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties of the application customizer. Specify values as a json string : "{Property1 : 'Value1', Property2: 'Value2'}"

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
The Client Side Host Properties of the application customizer. Specify values as a json string : "{'preAllocatedApplicationCustomizerTopHeight': '50', 'preAllocatedApplicationCustomizerBottomHeight': '50'}"

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
The description of the application customizer

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
The scope of the CustomAction to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.

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
Sequence of this application customizer being injected. Use when you have a specific sequence with which to have multiple application customizers being added to the page.

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
The title of the application customizer

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