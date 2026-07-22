---
Module Name: PnP.PowerShell
title: Get-PnPSubWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSubWeb.html
---
 
# Get-PnPSubWeb

## SYNOPSIS
Returns the subwebs of the current web

## SYNTAX

```powershell
Get-PnPSubWeb [[-Identity] <WebPipeBind>] [-Recurse] [-Connection <PnPConnection>]
 [-Includes <String[]>] 
```

## DESCRIPTION

Allows to retrieve subwebs of the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSubWeb
```

Retrieves all subsites of the current context returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 2
```powershell
Get-PnPSubWeb -Recurse
```

Retrieves all subsites of the current context and all of their nested child subsites returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 3
```powershell
Get-PnPSubWeb -Recurse -Includes "WebTemplate","Description" | Select ServerRelativeUrl, WebTemplate, Description
```

Retrieves all subsites of the current context and shows the ServerRelativeUrl, WebTemplate and Description properties in the resulting output

### EXAMPLE 4
```powershell
Get-PnPSubWeb -Identity Team1 -Recurse
```

Retrieves all subsites of the subsite Team1 and all of its nested child subsites

### EXAMPLE 5
```powershell
Get-PnPSubWeb -Identity Team1 -Recurse -IncludeRootWeb
```

Retrieves the root web, all subsites of the subsite Team1 and all of its nested child subsites

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

### -Identity
If provided, only the subsite with the provided Id, GUID or the Web instance will be returned

```yaml
Type: WebPipeBind
Parameter Sets: (All)

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

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeRootWeb
If provided, the results will also contain the root web

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned sub web which are not included in the response by default

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

