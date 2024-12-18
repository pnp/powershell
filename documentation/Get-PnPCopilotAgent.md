---
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PnP.PowerShell
ms.date: 12/06/2024
PlatyPS schema version: 2024-05-01
title: Get-PnPCopilotAgent
---

# Get-PnPCopilotAgent

## SYNOPSIS

Returns the Microsoft Copilot Agents (*.agent) in a site collection.

## SYNTAX

### __AllParameterSets

```powershell
Get-PnPCopilotAgent [-ServerRelativeUrl <string>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet iterates through the document libraries in a site and finds the copilot agents in that site.

## EXAMPLES

### Example 1

```powershell
Get-PnPCopilotAgent
```

This will return all the Microsoft Copilot agents in a site.


### Example 2

```powershell
Get-PnPCopilotAgent -ServerRelativeUrl /sites/demo/siteassets/copilots/approved/main.agent
```

This will return the specific Microsoft Copilot agent if it exists.


## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnP.PowerShell.Commands.Base.PnPConnection
DefaultValue: ''
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

### -ServerRelativeUrl

The server relative URL to the .agent file.

```yaml
Type: System.String
DefaultValue: ''
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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)