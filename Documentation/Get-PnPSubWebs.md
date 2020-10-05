---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpsubwebs
schema: 2.0.0
title: Get-PnPSubWebs
---

# Get-PnPSubWebs

## SYNOPSIS
Returns the subwebs of the current web

## SYNTAX

```
Get-PnPSubWebs [[-Identity] <WebPipeBind>] [-Recurse] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [-Includes <String[]>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSubWebs
```

Retrieves all subsites of the current context returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 2
```powershell
Get-PnPSubWebs -Recurse
```

Retrieves all subsites of the current context and all of their nested child subsites returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 3
```powershell
Get-PnPSubWebs -Recurse -Includes "WebTemplate","Description" | Select ServerRelativeUrl, WebTemplate, Description
```

Retrieves all subsites of the current context and shows the ServerRelativeUrl, WebTemplate and Description properties in the resulting output

### EXAMPLE 4
```powershell
Get-PnPSubWebs -Identity Team1 -Recurse
```

Retrieves all subsites of the subsite Team1 and all of its nested child subsites returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
If provided, only the subsite with the provided Id, GUID or the Web instance will be returned

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recurse
If provided, recursion through all subsites and their children will take place to return them as well

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
The web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)