---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailablePageComponents.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAvailablePageComponents
Module Name: PnP.PowerShell
---
  
# Get-PnPAvailablePageComponents

## SYNOPSIS
Retrieves the page components that can be added to a page

## SYNTAX

```powershell
Get-PnPAvailablePageComponents [-Page] <PagePipeBind> [-Component <PageComponentPipeBind>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet returns the client side web parts that are available to be added to the specified page, which includes the web parts deployed to the site through the app catalog. Use it to look up the identifier of a component before adding it with [Add-PnPPageWebPart](Add-PnPPageWebPart.md).

To retrieve the components that are already placed on a page instead, use [Get-PnPPageComponent](Get-PnPPageComponent.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAvailablePageComponents -Page Home
```

Returns all the page components that can be added to the page named 'Home'.

### EXAMPLE 2
```powershell
Get-PnPAvailablePageComponents -Page Home -Component "HelloWorld"
```

Returns the page component named 'HelloWorld' that can be added to the page named 'Home'.

## PARAMETERS

### -Component
The name or id of a specific component to return. If not provided, all available components are returned.

```yaml
Type: PageComponentPipeBind
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

### -Page
The name of the page to retrieve the available components for.

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

