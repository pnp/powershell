---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpsiteclosure
schema: 2.0.0
title: Set-PnPSiteClosure
---

# Set-PnPSiteClosure

## SYNOPSIS
Opens or closes a site which has a site policy applied

## SYNTAX

```powershell
Set-PnPSiteClosure -State <ClosureState> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteClosure -State Open
```

This opens a site which has been closed and has a site policy applied.

### EXAMPLE 2
```powershell
Set-PnPSiteClosure -State Closed
```

This closes a site which is open and has a site policy applied.

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

### -State
The state of the site

```yaml
Type: ClosureState
Parameter Sets: (All)
Accepted values: Open, Closed

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)