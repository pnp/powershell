---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPCopilotAdminLimitedMode.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPCopilotAdminLimitedMode
---
  
# Set-PnPCopilotAdminLimitedMode

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: CopilotSettings-LimitedMode.ReadWrite as delegated permission. Application permission not supported.

Allows configuring whether Copilot in Teams Meetings should respond to sentiment related prompts.

## SYNTAX

```powershell
Set-PnPCopilotAdminLimitedMode -IsEnabledForGroup <boolean> -GroupId <String> [-Verbose] [-Connection <PnPConnection>] 
```
## DESCRIPTION

Represents a setting that controls whether Microsoft 365 Copilot in Teams Meetings users can receive responses to sentiment-related prompts. If this setting is enabled, Copilot in Teams Meetings doesn't respond to sentiment-related prompts and questions asked by the user. If the setting is disabled, Copilot in Teams Meetings responds to sentiment-related prompts and questions asked by the user. Copilot in Teams Meetings currently honors this setting. By default, the setting is disabled.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPCopilotAdminLimitedMode -IsEnabledForGroup:$true -GroupId 32b5ad0f-b502-4083-9d01-0f192f15b2b6
```

This cmdlet will prevent Copilot in Teams Meetings from responding to sentiment related prompts for the specified group.

### EXAMPLE 2
```powershell
Set-PnPCopilotAdminLimitedMode -IsEnabledForGroup:$false
```

This cmdlet will allow Copilot in Teams Meetings to respond to sentiment related prompts for everyone.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsEnabledForGroup
Enables the user to be in limited mode for Copilot in Teams meetings. When enabled, users in this mode can ask any questions, but Copilot doesn't respond to certain questions related to inferring emotions, behavior, or judgments. When disabled, the current mode for Copilot, it responds to any types of questions grounded to the meeting conversation. The default value is false.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupId
The ID of a Microsoft Entra group, of which the value of IsEnabledForGroup is applied value for its members. The default value is null. This parameter is optional. If isEnabledForGroup is set to true, the groupId value must be provided for the IsEnabledForGroup to be enabled for the members of the group.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/copilotadminlimitedmode-update)