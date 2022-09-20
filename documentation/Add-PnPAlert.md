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
Alert time (if frequency is not immediate)

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


