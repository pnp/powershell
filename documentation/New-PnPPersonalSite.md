---
Module Name: PnP.PowerShell
title: New-PnPPersonalSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPPersonalSite.html
---
 
# New-PnPPersonalSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Office365 only: Creates a personal / OneDrive For Business site

## SYNTAX

```powershell
New-PnPPersonalSite [-Email] <String[]> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
$users = ('katiej@contoso.onmicrosoft.com','garth@contoso.onmicrosoft.com')
                 New-PnPPersonalSite -Email $users
```

Creates a personal / OneDrive For Business site for the 2 users in the variable $users

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

### -Email
The UserPrincipalName (UPN) of the users

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

