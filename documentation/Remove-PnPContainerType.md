---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContainerType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPContainerType
---
  
# Remove-PnPContainerType

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

The Remove-PnPContainerType cmdlet removes a trial container from the SharePoint tenant. The container to remove is specified by the Identity parameter.


## SYNTAX

```powershell
Remove-PnPContainerType [-Identity] <Guid> [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPContainerType -Identity 00be1092-0c75-028a-18db-89e57908e7d6 
```

Removes the specified trial container by using the container id.

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

Specify the container id.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True 
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)