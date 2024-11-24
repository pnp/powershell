---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPEventReceiver.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPEventReceiver
---

# Remove-PnPEventReceiver

## SYNOPSIS

Remove an event receiver.

## SYNTAX

### Default (Default)

```
Remove-PnPEventReceiver -Identity <EventReceiverPipeBind> [-List <ListPipeBind>]
 [-Scope <EventReceiverScope>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Force

Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The Guid or name of the event receiver.

```yaml
Type: EventReceiverPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

The list object from where to remove the event receiver object.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

The scope of the event receivers to remove.

```yaml
Type: EventReceiverScope
DefaultValue: Web
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Scope
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Web
- Site
- All
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
