---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Send-PnPMail
---

# Send-PnPMail

## SYNOPSIS

Allows sending an e-mail

## SYNTAX

### Send through Microsoft Graph with attachments from local file system

```
Send-PnPMail -From <String> -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>]
 [-Bcc <String[]>] [-ReplyTo <String[]>] [-Importance <MessageImportanceType>]
 [-BodyContentType <MessageBodyContentType>] [-SaveToSentItems <bool>] [-Connection <PnPConnection>]
 [-Verbose] [-Attachments <String[]>]
```

### Send through Microsoft Graph with attachments from SPO

```
Send-PnPMail -From <String> -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>]
 [-Bcc <String[]>] [-ReplyTo <String[]>] [-Importance <MessageImportanceType>]
 [-BodyContentType <MessageBodyContentType>] [-SaveToSentItems <bool>] [-Connection <PnPConnection>]
 [-Verbose] [-Files <String[]>]
```

### Send through SharePoint Online (Default)

```
Send-PnPMail -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>] [-Bcc <String[]>]
 [-Connection <PnPConnection>] [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows sending an e-mail through SharePoint Online or Microsoft Graph. Sending e-mail through Microsoft Graph requires the **Mail.Send** permission.

## EXAMPLES

### EXAMPLE 1

```powershell
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.com" -Subject "Test message" -Body "This is a test message"
```

Sends an e-mail using Microsoft Graph to one recipient. E-mail is sent from the user specified in the From parameter and can be sent to both internal and external addresses. A copy of the sent e-mail will be stored in the mailbox of the user specified in the From parameter.

### EXAMPLE 2

```powershell
Send-PnPMail -From "sharedmailbox@contoso.onmicrosoft.com" -To "recipient1@contoso.com","recipient2@contoso.com","recipient3@contoso.com" -Cc "recipient4@contoso.com" -Bcc "recipient5@contoso.com" -Subject "Test message" -Body "This is a test message" -Importance Low
```

Sends an e-mail using Microsoft Graph from a shared mailbox to multiple recipients. E-mail is sent from the shared mailbox specified in the From parameter and can be sent to both internal and external addresses. A copy of the sent e-mail will be stored in the shared mailbox.

### EXAMPLE 3

```powershell
Send-PnPMail -To "address@tenant.microsoftonline.com" -Subject "Test message" -Body "This is a test message"
```

Sends an e-mail using the SharePoint Online SendEmail method using the current context. E-mail is sent from the SharePoint Online no-reply e-mail address and can only be sent to accounts in the same tenant. The from address will show the title of the site you are connected with along with the e-mail address no-reply@sharepointonline.com.

### EXAMPLE 4

```powershell
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.com" -Subject "Test message" -Body "This is a test message" -Attachments "C:\PnPCommunity\Test\test.docx"
```

Sends an e-mail using Microsoft Graph to one recipient. E-mail is sent from the user specified in the From parameter and can be sent to both internal and external addresses. A copy of the sent e-mail will be stored in the mailbox of the user specified in the From parameter. It will also upload the file from the local file system as attachment.

### EXAMPLE 5

```powershell
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.com" -Subject "Test message" -Body "This is a test message" -Files "/sites/test/Shared Documents/Test.docx"
```

Sends an e-mail using Microsoft Graph to one recipient. E-mail is sent from the user specified in the From parameter and can be sent to both internal and external addresses. A copy of the sent e-mail will be stored in the mailbox of the user specified in the From parameter. It will also upload the file from the SharePoint site collection and send it as attachment.

## PARAMETERS

### -Attachments

List of attachments from local file system to be uploaded and sent as attachments.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Bcc

List of recipients on BCC

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

### -Body

Body of the email. Accepts simple HTML as `&lt;h1&gt;&lt;/h1&gt;`, `&lt;br/&gt;` etc.

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

### -BodyContentType

Allows defining what type of content is in the Body parameter. Defaults to HTML.

```yaml
Type: MessageBodyContentType
DefaultValue: Html
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Html
- Text
HelpMessage: ''
```

### -Cc

List of recipients on CC

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

### -Files

List of files from the SharePoint site collection to be sent as attachments.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -From

The sender of the e-mail. When Microsoft Graph is used, this can be a user or a shared mailbox.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Importance

Allows defining what the importance of the e-mail is. Defaults to Normal.

```yaml
Type: MessageImportanceType
DefaultValue: Normal
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Low
- Normal
- High
HelpMessage: ''
```

### -ReplyTo

List of return addresses to use for the e-mail

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SaveToSentItems

Allows indicating if the sent e-mail should be stored in the Sent Items of the mailbox used to send out the e-mail.

```yaml
Type: String[]
DefaultValue: True
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Send through Microsoft Graph with attachments from SPO
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Send through Microsoft Graph with attachments from local file system
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Subject

Subject of the email

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

### -To

List of recipients as a string array

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
