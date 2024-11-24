---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPEventReceiver.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPEventReceiver
---

# Get-PnPEventReceiver

## SYNOPSIS

Returns registered event receivers

## SYNTAX

### Default (Default)

```
Get-PnPEventReceiver [-List <ListPipeBind>] [-Scope <EventReceiverScope>]
 [-Identity <EventReceiverPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

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

The Guid of the event receiver

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

### -Includes

Optionally allows properties to be retrieved for the returned event receiver which are not included in the response by default

```yaml
Type: String[]
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

### -List

The list object from which to get the event receiver object

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

The scope of the EventReceivers to retrieve

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
