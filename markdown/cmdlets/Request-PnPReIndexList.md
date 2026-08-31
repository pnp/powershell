---
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
applicable: SharePoint Online
title: Request-PnPReIndexList
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPReIndexList.html
tags: Available in the current Nightly Release only.
---
  
# Request-PnPReIndexList

## SYNOPSIS
Marks the list for full indexing during the next incremental crawl

## SYNTAX

```powershell
Request-PnPReIndexList [-Identity] <ListPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to mark the list for full indexing during the next incremental crawl.

## EXAMPLES

### EXAMPLE 1
```powershell
Request-PnPReIndexList -Identity "Demo List"
```

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
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


