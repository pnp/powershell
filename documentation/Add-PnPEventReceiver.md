---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPEventReceiver.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPEventReceiver
---
  
# Add-PnPEventReceiver

## SYNOPSIS
Adds a new remote event receiver

## SYNTAX

```powershell
Add-PnPEventReceiver [-List <ListPipeBind>] [-Scope <EventReceiverScope>] -Name <String> -Url <String> -EventReceiverType <EventReceiverType>
 -Synchronization <EventReceiverSynchronization> [-SequenceNumber <Int32>] [-Force] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet adds a new remote event receiver.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPEventReceiver -List "ProjectList" -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType ItemAdded -Synchronization Asynchronous
```

This will add a new remote event receiver that is executed after an item has been added to the ProjectList list

### EXAMPLE 2
```powershell
Add-PnPEventReceiver -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType WebAdding -Synchronization Synchronous
```

This will add a new remote event receiver that is executed before a new subsite is being created

### EXAMPLE 3
```powershell
Add-PnPEventReceiver -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType ListAdding -Synchronization Synchronous -Scope Site
```

This will add a new remote event receiver that is executed before a new list is being created inside the site collection scope

### EXAMPLE 4
```powershell
Add-PnPEventReceiver -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType ListDeleted -Synchronization Asynchronous -Scope Web
```

This will add a new remote event receiver that is executed after a list has been deleted from the current site

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

### -EventReceiverType
The type of the event receiver like ItemAdded, ItemAdding. See https://learn.microsoft.com/previous-versions/office/sharepoint-server/jj167297(v=office.15) for the full list of available types.

```yaml
Type: EventReceiverType
Parameter Sets: (All)
Aliases: Type
Accepted values: ItemAdding, ItemUpdating, ItemDeleting, ItemCheckingIn, ItemCheckingOut, ItemUncheckingOut, ItemAttachmentAdding, ItemAttachmentDeleting, ItemFileMoving, ItemVersionDeleting, FieldAdding, FieldUpdating, FieldDeleting, ListAdding, ListDeleting, SiteDeleting, WebDeleting, WebMoving, WebAdding, SiteMovingFromGeoLocation, GroupAdding, GroupUpdating, GroupDeleting, GroupUserAdding, GroupUserDeleting, RoleDefinitionAdding, RoleDefinitionUpdating, RoleDefinitionDeleting, RoleAssignmentAdding, RoleAssignmentDeleting, InheritanceBreaking, InheritanceResetting, WorkflowStarting, ItemAdded, ItemUpdated, ItemDeleted, ItemCheckedIn, ItemCheckedOut, ItemUncheckedOut, ItemAttachmentAdded, ItemAttachmentDeleted, ItemFileMoved, ItemFileConverted, ItemVersionDeleted, FieldAdded, FieldUpdated, FieldDeleted, ListAdded, ListDeleted, SiteDeleted, WebDeleted, WebMoved, WebProvisioned, WebRestored, GroupAdded, GroupUpdated, GroupDeleted, GroupUserAdded, GroupUserDeleted, RoleDefinitionAdded, RoleDefinitionUpdated, RoleDefinitionDeleted, RoleAssignmentAdded, RoleAssignmentDeleted, InheritanceBroken, InheritanceReset, WorkflowStarted, WorkflowPostponed, WorkflowCompleted, EntityInstanceAdded, EntityInstanceUpdated, EntityInstanceDeleted, AppInstalled, AppUpgraded, AppUninstalling, EmailReceived, ContextEvent, InvalidReceiver

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list object or name where the remote event receiver needs to be added. If omitted, the remote event receiver will be added to the web.

```yaml
Type: ListPipeBind
Parameter Sets: List

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
The scope of the EventReceiver to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.

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

### -Name
The name of the remote event receiver

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SequenceNumber
The sequence number where this remote event receiver should be placed

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Synchronization
The synchronization type: Asynchronous or Synchronous

```yaml
Type: EventReceiverSynchronization
Parameter Sets: (All)
Aliases: Sync
Accepted values: DefaultSynchronization, Synchronous, Asynchronous

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The URL of the remote event receiver web service

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
