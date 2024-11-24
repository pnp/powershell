---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPMicrosoft365GroupOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPMicrosoft365GroupOwner
---

# Add-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds users to a Microsoft 365 Group as Owners.

## SYNTAX

### Default (Default)

```
Add-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind> -Users <String[]>
 [-RemoveExisting] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add multiple users to Microsoft 365 Group as owners.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPMicrosoft365GroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional owners to the Microsoft 365 Group named "Project Team".

### EXAMPLE 2

```powershell
Add-PnPMicrosoft365GroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com" -RemoveExisting
```

Sets the provided two users as the only owners of the Microsoft 365 Group named "Project Team" by removing any current existing members first.

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

The Identity of the Microsoft 365 Group to add owners to.

```yaml
Type: Microsoft365GroupPipeBind
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

### -RemoveExisting

If provided, all existing owners will be removed and only those provided through Users parameter will become owners.

```yaml
Type: SwitchParameter
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

### -Users

The UPN(s) of the user(s) to add to the Microsoft 365 Group as an owner.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-members)
