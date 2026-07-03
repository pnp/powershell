---
Module Name: PnP.PowerShell
title: Set-PnPContext
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContext.html
---
 
# Set-PnPContext

## SYNOPSIS
Set the ClientContext

## SYNTAX

```powershell
Set-PnPContext -Context <ClientContext> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets the Client Context to be used by the cmdlets, which allows easy context switching. See examples for details.

## EXAMPLES

### EXAMPLE 1
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

### -Context
The ClientContext to set

```yaml
Type: ClientContext
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying -ReturnConnection on Connect-PnPOnline. If not provided, the connection will be retrieved from the current context.

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