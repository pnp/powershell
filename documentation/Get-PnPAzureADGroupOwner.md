---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADGroupOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADGroupOwner
---

# Get-PnPAzureADGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets owners of a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

### Default (Default)

```
Get-PnPAzureADGroupOwner -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to list owners from a given Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADGroupOwner -Identity $groupId
```

Retrieves all the owners of a specific Azure Active Directory group based on its ID.

### EXAMPLE 2

```powershell
Get-PnPAzureADGroupOwner -Identity $group
```

Retrieves all the owners of a specific Azure Active Directory group based on the group's object instance.

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

The identity of the Azure Active Directory group.

```yaml
Type: AzureADGroupPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
