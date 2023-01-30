---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPAlert.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPAlert
---
  
# Add-PnPAlert

## SYNOPSIS
Adds an alert for a user to a list

## SYNTAX

```powershell
Add-PnPAlert [-List] <ListPipeBind> [-Title <String>] [-User <UserPipeBind>]
 [-DeliveryMethod <AlertDeliveryChannel>] [-ChangeType <AlertEventType>] [-Frequency <AlertFrequency>]
 [-Filter <AlertFilter>] [-Time <DateTime>] [-AlertTemplateName <string>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

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

Adds a new weekly alert for user "Alice" to the "Demo List". The moment the alert will be sent out is based on the Date passed in through -Time. It will take the date and time you pass in and make that the day and time of the week to send out the alert. I.e. if today is a Friday at it is 5.00 PM and you provide (Get-Date).AddDays(1), it will schedule the alert to be sent out on Saturdays at 5.00 PM. Through the web interface of SharePoint Online, the date and time shown with the alert will be converted to its equivallent in PST (Redmond time zone), regardless of the region configuration of the site.

## PARAMETERS

### -ChangeType
Alert change type

```yaml
Type: AlertEventType
Parameter Sets: (All)
Accepted values: AddObject, ModifyObject, DeleteObject, Discussion, All

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

### -DeliveryMethod
Alert delivery method

```yaml
Type: AlertDeliveryChannel
Parameter Sets: (All)
Accepted values: Email, Sms

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Alert filter

```yaml
Type: AlertFilter
Parameter Sets: (All)
Accepted values: AnythingChanges, SomeoneElseChangesAnItem, SomeoneElseChangesItemCreatedByMe, SomeoneElseChangesItemLastModifiedByMe

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Frequency
Alert frequency

For daily: Use the -Time parameter to specify the time of the day the alert should be sent out. I.e. pass in -Time (Get-Date -Hour 11 -Minute 00 -Second 00) to have the alerts sent every day at 11 AM.

For weekly: It will take the date and time you pass in and make that the day and time of the week to send out the alert. I.e. if today is a Friday at it is 5.00 PM and you provide (Get-Date).AddDays(1), it will schedule the alert to be sent out on Saturdays at 5.00 PM. Through the web interface of SharePoint Online, the date and time shown with the alert will be converted to its equivallent in PST (Redmond time zone), regardless of the region configuration of the site.

```yaml
Type: AlertFrequency
Parameter Sets: (All)
Accepted values: Immediate, Daily, Weekly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Time
Alert time (if frequency is not immediate). See additional notes on how to use this parameter under -Frequency.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Alert title

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
User to create the alert for (User ID, login name or actual User object). Skip this parameter to create an alert for the current user. Note: Only site owners can create alerts for other users.

```yaml
Type: UserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AlertTemplateName
To define a particular alert template. Refer this [link](https://learn.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/bb802738(v=office.14)) to specify the template value name.

```yaml
Type: string
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
