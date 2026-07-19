---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPCopilotAdminLimitedMode.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPCopilotAdminLimitedMode
---
  
# Get-PnPCopilotAdminLimitedMode

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: CopilotSettings-LimitedMode.Read or CopilotSettings-LimitedMode.ReadWrite as delegated permission. Application permission not supported.

Returns the current configuration if Copilot in Teams Meetings should respond to sentiment related prompts.

## SYNTAX

```powershell
Get-PnPCopilotAdminLimitedMode [-Verbose] [-Connection <PnPConnection>] 
```
## DESCRIPTION

Returns a setting that controls whether Microsoft 365 Copilot in Teams Meetings users can receive responses to sentiment-related prompts. If this setting is enabled, Copilot in Teams Meetings doesn't respond to sentiment-related prompts and questions asked by the user. If the setting is disabled, Copilot in Teams Meetings responds to sentiment-related prompts and questions asked by the user. Copilot in Teams Meetings currently honors this setting. By default, the setting is disabled.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPCopilotAdminLimitedMode
```

This cmdlet will return the current configuration if Copilot in Teams Meetings should respond to sentiment related prompts.

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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/copilotadminlimitedmode-get)