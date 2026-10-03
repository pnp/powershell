---
Module Name: PnP.PowerShell
title: Get-PnPPageComponent
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPageComponent.html
---
 
# Get-PnPPageComponent

## SYNOPSIS
Retrieve one or more page components from a page

## SYNTAX

```powershell
Get-PnPPageComponent [-Page] <PagePipeBind> [-InstanceId <Guid>] 
 [-Connection <PnPConnection>] 
```

```powershell
Get-PnPPageComponent [-Page] <PagePipeBind> [-ListAvailable] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command allows the retrieval of the components placed on a modern sitepage along with its properties. Note that for a newly created modern site, the Home.aspx page will not be returning any components. This is because the underlying CanvasContent1 will not be populated until the homepage has been edited and published. The reason for this behavior is to allow for the default homepage to be able to be updated by Microsoft as long as it hasn't been modified. For any other site page or after editing and publishing the homepage, this command will return the correct components as they are positioned on the site page.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPageComponent -Page Home
```

Returns all controls defined on the given page.

### EXAMPLE 2
```powershell
Get-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns a specific control defined on the given page.

### EXAMPLE 3
```powershell
Get-PnPPageComponent -Page Home -ListAvailable
```

Returns all available components that can be added to the page.

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

### -InstanceId
The instance id of the component

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Page
The name of the page

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

