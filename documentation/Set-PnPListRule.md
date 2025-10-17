---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListRule.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPListRule
---
  
# Set-PnPListRule

## SYNOPSIS
Updates an existing SharePoint list or library rule.

## SYNTAX

```powershell
Set-PnPListRule -List <ListPipeBind> -Identity <RulePipeBind> [-Title <String>] [-Description <String>]
 [-TriggerEventType <String>] [-ActionType <String>] [-EmailRecipients <String[]>] [-EmailSubject <String>]
 [-EmailBody <String>] [-Condition <String>] [-Enabled <Boolean>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Updates an existing rule in a SharePoint list or library. Only the specified parameters will be updated.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListRule -List "Demo List" -Identity "12345678-1234-1234-1234-123456789012" -Title "Updated Rule Title"
```

Updates the title of the specified rule.

### EXAMPLE 2
```powershell
Set-PnPListRule -List "Documents" -Identity "My Rule" -Enabled $false
```

Disables the rule with title "My Rule".

### EXAMPLE 3
```powershell
Set-PnPListRule -List "Tasks" -Identity "12345678-1234-1234-1234-123456789012" -EmailRecipients "newuser@contoso.com","admin@contoso.com" -EmailSubject "New Subject"
```

Updates the email recipients and subject for the specified rule.

## PARAMETERS

### -ActionType
The type of action to perform when the rule is triggered.

```yaml
Type: String
Parameter Sets: (All)

Required: False
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
The description for the rule.

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
Whether the rule is enabled or disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID or Title of the rule to update.

```yaml
Type: RulePipeBind
Parameter Sets: (All)

Required: True
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

### -Title
The title/name of the rule.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TriggerEventType
The type of event that triggers the rule.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[SharePoint Alerts Retirement](https://support.microsoft.com/en-us/office/sharepoint-alerts-retirement-813a90c7-3ff1-47a9-8a2f-152f48b2486f)
