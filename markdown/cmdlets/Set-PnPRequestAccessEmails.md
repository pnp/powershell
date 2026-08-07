---
Module Name: PnP.PowerShell
title: Set-PnPRequestAccessEmails
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPRequestAccessEmails.html
---
 
# Set-PnPRequestAccessEmails

## SYNOPSIS
Sets Request Access Email on a web

## SYNTAX

```powershell
Set-PnPRequestAccessEmails [-Emails <String[]>] [-Disabled] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Enables or disables access requests to be sent and configures which e-mail address should receive these requests. The web you apply this on must have unique rights.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPRequestAccessEmails -Emails someone@example.com
```

This will enable requesting access and send the requests to the provided e-mail address

### EXAMPLE 2
```powershell
Set-PnPRequestAccessEmails -Disabled
```

This will disable the ability to request access to the site

### EXAMPLE 3
```powershell
Set-PnPRequestAccessEmails -Disabled:$false
```

This will enable the ability to request access to the site and send the requests to the default owners of the site

## PARAMETERS

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

### -Disabled
Enables or disables access to be requested

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Emails
Email address to send the access requests to

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

