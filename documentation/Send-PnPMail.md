---
Module Name: PnP.PowerShell
title: Send-PnPMail
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html
---
 
# Send-PnPMail

## SYNOPSIS
Allows sending an e-mail

## SYNTAX

### Send through Microsoft Graph

```powershell
Send-PnPMail -From <String> -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>] [-Bcc <String[]>] [-ReplyTo <String[]>] [-Importance <MessageImportanceType>] [-BodyContentType <MessageBodyContentType>] [-SaveToSentItems <bool>] [-Connection <PnPConnection>] [-Verbose]
```

### Send through SharePoint Online (Default)

```powershell
Send-PnPMail -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>] [-Bcc <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

### Send through SMTP

```powershell
Send-PnPMail -Server <String> -From <String> -To <String[]> -Subject <String> -Body <String> [-Cc <String[]>] [-Bcc <String[]>] [-Importance <MessageImportanceType>] [-BodyContentType <MessageBodyContentType>] [-ServerPort <short>] [-EnableSsl <bool>] [-Username <String>] [-Password <String>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

Allows sending an e-mail through SharePoint Online, SMTP or Microsoft Graph. Sending e-mail through Microsoft Graph requires the Mail.Send permission.

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
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.onmicrosoft.com" -Subject "Test message" -Body "This is a test message" -Server contoso.mail.protection.outlook.com
```

Sends an e-mail via the SMTP service belonging to a specific tenant. E-mail is sent from the user specified in the From parameter and can be sent only to addresses residing in the tenant you address through the Server parameter, in this case contoso.

### EXAMPLE 5
```powershell
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.com" -Subject "Test message" -Body "This is a test message" -Server smtp.myisp.com 
```

Sends an e-mail via a custom SMTP server of your Internet Service Provider which does not require authentication and uses port TCP 25. E-mail is sent from the user specified in the From parameter and can be sent to both internal and external addresses.

### EXAMPLE 6
```powershell
Send-PnPMail -From "user@contoso.onmicrosoft.com" -To "recipient@contoso.com" -Subject "Test message" -Body "This is a test message" -Server smtp.myisp.com -Port 587 -EnableSsl:$true -Username "userxyz" -Password "password123"
```

Sends an e-mail via a custom SMTP server of your Internet Service Provider which requires authentication and uses SSL over port TCP 587. E-mail is sent from the user specified in the From parameter and can be sent to both internal and external addresses.

## PARAMETERS

### -Body
Body of the email. Accepts simple HTML as `&lt;h1&gt;&lt;/h1&gt;`, `&lt;br/&gt;` etc.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Cc
List of recipients on CC

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Bcc
List of recipients on BCC

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BodyContentType
Allows defining what type of content is in the Body parameter. Defaults to HTML.

```yaml
Type: MessageBodyContentType
Parameter Sets: Send through Microsoft Graph, Send through SMTP
Accepted values: Html, Text

Required: False
Position: Named
Default value: Html
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

### -EnableSsl
Allows you to specify if SSL should be used when connecting to the SMTP server. Only used when the Server parameter is specified.

```yaml
Type: Boolean
Parameter Sets: Send through SMTP

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -From
The sender of the e-mail. When Microsoft Graph is used, this can be a user or a shared mailbox.

```yaml
Type: String
Parameter Sets: Send through SMTP, Send through Microsoft Graph

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Importance
Allows defining what the importance of the e-mail is. Defaults to Normal.

```yaml
Type: MessageImportanceType
Parameter Sets: Send through Microsoft Graph, Send through SMTP
Accepted values: Low, Normal, High

Required: False
Position: Named
Default value: Normal
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username
Username to use to authenticate to the outbound mailserver. Only used when the Server parameter is specified and if not provided, an anonymous connection will be made with the SMTP server specified through Server.

```yaml
Type: String
Parameter Sets: Send through SMTP

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
Password to use to authenticate to the outbound mailserver. Only used when the Server parameter is specified and if not provided, an anonymous connection will be made with the SMTP server specified through Server.

```yaml
Type: String
Parameter Sets: Send through SMTP

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReplyTo
List of return addresses to use for the e-mail

```yaml
Type: String[]
Parameter Sets: Send through Microsoft Graph

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SaveToSentItems
Allows indicating if the sent e-mail should be stored in the Sent Items of the mailbox used to send out the e-mail.

```yaml
Type: String[]
Parameter Sets: Send through Microsoft Graph

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server
SMTP server to use to send the e-mail. You can use the SMTP server of your Internet Service Provider or the SMTP server of your tenant by using tenant.mail.protection.outlook.com where you replace tenant with your own tenant name. This allows you to send e-mail without requiring any authentication, but only to recipients in that tenant. Use a custom SMTP server if you want to send e-mail to various external recipients or go for the Microsoft Graph option in which case you don't specify a server.

```yaml
Type: String
Parameter Sets: Send through SMTP

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerPort
SMTP server port to use to send the e-mail. Used in combination with the Server parameter. Defaults to 25.

```yaml
Type: String
Parameter Sets: Send through SMTP

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Subject
Subject of the email

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -To
List of recipients as a string array

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)