---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSharePointAddIn.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSharePointAddIn
---
  
# Get-PnPSharePointAddIn

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the list of SharePoint addins installed in the site collection

## SYNTAX

```powershell
Get-PnPSharePointAddIn [-IncludeSubsites <SwitchParameter>] [-Connection <PnPConnection>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSharePointAddIn
```

Returns the SharePoint addins installed in your site collection

### EXAMPLE 2
```powershell
Get-PnPSharePointAddIn -IncludeSubsites
```

Returns the SharePoint addins installed in your site collection as well as the subsites.

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

### -IncludeSubsites
When specified, it determines whether we should use also search the subsites of the connected site collection and lists the installed AddIns.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


