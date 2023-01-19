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
Sends an email using the Office 365 SMTP Service or SharePoint, depending on the parameters specified.

## SYNTAX

```powershell
Send-PnPMail [-Server <String>] [-From <String>] [-Password <String>] -To <String[]> [-Cc <String[]>] [-Bcc <String[]>] -Subject <String> -Body <String> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to send an email using the Office 365 SMTP Service or SharePoin.

## EXAMPLES

### EXAMPLE 1
```powershell
Send-PnPMail -To address@tenant.microsoftonline.com -Subject test -Body test
```

Sends an e-mail using the SharePoint SendEmail method using the current context. E-mail is sent from the system account and can only be sent to accounts in the same tenant.

### EXAMPLE 2
```powershell
Send-PnPMail -To address@contoso.com -Subject test -Body test -From me@tenant.onmicrosoft.com -Password xyz
```

Sends an e-mail via Office 365 SMTP and requires a from address and password. E-mail is sent from the from user and can be sent to both internal and external addresses.

### EXAMPLE 3
```powershell
Send-PnPMail -To address@contoso.com -Subject test -Body test -From me@server.net -Password xyz -Server yoursmtp.server.net
```

Sends an e-mail via a custom SMTP server and requires a from address and password. E-mail is sent from the from user.

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

### -From
If using from address, you also have to provide a password

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
If using a password, you also have to provide the associated from address

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server

```yaml
Type: String
Parameter Sets: (All)

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
