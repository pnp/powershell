---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFileSharingInvite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFileSharingInvite
---

# Add-PnPFileSharingInvite

## SYNOPSIS

Creates an invitation for users to a file.

## SYNTAX

### Default (Default)

```
Add-PnPFileSharingInvite -FileUrl <String> -Users <String[]> -Message <String>
 -RequireSignIn <SwitchParameter> -SendInvitation <SwitchParameter> -Role <PermissionRole>
 -ExpirationDateTime <DateTime> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates an invitation for users to a file.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPFileSharingInvite -FileUrl "/sites/demo/Shared Documents/Test.docx" -Users "john@contoso.onmicrosoft.com" -RequireSignIn
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test.docx` file with Read permissions and will require that user to sign in before accessing the file.

### EXAMPLE 2

```powershell
Add-PnPFileSharingInvite -FileUrl "/sites/demo/Shared Documents/Test.docx" -Users "john@contoso.onmicrosoft.com" -RequireSignIn -SendInvitation -Role Owner
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test.docx` file with Read permissions and will require that user to sign in before accessing the file. The invitation will be sent and the user will have `Owner` permissions

### EXAMPLE 3

```powershell
Add-PnPFileSharingInvite -FileUrl "/sites/demo/Shared Documents/Test.docx" -Users "john@contoso.onmicrosoft.com" -RequireSignIn -ExpirationDate (Get-Date).AddDays(15)
```

This will invite the user `John@contoso.onmicrosoft.com` to the `Test.docx` file with Read permissions and will require that user to sign in before accessing the file. The link will expire after 15 days.

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

### -ExpirationDateTime

The expiration date for the FileUrl after which the permission expires.

```yaml
Type: String
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

### -FileUrl

The FileUrl in the site

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

### -Message

A plain text formatted message that is included in the sharing invitation. Maximum length 2000 characters.

```yaml
Type: String
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

### -RequireSignIn

Specifies where the recipient of the invitation is required to sign-in to view the shared item

```yaml
Type: SwitchParamter
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

### -Role

Specify the role that is to be granted to the recipients of the sharing invitation.

Supported values are `Owner`, `Write` and `Read`.

```yaml
Type: PnP.Core.Model.Security.PermissionRole
DefaultValue: Read
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

### -SendInvitation

Specifies if an email or post is generated (false) or if the permission is just created (true).

```yaml
Type: SwitchParamter
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

A collection of recipients who will receive access and the sharing invitation.
**Currently, only one user at a time is supported**. We are planning to add support for multiple users a bit later.

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
