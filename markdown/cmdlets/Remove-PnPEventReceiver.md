---
Module Name: PnP.PowerShell
title: Remove-PnPEventReceiver
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEventReceiver.html
---
 
# Remove-PnPEventReceiver

## SYNOPSIS
Remove an event receiver.

## SYNTAX

```powershell
Remove-PnPEventReceiver -Identity <EventReceiverPipeBind> [-List <ListPipeBind>] [-Scope <EventReceiverScope>] [-Force] 
[-Connection <PnPConnection>] 
```

## DESCRIPTION
Removes/unregister a specific event receiver.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPEventReceiver -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will remove the event receiver with ReceiverId "fb689d0e-eb99-4f13-beb3-86692fd39f22" from the current web.

### EXAMPLE 2
```powershell
Remove-PnPEventReceiver -List ProjectList -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will remove the event receiver with ReceiverId "fb689d0e-eb99-4f13-beb3-86692fd39f22" from the "ProjectList" list.

### EXAMPLE 3
```powershell
Remove-PnPEventReceiver -List ProjectList -Identity MyReceiver
```

This will remove the event receiver with ReceiverName "MyReceiver" from the "ProjectList" list.

### EXAMPLE 4
```powershell
Remove-PnPEventReceiver -List ProjectList
```

This will remove all event receivers from the "ProjectList" list.

### EXAMPLE 5
```powershell
Remove-PnPEventReceiver
```

This will remove all event receivers from the current web.

### EXAMPLE 6
```powershell
Get-PnPEventReceiver | ? ReceiverUrl -Like "*azurewebsites.net*" | Remove-PnPEventReceiver
```

This will remove all event receivers from the current web which are pointing to a service hosted on Azure Websites.

### EXAMPLE 7
```powershell
Remove-PnPEventReceiver -Scope Site
```

This will remove all the event receivers defined on the current site collection.

### EXAMPLE 8
```powershell
Remove-PnPEventReceiver -Scope Web
```

This will remove all the event receivers defined on the current web.

### EXAMPLE 9
```powershell
Remove-PnPEventReceiver -Scope All
```

This will remove all the event receivers defined on the current site and web.

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

### -Force
Specifying the Force parameter will skip the confirmation question.

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
The Guid or name of the event receiver.

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
The list object from where to remove the event receiver object.

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
The scope of the event receivers to remove.

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


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
