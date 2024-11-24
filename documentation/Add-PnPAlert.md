---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPAlert.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPAlert
---

# Add-PnPAlert

## SYNOPSIS

Adds an alert for a user to a list

## SYNTAX

### Default (Default)

```
Add-PnPAlert [-List] <ListPipeBind> [-Title <String>] [-User <UserPipeBind>]
 [-DeliveryMethod <AlertDeliveryChannel>] [-ChangeType <AlertEventType>]
 [-Frequency <AlertFrequency>] [-Filter <AlertFilter>] [-Time <DateTime>]
 [-AlertTemplateName <string>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets allows to add an alert for a user to a list.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPAlert -List "Demo List"
```

Adds a new alert to the "Demo List" for the current user.

### EXAMPLE 2

```powershell
Add-PnPAlert -Title "Daily summary" -List "Demo List" -Frequency Daily -ChangeType All -Time (Get-Date -Hour 11 -Minute 00 -Second 00)
```

Adds a daily alert for the current user at the given time to the "Demo List". Note: a timezone offset might be applied so please verify on your tenant that the alert indeed got the right time.

### EXAMPLE 3

```powershell
Add-PnPAlert -Title "Alert for user" -List "Demo List" -User "i:0#.f|membership|Alice@contoso.onmicrosoft.com"
```

Adds a new alert for user "Alice" to the "Demo List". Note: Only site owners and admins are permitted to set alerts for other users.

### EXAMPLE 4

```powershell
Add-PnPAlert -Title "Alert for user" -List "Demo List" -User "i:0#.f|membership|Alice@contoso.onmicrosoft.com" -Frequency Daily -Time ((Get-Date).AddDays(1))
```

Adds a new weekly alert for user "Alice" to the "Demo List". The moment the alert will be sent out is based on the Date passed in through -Time. It will take the date and time you pass in and make that the day and time of the week to send out the alert. I.e. if today is a Friday at it is 5.00 PM and you provide (Get-Date).AddDays(1), it will schedule the alert to be sent out on Saturdays at 5.00 PM. Through the web interface of SharePoint Online, the date and time shown with the alert will be converted to its equivalent in PST (Redmond time zone), regardless of the region configuration of the site.

## PARAMETERS

### -AlertTemplateName

To define a particular alert template. Refer this [link](https://learn.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/bb802738(v=office.14)) to specify the template value name.

```yaml
Type: string
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

### -ChangeType

Alert change type

```yaml
Type: AlertEventType
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
AcceptedValues:
- AddObject
- ModifyObject
- DeleteObject
- Discussion
- All
HelpMessage: ''
```

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

### -DeliveryMethod

Alert delivery method

```yaml
Type: AlertDeliveryChannel
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
AcceptedValues:
- Email
- Sms
HelpMessage: ''
```

### -Filter

Alert filter

```yaml
Type: AlertFilter
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
AcceptedValues:
- AnythingChanges
- SomeoneElseChangesAnItem
- SomeoneElseChangesItemCreatedByMe
- SomeoneElseChangesItemLastModifiedByMe
HelpMessage: ''
```

### -Frequency

Alert frequency

For daily: Use the -Time parameter to specify the time of the day the alert should be sent out. I.e. pass in -Time (Get-Date -Hour 11 -Minute 00 -Second 00) to have the alerts sent every day at 11 AM.

For weekly: It will take the date and time you pass in and make that the day and time of the week to send out the alert. I.e. if today is a Friday at it is 5.00 PM and you provide (Get-Date).AddDays(1), it will schedule the alert to be sent out on Saturdays at 5.00 PM. Through the web interface of SharePoint Online, the date and time shown with the alert will be converted to its equivalent in PST (Redmond time zone), regardless of the region configuration of the site.

```yaml
Type: AlertFrequency
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
AcceptedValues:
- Immediate
- Daily
- Weekly
HelpMessage: ''
```

### -List

The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Time

Alert time (if frequency is not immediate). See additional notes on how to use this parameter under -Frequency.

```yaml
Type: DateTime
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

### -Title

Alert title

```yaml
Type: String
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

### -User

User to create the alert for (User ID, login name or actual User object). Skip this parameter to create an alert for the current user. Note: Only site owners can create alerts for other users.

```yaml
Type: UserPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
