---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPScriptSafeDomain.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPnPScriptSafeDomain
---
  
# Add-PnPPnPScriptSafeDomain

## SYNOPSIS
Adds a script safe domain to the site collection in the current context

## SYNTAX

```powershell
Add-PnPScriptSafeDomain -DomainName <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command adds a script safe domain to the site collection in the current context. It does not replace or remove existing script safe domains.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPRoleDefinition -DomainName "contoso.com"
```

Creates additional script safe domains.

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

### -DomainName
Name of the new script safe domain name.

```yaml
Type: String
Parameter Sets: (All)

Required: true
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


