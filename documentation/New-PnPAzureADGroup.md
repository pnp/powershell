---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPAzureADGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPAzureADGroup
---

# New-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.Create, Group.ReadWrite.All

Creates a new Azure Active Directory group. This can be a security or distribution group.

## SYNTAX

### Default (Default)

```
New-PnPAzureADGroup -DisplayName <String> -Description <String> -MailNickname <String>
 [-Owners <String[]>] [-Members <String[]>] [-IsSecurityEnabled <SwitchParameter>]
 [-IsMailEnabled <SwitchParameter>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create an Azure Active Directory group. This can be either security or distribution group.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPAzureADGroup -DisplayName $displayName -Description $description -MailNickname $nickname
```

Creates an Azure Active Directory group with all the required properties

### EXAMPLE 2

```powershell
New-PnPAzureADGroup -DisplayName $displayName -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers
```

Creates a new Azure Active Directory group with all the required properties, and with a custom list of Owners and a custom list of Members

### EXAMPLE 3

```powershell
New-PnPAzureADGroup -DisplayName $displayName -Description $description -MailNickname $nickname -IsSecurityEnabled -IsMailEnabled
```

Creates a new Azure Active Directory group which is mail and security enabled

## PARAMETERS

### -Description

The Description of the Azure Active Directory group

```yaml
Type: String
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

### -DisplayName

The Display Name of the Azure Active Directory group

```yaml
Type: String
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

### -Force

Specifying the Force parameter will skip the confirmation question.

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

### -IsMailEnabled

Creates an Azure Active Directory group which can be used to send e-mail to

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

### -IsSecurityEnabled

Creates an Azure Active Directory group which can be used to set permissions

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

### -MailNickname

The Mail Nickname of the Azure Active Directory group. Cannot contain spaces.

```yaml
Type: String
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

### -Members

The array UPN values of the group's members

```yaml
Type: String[]
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

### -Owners

The array UPN values of the group's owners

```yaml
Type: String[]
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-groups)
