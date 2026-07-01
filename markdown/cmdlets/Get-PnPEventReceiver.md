---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEventReceiver.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPEventReceiver
---
  
# Get-PnPEventReceiver

## SYNOPSIS
Returns registered event receivers

## SYNTAX

```powershell
Get-PnPEventReceiver [-List <ListPipeBind>] [-Scope <EventReceiverScope>] [-Identity <EventReceiverPipeBind>] 
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION
Returns all registered or a specific event receiver

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEventReceiver
```

This will return all registered event receivers on the current web

### EXAMPLE 2
```powershell
Get-PnPEventReceiver -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will return the event receiver with the provided ReceiverId "fb689d0e-eb99-4f13-beb3-86692fd39f22" from the current web

### EXAMPLE 3
```powershell
Get-PnPEventReceiver -Identity MyReceiver
```

This will return the event receiver with the provided ReceiverName "MyReceiver" from the current web

### EXAMPLE 4
```powershell
Get-PnPEventReceiver -List "ProjectList"
```

This will return all registered event receivers in the provided "ProjectList" list

### EXAMPLE 5
```powershell
Get-PnPEventReceiver -List "ProjectList" -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will return the event receiver in the provided "ProjectList" list with with the provided ReceiverId "fb689d0e-eb99-4f13-beb3-86692fd39f22"

### EXAMPLE 6
```powershell
Get-PnPEventReceiver -List "ProjectList" -Identity MyReceiver
```

This will return the event receiver in the "ProjectList" list with the provided ReceiverName "MyReceiver"

### EXAMPLE 7
```powershell
Get-PnPEventReceiver -Scope Site
```

This will return all the event receivers defined on the current site collection

### EXAMPLE 8
```powershell
Get-PnPEventReceiver -Scope Web
```

This will return all the event receivers defined on the current site

### EXAMPLE 9
```powershell
Get-PnPEventReceiver -Scope All
```

This will return all the event receivers defined on the current site and web

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

### -Identity
The Guid of the event receiver

```yaml
Type: EventReceiverPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The list object from which to get the event receiver object

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
The scope of the EventReceivers to retrieve

```yaml
Type: EventReceiverScope
Parameter Sets: Scope
Accepted values: Web, Site, All

Required: False
Position: Named
Default value: Web
Accept pipeline input: False
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned event receiver which are not included in the response by default

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
