---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPGroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPGroupMember
---

# Add-PnPGroupMember

## SYNOPSIS

Adds a user to a SharePoint group

## SYNTAX

### Internal

```
Add-PnPGroupMember -LoginName <String> -Group <GroupPipeBind> [-Connection <PnPConnection>]
```

### External

```
Add-PnPGroupMember -Group <GroupPipeBind> -EmailAddress <String> [-SendEmail] [-EmailBody <String>]
 [-Connection <PnPConnection>]
```

### Batched

```
Add-PnPGroupMember -LoginName <String> -Group <GroupPipeBind> -Batch <PnPBatch>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add new user to SharePoint group. The SharePoint group may be specified either by id, name or related object.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPGroupMember -LoginName user@company.com -Group 'Marketing Site Members'
```

Add the specified user to the SharePoint group "Marketing Site Members"

### EXAMPLE 2

```powershell
Add-PnPGroupMember -LoginName user@company.com -Group 5
```

Add the specified user to the SharePoint group with Id 5

### EXAMPLE 3

```powershell
$batch = New-PnPBatch
Add-PnPGroupMember -LoginName user@company.com -Group 5 -Batch $batch
Add-PnPGroupMember -LoginName user1@company.com -Group 5 -Batch $batch
Invoke-PnPBatch $batch
```

Add the specified users to the SharePoint group with Id 5 in a batch.

## PARAMETERS

### -Batch



```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
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

### -EmailAddress

The email address of the user

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: External
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EmailBody



```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: External
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Group

The SharePoint group id, SharePoint group name or SharePoint group object to add the user to

```yaml
Type: GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LoginName

The login name of the user

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Internal
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SendEmail



```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: External
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
