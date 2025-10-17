---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListRule.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPListRule
---
  
# Add-PnPListRule

## SYNOPSIS
Adds a new SharePoint list or library rule.

## SYNTAX

```powershell
Add-PnPListRule -List <ListPipeBind> -Title <String> -TriggerEventType <String> -ActionType <String>
 [-Description <String>] [-EmailRecipients <String[]>] [-EmailSubject <String>] [-EmailBody <String>]
 [-Condition <String>] [-Enabled] [-Connection <PnPConnection>]
```

## DESCRIPTION
Adds a new rule to a SharePoint list or library. SharePoint Rules are the replacement for SharePoint Alerts which are being retired. Rules can trigger actions like sending email notifications when items are created, modified, or deleted.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPListRule -List "Demo List" -Title "Notify on new items" -TriggerEventType "create" -ActionType "sendEmail" -EmailRecipients "user@contoso.com"
```

Creates a rule that sends an email to the specified recipient when a new item is created in the "Demo List".

### EXAMPLE 2
```powershell
Add-PnPListRule -List "Documents" -Title "Document modified alert" -TriggerEventType "update" -ActionType "sendEmail" -EmailRecipients "team@contoso.com" -EmailSubject "Document Updated" -EmailBody "A document has been modified in the library"
```

Creates a rule that sends a custom email when a document is modified in the "Documents" library.

### EXAMPLE 3
```powershell
Add-PnPListRule -List "Tasks" -Title "Task deleted notification" -TriggerEventType "delete" -ActionType "sendEmail" -EmailRecipients "manager@contoso.com","admin@contoso.com" -Description "Notify managers when tasks are deleted"
```

Creates a rule that notifies multiple recipients when a task is deleted, with a description for the rule.

## PARAMETERS

### -ActionType
The type of action to perform when the rule is triggered (e.g., "sendEmail").

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Condition
Optional condition that must be met for the rule to trigger.

```yaml
Type: String
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

### -Description
Optional description for the rule.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailBody
The body content for email notifications.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailRecipients
The email addresses to send notifications to.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailSubject
The subject line for email notifications.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
Whether the rule is enabled. Enabled by default.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
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

### -Title
The title/name of the rule.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TriggerEventType
The type of event that triggers the rule (e.g., "create", "update", "delete").

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
[SharePoint Alerts Retirement](https://support.microsoft.com/en-us/office/sharepoint-alerts-retirement-813a90c7-3ff1-47a9-8a2f-152f48b2486f)
