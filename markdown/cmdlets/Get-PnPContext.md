---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContext.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContext
---
  
# Get-PnPContext

## SYNOPSIS
Returns the current SharePoint Online CSOM context

## SYNTAX

```powershell
Get-PnPContext [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns a SharePoint Online Client Side Object Model (CSOM) context

## EXAMPLES

### EXAMPLE 1
```powershell
$ctx = Get-PnPContext
```

This will put the current context in the $ctx variable.

### EXAMPLE 2
```powershell
Connect-PnPOnline -Url $siteAurl -Credentials $credentials
$ctx = Get-PnPContext
Get-PnPList # returns the lists from site specified with $siteAurl
Connect-PnPOnline -Url $siteBurl -Credentials $credentials
Get-PnPList # returns the lists from the site specified with $siteBurl
Set-PnPContext -Context $ctx # switch back to site A
Get-PnPList # returns the lists from site A
```

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection. If not provided, the context of the connection will be retrieved from the current connection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)